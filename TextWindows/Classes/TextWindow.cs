using System;
using System.Collections.Generic;

namespace TextWindows
{

	public class TextWindow
    {

		static int LastWindowId = 100;

		int _id;

		TextWindowType _type;

		TextWindowFrameType _frameType = TextWindowFrameType.Single;

		bool _visible;

		bool _dirty;

		int _top;
		int _left;
		int _height;
		int _width;

		int _zIndex;

		ConsoleColor _backgroundColor = ConsoleColor.Black;
		ConsoleColor _foregroundColor = ConsoleColor.White;
		ConsoleColor _frameBackgroundColor = ConsoleColor.Black;
		ConsoleColor _frameForegroundColor = ConsoleColor.White;

		string _title;



		List<TextWindow> _children = new List<TextWindow>();

		public TextWindow(TextWindowType p_type, int p_left, int p_top, int p_width, int p_height, string p_title)
		{
			_id = LastWindowId++;
			_type = p_type;
			_top = p_top;
			_left = p_left;
			_height = p_height;
			_width = p_width;
			_title = p_title;
			_visible = true;
			_dirty = false;
			
		}

		public virtual void OnDraw(ConsoleDrawContext windowContext)
		{

			if (_visible) {

				ConsoleDrawContext clientContext = null;

				if (_type == TextWindowType.Frame)
				{
					windowContext.ForegroundColor = _frameForegroundColor;
					windowContext.BackgroundColor = _frameBackgroundColor;
					windowContext.DrawFrame(_frameType, 0, 0, _width, _height);

					if (!string.IsNullOrEmpty(_title))
					{
						windowContext.DrawText(2, 0, _title);
					}

					clientContext = windowContext.GetNewContext(1, 1, _width - 2, _height - 2);

					clientContext.ForegroundColor = _foregroundColor;
					clientContext.BackgroundColor = _backgroundColor;
					clientContext.Clear();
					
				}
				else
				{
					if (_type != TextWindowType.Transparent)
					{
						windowContext.ForegroundColor = _foregroundColor;
						windowContext.BackgroundColor = _backgroundColor;
						windowContext.Clear();
					}

					clientContext = windowContext;
					
				}

				OnClientDraw(clientContext);

			}
		}

		public void AddChild(TextWindow p_newWindow)
		{
			_children.Add(p_newWindow);
			
		}


		public virtual void OnClientDraw(ConsoleDrawContext p_context)
		{
			p_context.DrawText(0,0,"Client Area");
		}

		public virtual void Draw()
		{
			ConsoleDrawContext drawContext = new ConsoleDrawContext(0,0,_height,_width);
			Draw(drawContext);
		}

		public virtual void Draw(ConsoleDrawContext p_context)
		{
			ConsoleDrawContext myContext = p_context.GetNewContext(_left, _top, _width, _height);

			OnDraw(myContext);
			foreach (TextWindow _child in _children)
			{
				_child.Draw(myContext);
			}
		}

		public virtual void Initialize()
		{

		}


		public int Id { get => _id; }

		public TextWindowType Type { get => _type; }

		public int ZIndex { get => _zIndex; set => _zIndex = value; }

		public int Top { get => _top; set => _top = value; }
		public int Left { get => _left; set => _left = value; }
		public int Height { get => _height; set => _height = value; }
		public int Width { get => _width; set => _width = value; }

		public int ClientHeight
		{
			get
			{
				if (_type == TextWindowType.NoFrame) return _height;
				return _height - 2;
			}
		}

		public int ClientWidth
		{
			get
			{
				if (_type == TextWindowType.NoFrame) return _width;
				return _width - 2;
			}
		}

		public ConsoleColor BackgroundColor { get => _backgroundColor; set => _backgroundColor = value; }
		public ConsoleColor ForegroundColor { get => _foregroundColor; set => _foregroundColor = value; }
		public ConsoleColor FrameBackgroundColor { get => _frameBackgroundColor; set => _frameBackgroundColor = value; }
		public ConsoleColor FrameForegroundColor { get => _frameForegroundColor; set => _frameForegroundColor = value; }

		public string Title { get => _title; set => _title = value; }
		
		public bool Visible { get => _visible; set => _visible = value; }
		public bool Dirty { get => _dirty; set => _dirty = value; }
		public TextWindowFrameType FrameType { get => _frameType; set => _frameType = value; }
	}
}
