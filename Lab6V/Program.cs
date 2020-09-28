using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6V
{
	class Program
	{
		static void Main(string[] args)
		{
			int height = 30;
			int width = 40;
			int targets = 100;
			int scouts = 5;
			int threadSpead = 100;
			Console.CursorVisible = false;
			AerialReconnaissance aerialReconnaissance = null;
			Console.WriteLine("Press Space to start");
			void Start()
			{
				while (true)
				{
					ConsoleKeyInfo keypress = Console.ReadKey(true);
					if (keypress.Key == ConsoleKey.Spacebar)
					{
						if (aerialReconnaissance != null)
						{
							aerialReconnaissance.KillThreads();
						}
						Console.CursorVisible = false;
						Console.Clear();
						Console.SetCursorPosition(0, 0);
						Field field = new Field(width, height, targets);
						field.Print();
						aerialReconnaissance = new AerialReconnaissance(field, scouts, threadSpead);
						aerialReconnaissance.Start();
					}
				}
			}
			Start();
		}
	}
}
