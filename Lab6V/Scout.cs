using System;

namespace Lab6V
{
	class Scout
	{
		Random rand = new Random();
		int oneDirectionMoveCount = 0;

		public Point Location { get; set; }
		public ConsoleColor Color { get; }
		internal Point FinishPoint { get; set; }
		public int FoundTargets { get; set; } = 0;


		public Scout(Point location, ConsoleColor color)
		{
			Location = location;
			Color = color;
		}

		public void CalculateFinishPoit(int fieldWidth, int fieldHeight)
		{
			int x = 0, y = 0;
			if (Location.x < fieldHeight - Location.x)
			{
				x = fieldHeight - 1;
			}
			if (Location.y < fieldWidth - Location.y)
			{
				y = fieldWidth - 1;
			}
			FinishPoint = new Point(x, y);
		}

		public bool Move()
		{
			if (Location.x == FinishPoint.x && Location.y == FinishPoint.y)
			{
				return false;
			}
			if (Location.x == FinishPoint.x)
			{
				HorisontalMove();
			}
			else if (Location.y == FinishPoint.y)
			{
				VerticallMove();
			}
			else if (Math.Abs(Location.y - FinishPoint.y) > Math.Abs(Location.x - FinishPoint.x))
			{
				if (oneDirectionMoveCount == 4)
				{
					VerticallMove();
					oneDirectionMoveCount = 0;

				}
				else
				{
					HorisontalMove();
					oneDirectionMoveCount++;
				}
			}
			else
			{
				if (oneDirectionMoveCount == 4)
				{
					HorisontalMove();
					oneDirectionMoveCount = 0;
				} else
				{
					VerticallMove();
					oneDirectionMoveCount++;
				}

			}
			return true;
		}

		void HorisontalMove()
		{
			if (Location.y < FinishPoint.y)
			{
				Location.y += 1;
			}
			else
			{
				Location.y -= 1;

			}
		}

		void VerticallMove()
		{
			if (Location.x < FinishPoint.x)
			{
				Location.x += 1;
			}
			else
			{
				Location.x -= 1;

			}
		}

		void RandomMove()
		{
			if (rand.Next(2) == 0)
			{
				HorisontalMove();
			}
			else
			{
				VerticallMove();
			}
		}
	}
}
