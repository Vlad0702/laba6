using System;

namespace Lab6V
{
	class Field
	{
		private int height, width, targetsAmount;
		private byte[,] field;
		Random rand = new Random();

		public Field(int width, int height, int targetsAmount)
		{
			Width = width;
			Height = height;
			TargetsAmount = targetsAmount;
			Init();
		}

		public int Height { get
			{
				return height;
			}

			set
			{
				if (value < 5)
				{
					throw new Exception("Слишком маленькая высота");
				}

				height = value;
			}
		}

		public int Width
		{
			get
			{
				return width;
			}

			set
			{
				if (value < 5)
				{
					throw new Exception("Слишком маленькая ширина");
				}

				width = value;
			}
		}

		public int TargetsAmount {
			get
			{
				return targetsAmount;
			}

			set
			{
				if (value < 1)
				{
					throw new Exception("Слишком мало целей");
				}

				if (value > (width-2)*(height-2) )
				{
					throw new Exception("Слишком много целей");
				}

				targetsAmount = value;
			}
		}

		public void Init()
		{
			field = new byte[height, width];
			SetTargets();
		}

		public void SetTargets()
		{
			int count = 0;
			while (count < targetsAmount)
			{
				int i = rand.Next(height - 1);
				int j = rand.Next(width - 1);
				if (field[i,j] == 2)
				{
					continue;
				}
				field[i, j] = 2;
				count++;
			}
		}

		public void Print()
		{
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					if (field[i, j] == 2 || field[i, j] == 1 || field[i, j] == 3)
					{
						if (field[i, j] == 2) { 
							Console.ForegroundColor = ConsoleColor.Red;
							Console.Write("?");
						}
						if (field[i, j] == 3) { 
							Console.ForegroundColor = ConsoleColor.Blue;
							Console.Write("█");
						}
					}
					else {
						Console.ForegroundColor = ConsoleColor.White;
						Console.Write("░");
					}
				}
				Console.WriteLine();
			}
		}

		public bool IsTarget(Point point)
		{
			if (field[point.x, point.y] == 2)
			{
				return true;
			}
			return false;
		}
	}
}
