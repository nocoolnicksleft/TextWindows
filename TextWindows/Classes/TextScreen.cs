using System;

namespace TextWindows
{
    public class TextScreen : TextWindow
    {
		public TextScreen(int p_width, int p_height, string p_title)
			: base(TextWindowType.NoFrame,  0, 0, p_width, p_height, p_title)
		{
			
		}


		public virtual void Initialize()
		{
			base.Initialize();

			Console.Title = Title;

			Width = Math.Min(this.Width, Console.LargestWindowWidth);
			Height = Math.Min(this.Height, Console.LargestWindowHeight);

			Console.SetWindowSize(this.Width, this.Height);
			Console.SetBufferSize(this.Width, this.Height);

			Console.CursorVisible = false;

			Console.BackgroundColor = BackgroundColor;
			Console.ForegroundColor = ForegroundColor;

			Console.Clear();		
		}



	}
}
