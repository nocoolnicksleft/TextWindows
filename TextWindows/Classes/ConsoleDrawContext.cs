using System;

namespace TextWindows
{
	public class ConsoleDrawContext
	{
		int _offsetX;
		int _offsetY;

		int _height;
		int _width;

		int _cursorX;
		int _cursorY;

		ConsoleColor _backgroundColor = ConsoleColor.Black;
		ConsoleColor _foregroundColor = ConsoleColor.White;

		public ConsoleColor BackgroundColor { get => _backgroundColor; set => _backgroundColor = value; }
		public ConsoleColor ForegroundColor { get => _foregroundColor; set => _foregroundColor = value; }

		public ConsoleDrawContext(int p_offsetX, int p_offsetY, int p_width, int p_height)
		{
			_offsetX = p_offsetX;
			_offsetY = p_offsetY;
			_width = p_width;
			_height = p_height;
			_cursorX = 0;
			_cursorY = 0;
		}

		public ConsoleDrawContext GetNewContext(int p_offsetX, int p_offsetY, int p_width, int p_height)
		{
			ConsoleDrawContext newContext = new ConsoleDrawContext(_offsetX + p_offsetX, _offsetY + p_offsetY, p_width, p_height);
			newContext.BackgroundColor = _backgroundColor;
			newContext.ForegroundColor = _foregroundColor;
			return newContext;
		}

		public void DrawFrame(TextWindowFrameType p_frameType, int p_x, int p_y, int p_width, int p_height)
		{
			if (p_frameType == TextWindowFrameType.None) return;

			if (p_x > _width) return;
			if (p_y > _height) return;

			// Single is default
			char fcTopLeft = '┌';
			char fcHorzTop = '─';
			char fcTopRight = '┐';
			char fcVertLeft = '│';
			char fcVertRight = '│';
			char fcBottomLeft = '└';
			char fcHorzBottom = '─';
			char fcBottomRight = '┘';

			switch (p_frameType)
			{
				case TextWindowFrameType.Double:
					fcTopLeft = '╔';
					fcHorzTop = fcHorzBottom = '═';
					fcTopRight = '╗';
					fcVertLeft = fcVertRight = '║';
					fcBottomLeft = '╚';					
					fcBottomRight = '╝';
					break;

				case TextWindowFrameType.Empty:
					fcTopLeft = fcHorzTop = fcTopRight = ' ';
					fcVertLeft = fcVertRight = ' ';
					fcBottomLeft = fcHorzBottom = fcBottomRight = ' ';
					break;

				case TextWindowFrameType.Block:
					fcTopLeft = fcHorzTop = fcTopRight = '▄';
					fcVertLeft = fcVertRight = '█';
					fcBottomLeft = fcHorzBottom = fcBottomRight = '▀';
					break;

				case TextWindowFrameType.Dots:
					fcTopLeft = fcHorzTop = fcTopRight = '■';
					fcVertLeft = fcVertRight = '■';
					fcBottomLeft = fcHorzBottom = fcBottomRight = '■';
					break;

				case TextWindowFrameType.Stars:
					fcTopLeft = fcHorzTop = fcTopRight = '*';
					fcVertLeft = fcVertRight = '*';
					fcBottomLeft = fcHorzBottom = fcBottomRight = '*';
					break;
			}

			int horzLineWidth = Math.Min(p_width - 2, _width - 1);
			int vertLineHeight = Math.Min(p_height - 2, _height - 1);
			bool rightEdgeVisible = (horzLineWidth == p_width - 2);
			bool bottomEdgeVisible = (vertLineHeight == p_height - 2);

			Console.ForegroundColor = _foregroundColor;
			Console.BackgroundColor = _backgroundColor;

			Console.SetCursorPosition(_offsetX + p_x, _offsetY + p_y);
			Console.Write(fcTopLeft);
			Console.Write(new string(fcHorzTop, horzLineWidth));
			if (rightEdgeVisible)
			{
				Console.Write(fcTopRight);
			}

			for (int iy = _offsetY + p_y + 1; iy < _offsetY + p_y + vertLineHeight + 1; iy++)
			{
				Console.SetCursorPosition(_offsetX + p_x , iy);
				Console.Write(fcVertLeft);
				if (rightEdgeVisible)
				{
					Console.SetCursorPosition(_offsetX + p_x + p_width - 1, iy);
					Console.Write(fcVertRight);
				}
			}

			if (bottomEdgeVisible)
			{
				Console.SetCursorPosition(_offsetX + p_x, _offsetY + p_y + p_height - 1);
				Console.Write(fcBottomLeft);
				Console.Write(new string(fcHorzBottom, horzLineWidth));
				if (rightEdgeVisible)
				{
					Console.Write(fcBottomRight);
				}
			}


		}

		public void Clear()
		{
			Console.ForegroundColor = _foregroundColor;
			Console.BackgroundColor = _backgroundColor;
			DrawRect(0, 0, _width, _height);
		}

		public void SetCursor(int x, int y)
		{
			_cursorX = Math.Min(_cursorX, _width);
			_cursorY = Math.Min(_cursorY, _height);
			Console.SetCursorPosition(_offsetX + _cursorX, _offsetY + _cursorY);
		}

		public void DrawRect(int p_x, int p_y, int p_width, int p_height)
		{
			p_x += _offsetX;
			p_y += _offsetY;

			p_height = Math.Min(p_height, _height);
			p_width = Math.Min(p_width, _width);

			for (int iy = p_y; iy < (p_y + p_height); iy++)
			{
				Console.SetCursorPosition(p_x, iy);
				Console.Write(new string(' ',p_width));
			}
		}

		public void DrawText(int p_x, int p_y, string p_text)
		{
			Console.SetCursorPosition(_offsetX + p_x, _offsetY + p_y);
			Console.Write(p_text.Substring(0, _width - p_x));
		}

		public void Write(string p_text)
		{

		}

		public void WriteLine(string p_text)
		{

		}

	}
}
