using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TextWindows;

namespace Demo
{
	class Program
	{
		static void Main(string[] args)
		{

			var Desktop = new TextWindows.TextScreen(100, 40, "My Textop");
			Desktop.BackgroundColor = ConsoleColor.Blue;
			Desktop.ForegroundColor = ConsoleColor.Green;
			Desktop.Initialize();

			var testWindow1 = new TextWindows.TextWindow(TextWindowType.Frame, 10, 10, 20, 5, "Testfenster 1");
			var testWindow2 = new TextWindows.TextWindow(TextWindowType.Frame, 80, 20, 23, 10, "Testfenster 2");
			var testWindow3 = new TextWindows.TextWindow(TextWindowType.WideFrame, 44, 20, 25, 10, "Testfenster 4");
			testWindow3.BackgroundColor = ConsoleColor.Red;
			testWindow3.ForegroundColor = ConsoleColor.Yellow;
			testWindow3.FrameBackgroundColor = ConsoleColor.Red;
			testWindow3.FrameForegroundColor = ConsoleColor.White;
			Desktop.AddChild(testWindow1);
			//Desktop.AddChild(testWindow2);
			Desktop.AddChild(testWindow3);
			Desktop.Draw();
			/*
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			Console.WriteLine("öäüßÖÄÜABC");
			Console.WriteLine("╗╝█╣");
			Console.WriteLine("");
			*/
			Console.ReadLine();
		}
	}
}
