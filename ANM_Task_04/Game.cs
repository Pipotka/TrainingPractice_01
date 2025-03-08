using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ANM_Task_04
{
	internal class Game
	{
		private char[,] map;
		private Player player;
		private List<Enemy> enemies = new List<Enemy>();
		private (int x, int y) exit;
		private bool showPath;
		private List<(int x, int y)> path = new List<(int, int)>();
		private List<(int x, int y)> redrawAreas = new List<(int, int)>();
		private Random random = new Random();

		public Game(string mapPath, int enemyCount)
		{
			LoadMap(mapPath);
			SpawnEnemies(enemyCount);
			FindPath();
		}

		private void LoadMap(string path)
		{
			if (!File.Exists(path))
				throw new Exception("Файл не найден");

			string[] lines = File.ReadAllLines(path);
			if (lines.Length == 0 || lines[0].Length == 0)
				throw new Exception("Неверный формат карты");

			int width = lines[0].Length;
			int height = lines.Length;
			map = new char[width, height];

			bool hasPlayer = false;
			bool hasExit = false;

			for (int y = 0; y < height; y++)
			{
				if (lines[y].Length != width)
					throw new Exception("Карта не прямоугольная");

				for (int x = 0; x < width; x++)
				{
					map[x, y] = lines[y][x];

					if (map[x, y] == '@')
					{
						if (hasPlayer) throw new Exception("Несколько игроков");
						player = new Player(x, y);
						hasPlayer = true;
					}
					else if (map[x, y] == 'e')
					{
						exit = (x, y);
						hasExit = true;
					}
				}
			}

			if (!hasPlayer || !hasExit)
				throw new Exception("Отсутствует игрок или выход");
		}

		private void SpawnEnemies(int count)
		{
			for (int i = 0; i < count; i++)
			{
				int x, y;
				do
				{
					x = random.Next(0, map.GetLength(0));
					y = random.Next(0, map.GetLength(1));
				} while (map[x, y] != ' ' ||
						(x == player.X && y == player.Y) ||
						(x == exit.x && y == exit.y));

				enemies.Add(new Enemy(x, y));
			}
		}

		public void Run()
		{
			Console.CursorVisible = false;

			InitialDraw();

			while (true)
			{
				Update();
				Draw();

				// Проверка условий завершения
				if (player.X == exit.x && player.Y == exit.y)
				{
					ShowGameOverScreen("ПОБЕДА! Вы достигли выхода!");
					return;
				}

				if (player.Health <= 0)
				{
					ShowGameOverScreen("ПОРАЖЕНИЕ! Ваши жизни закончились!");
					return;
				}

				Thread.Sleep(100);
			}
		}

		private void ShowGameOverScreen(string message)
		{
			Console.Clear();
			Console.WriteLine(message);
			Console.WriteLine("Нажмите любую клавишу для выхода...");
			Console.ReadKey();
		}

		private void InitialDraw()
		{
			// Полная отрисовка всей карты
			for (int y = 0; y < map.GetLength(1); y++)
			{
				for (int x = 0; x < map.GetLength(0); x++)
				{
					Console.SetCursorPosition(x, y);
					DrawStaticTile(x, y); // Отрисовка только статичных элементов
				}
			}

			// Отрисовка динамических объектов
			DrawDynamicElements();
			DrawHealthBar();
		}

		private void DrawStaticTile(int x, int y)
		{
			if (map[x, y] == '#')
			{
				Console.ForegroundColor = ConsoleColor.DarkGray;
				Console.Write('▓'); // Используем псевдографику для стен
			}
			else if (map[x, y] == 'e')
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.Write('▼');
			}
			else
			{
				Console.Write(' ');
			}
			Console.ResetColor();
		}

		private void DrawDynamicElements()
		{
			// Игрок
			Console.SetCursorPosition(player.X, player.Y);
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write('@');

			// Враги
			foreach (var enemy in enemies)
			{
				Console.SetCursorPosition(enemy.X, enemy.Y);
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write('F');
			}

			Console.ResetColor();
		}


		private void Update()
		{
			redrawAreas.Clear();

			// Обновляем путь при каждом движении игрока
			if (showPath)
			{
				path.ForEach(p => redrawAreas.Add(p)); // Помечаем старый путь
				FindPath();
				path.ForEach(p => redrawAreas.Add(p)); // Помечаем новый путь
			}

			// Сохраняем предыдущие позиции
			player.SavePosition();
			foreach (var e in enemies) e.SavePosition();

			HandleInput();
			MoveEnemies();

			// Добавляем области для перерисовки
			redrawAreas.Add((player.PrevX, player.PrevY));
			redrawAreas.Add((player.X, player.Y));

			foreach (var e in enemies)
			{
				redrawAreas.Add((e.PrevX, e.PrevY));
				redrawAreas.Add((e.X, e.Y));
			}
		}

		private void HandleInput()
		{
			if (!Console.KeyAvailable) return;

			var key = Console.ReadKey(true).Key;
			switch (key)
			{
				case ConsoleKey.W:
					player.TryMove(0, -1, map, enemies);
					break;
				case ConsoleKey.S:
					player.TryMove(0, 1, map, enemies);
					break;
				case ConsoleKey.A:
					player.TryMove(-1, 0, map, enemies);
					break;
				case ConsoleKey.D:
					player.TryMove(1, 0, map, enemies);
					break;
				case ConsoleKey.P:
					showPath = !showPath;
					if (showPath) FindPath();
					break;
			}
		}

		private void MoveEnemies()
		{
			foreach (var enemy in enemies)
			{
				enemy.TryMove(random.Next(-1, 2),
							random.Next(-1, 2),
							map, player);

				// Проверяем столкновение с игроком
				if (enemy.X == player.X && enemy.Y == player.Y)
				{
					player.TakeDamage(Enemy.EnemyDamage);
					enemy.ReturnToPrevPoss();
				}
			}
		}

		private void FindPath()
		{
			path.Clear();
			int width = map.GetLength(0);
			int height = map.GetLength(1);

			int[,] distance = new int[width, height];
			Queue<(int x, int y)> queue = new Queue<(int, int)>();

			// Инициализация
			for (int y = 0; y < height; y++)
				for (int x = 0; x < width; x++)
					distance[x, y] = -1;

			distance[player.X, player.Y] = 0;
			queue.Enqueue((player.X, player.Y));

			// Распространение волны
			while (queue.Count > 0)
			{
				var (x, y) = queue.Dequeue();

				// Останавливаемся при достижении выхода
				if (x == exit.x && y == exit.y) break;

				foreach (var dir in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
				{
					int nx = x + dir.Item1;
					int ny = y + dir.Item2;

					if (nx >= 0 && nx < width &&
						ny >= 0 && ny < height &&
						map[nx, ny] != '#' &&
						distance[nx, ny] == -1)
					{
						distance[nx, ny] = distance[x, y] + 1;
						queue.Enqueue((nx, ny));
					}
				}
			}

			// Восстановление пути только если выход достижим
			if (distance[exit.x, exit.y] == -1) return;

			int cx = exit.x;
			int cy = exit.y;
			while (cx != player.X || cy != player.Y)
			{
				path.Add((cx, cy));
				foreach (var dir in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
				{
					int nx = cx + dir.Item1;
					int ny = cy + dir.Item2;

					if (nx >= 0 && nx < width &&
						ny >= 0 && ny < height &&
						distance[nx, ny] == distance[cx, cy] - 1)
					{
						cx = nx;
						cy = ny;
						break;
					}
				}
			}
		}

		private void Draw()
		{
			// Отрисовка измененных клеток
			foreach (var (x, y) in redrawAreas)
			{
				if (x < 0 || x >= map.GetLength(0) ||
					y < 0 || y >= map.GetLength(1)) continue;

				Console.SetCursorPosition(x, y);
				DrawTile(x, y);
			}

			// Отрисовка здоровья
			DrawHealthBar();
		}

		private void DrawTile(int x, int y)
		{
			// Приоритеты отрисовки
			if (x == player.X && y == player.Y)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write('@');
				Console.ResetColor();
				return;
			}

			foreach (var enemy in enemies)
			{
				if (enemy.X == x && enemy.Y == y)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write('F');
					Console.ResetColor();
					return;
				}
			}

			// 2. Путь (после врагов, но до статичных объектов)
			if (showPath && path.Contains((x, y)))
			{
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.Write('·');
				Console.ResetColor();
				return;
			}

			if (x == exit.x && y == exit.y)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.Write('e');
				Console.ResetColor();
				return;
			}

			Console.Write(map[x, y] == '#' ? '#' : ' ');
		}

		private void DrawHealthBar()
		{
			const int BarLength = 20;
			int barPositionY = map.GetLength(1) + 2;

			// Очищаем всю строку
			Console.SetCursorPosition(0, barPositionY);
			Console.Write(new string(' ', Console.WindowWidth));

			// Устанавливаем курсор в начало строки
			Console.SetCursorPosition(0, barPositionY);

			// Рассчитываем заполненную часть
			float percent = player.Health / 100f;
			int filled = (int)(BarLength * percent);

			// Выбираем цвет в зависимости от здоровья
			if (percent < 0.2f)
				Console.ForegroundColor = ConsoleColor.Red;
			else if (percent < 0.5f)
				Console.ForegroundColor = ConsoleColor.Yellow;
			else
				Console.ForegroundColor = ConsoleColor.Green;

			// Рисуем полосу
			Console.Write("HP: [");
			Console.Write(new string('█', filled));
			Console.ResetColor();
			Console.Write(new string('─', BarLength - filled));

			// Выводим проценты с фиксированной шириной
			Console.Write($"] {player.Health,3}% ");
		}
	}
}
