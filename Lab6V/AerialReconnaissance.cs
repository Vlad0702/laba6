using System;
using System.Threading;

namespace Lab6V
{
	class AerialReconnaissance
	{
		private Scout[] scouts;
		private int threadsAmount;
		Random rand = new Random();
		private Point[] busyPoints;
		public Field Field { get; set; }
		object locker = new object();
		Thread[] threads;
		public int ThreadsAmount
		{
			get
			{
				return threadsAmount;
			}
			set
			{
				if (value > 5 || value < 1)
				{
					throw new Exception("Минимум 1 разведчик. Максимум - 5");
				}
				threadsAmount = value;
			}
		}

		public int ThreadSpead { get; set; }
		public ConsoleColor[] Colors { get; set; } = new ConsoleColor[5] {
			ConsoleColor.Blue,
			ConsoleColor.Green,
			ConsoleColor.Yellow,
			ConsoleColor.Cyan,
			ConsoleColor.Magenta
		};


		public AerialReconnaissance(Field field, int threadsAmount, int threadSpead)
		{
			Field = field;
			ThreadsAmount = threadsAmount;
			ThreadSpead = threadSpead;
			Init();
		}

		public void Init()
		{
			scouts = new Scout[ThreadsAmount];
			threads = new Thread[ThreadsAmount];
			busyPoints = new Point[ThreadsAmount];
			SetScouts();
		}

		void SetScouts()
		{
			int count = 0;
			while (count < ThreadsAmount)
			{
				int x = rand.Next(Field.Height - 2) + 1;
				int y = rand.Next(Field.Width - 2) + 1;
				Point point = new Point(x, y);
				if (IsBusyPoint(point))
				{
					continue;
				}
				busyPoints[count] = point;
				scouts[count] = new Scout(point, Colors[count]);
				scouts[count].CalculateFinishPoit(Field.Width, Field.Height);
				count++;
			}
			DrawScouts();
		}


		void DrawScouts()
		{

			for (int i = 0; i < ThreadsAmount; i++)
			{
				if (Field.IsTarget(scouts[i].Location))
				{
					WriteAt("X", scouts[i].Location, scouts[i].Color);
				}
				else
				{
					WriteAt("█", scouts[i].Location, scouts[i].Color);

				}
			}
		}

		public void Start()
		{
			for (int i = 0; i < ThreadsAmount; i++)
			{
				Thread myThread1 = new Thread(new ParameterizedThreadStart(Scouting));
				threads[i] = myThread1;
				myThread1.Start(scouts[i]);
			}

		}

		public void KillThreads()
		{
			for (int i = 0; i < ThreadsAmount; i++)
			{
				if (threads[i] != null)
				{
					threads[i].Abort();
				}
			}

		}

		void Scouting(object scout)
		{
			Scout scout1 = (Scout)scout;
			int count = 0;
			while (scout1.Move())
			{
				if (Field.IsTarget(scout1.Location))
				{
					scout1.FoundTargets += 1;
				}
				DrawScouts();
				count++;
				Thread.Sleep(ThreadSpead);
				PrintFoundTargets();
			}
		}

		void PrintFoundTargets()
		{
			lock (locker)
			{
				Console.CursorVisible = false;
				Console.ForegroundColor = ConsoleColor.White;
				Console.SetCursorPosition(0, Field.Height);
				for (int i = 0; i < ThreadsAmount; i++)
				{
					Console.WriteLine(scouts[i].Color + ": " + scouts[i].FoundTargets);
				}
			}
		}

		bool IsAllThreadsDead()
		{
			lock (locker)
			{
				bool result = true;
				for (int i = 0; i < ThreadsAmount; i++)
				{
					if (threads[i] != null && threads[i].IsAlive)
					{
						result = false;
					}
				}
				return result;
			}
		}

		bool IsBusyPoint(Point point)
		{
			for (int i = 0; i < busyPoints.Length; i++)
			{
				if (busyPoints[i] != null && busyPoints[i].x == point.x && busyPoints[i].y == point.y)
				{
					return true;
				}
			}

			return false;
		}

		void WriteAt(string s, Point point, ConsoleColor color)
		{
			lock (locker)
			{
				Console.ForegroundColor = color;
				Console.SetCursorPosition(point.y, point.x + 1);
				Console.Write("\b");
				Console.SetCursorPosition(point.y, point.x);
				Console.Write(s);
			}
		}
	}
}
