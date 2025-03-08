using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANM_Task_04
{
	internal class Player
	{
		public int X { get; private set; }
		public int Y { get; private set; }
		public int PrevX { get; private set; }
		public int PrevY { get; private set; }
		public int Health { get; private set; } = 100;

		public Player(int x, int y)
		{
			X = x;
			Y = y;
			SavePosition();
		}

		public void TakeDamage(int amount)
		{
			Health = Math.Max(Health - amount, 0);
		}

		public void SavePosition()
		{
			PrevX = X;
			PrevY = Y;
		}

		public void TryMove(int dx, int dy, char[,] map, List<Enemy> enemies)
		{
			int newX = X + dx;
			int newY = Y + dy;

			if (newX >= 0 && newX < map.GetLength(0) &&
				newY >= 0 && newY < map.GetLength(1) &&
				map[newX, newY] != '#')
			{
				// Проверяем столкновение с врагами при движении
				foreach (var enemy in enemies)
				{
					if (enemy.X == newX && enemy.Y == newY)
					{
						TakeDamage(Enemy.EnemyDamage);
						break;
					}
				}

				X = newX;
				Y = newY;
			}
		}
	}
}
