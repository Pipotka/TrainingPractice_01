using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANM_Task_04
{
	internal class Enemy
	{
		public const int EnemyDamage = 20;
		public int X { get; private set; }
		public int Y { get; private set; }
		public int PrevX { get; private set; }
		public int PrevY { get; private set; }

		public Enemy(int x, int y)
		{
			X = x;
			Y = y;
			SavePosition();
		}

		public void SavePosition()
		{
			PrevX = X;
			PrevY = Y;
		}

		public void TryMove(int dx, int dy, char[,] map, Player player)
		{
			int newX = X + dx;
			int newY = Y + dy;

			if (newX >= 0 && newX < map.GetLength(0) &&
				newY >= 0 && newY < map.GetLength(1) &&
				map[newX, newY] != '#')
			{
				// Проверяем столкновение с игроком при движении
				if (newX == player.X && newY == player.Y)
				{
					player.TakeDamage(EnemyDamage);
				}

				X = newX;
				Y = newY;
			}
		}

		public void ReturnToPrevPoss()
		{
			X = PrevX; 
			Y = PrevY;
		}
	}
}
