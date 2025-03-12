using System;
using System.Collections.Generic;
using System.IO;

namespace ANM_Task_05
{
	internal class Grid
	{
        private static readonly Random random = new Random();
		private const int N = 3;
		private const int Size = 9;

		/// <summary>
		/// Игравое поле 9x9
		/// </summary>
		public int[,] PlayingField { get; set; } = new int[9, 9];

		public void GenerateBaseGrid()
		{
            int n = 3; // Размер подквадрата (3x3)
            int size = n * n; // Размер таблицы (9x9)

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // Формула для генерации чисел от 1 до 9
                    PlayingField[i, j] = (i * n + i / n + j) % size + 1;
                }
            }
        }

		public static Grid FromFile(string fileName)
		{
			var grid = new int[9, 9];
			var lines = File.ReadAllLines(fileName);
			if (lines.Length != 9)
			{
				throw new Exception("Размер поля должен быть 9 на 9");
			}
			for (var i = 0; i < lines.Length; i++)
			{
				if (lines[i].Length != 9)
				{
                    throw new Exception("Размер поля должен быть 9 на 9");
                }

				for (var j = 0; j < lines[i].Length; j++)
				{
					if (!char.IsDigit(lines[i][j]))
					{
                        throw new Exception("Поле должно содержать только цифры");
                    }
					grid[i, j] = int.Parse(lines[i][j].ToString());
				}
			}
			return new Grid {PlayingField = grid };
		}

        /// <summary>
        /// Транспонирование таблицы (строки становятся столбцами, а столбцы — строками)
        /// </summary>
        private void Transpose()
        {
            int size = PlayingField.GetLength(0);
            int[,] transposed = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    transposed[j, i] = PlayingField[i, j];
                }
            }

            PlayingField = transposed;
        }

        /// <summary>
        /// Обмен двух строк в пределах одного района
        /// </summary>
        private void SwapRowsSmall()
        {
            int n = 3; // Размер подквадрата (3x3)
            int area = random.Next(0, n); // Случайный выбор района (0, 1 или 2)
            int line1 = random.Next(0, n); // Случайный выбор первой строки в районе
            int N1 = area * n + line1; // Номер первой строки для обмена

            int line2 = random.Next(0, n); // Случайный выбор второй строки в районе
            while (line1 == line2)
            {
                line2 = random.Next(0, n); // Убедимся, что строки разные
            }
            int N2 = area * n + line2; // Номер второй строки для обмена

            // Обмен строк
            for (int j = 0; j < PlayingField.GetLength(1); j++)
            {
                int temp = PlayingField[N1, j];
                PlayingField[N1, j] = PlayingField[N2, j];
                PlayingField[N2, j] = temp;
            }
        }

        /// <summary>
        /// Обмен двух столбцов в пределах одного района
        /// </summary>
        private void SwapColumnsSmall()
        {
            Transpose(); // Транспонируем таблицу (столбцы становятся строками)
            SwapRowsSmall(); // Обмениваем строки (бывшие столбцы)
            Transpose(); // Транспонируем обратно (строки снова становятся столбцами)
        }

        /// <summary>
        /// Обмен двух районов по горизонтали
        /// </summary>
        private void SwapRowsArea()
        {
            int n = 3; // Размер подквадрата (3x3)
            int area1 = random.Next(0, n); // Случайный выбор первого района
            int area2 = random.Next(0, n); // Случайный выбор второго района

            // Убедимся, что районы разные
            while (area1 == area2)
            {
                area2 = random.Next(0, n);
            }

            // Обмен всех строк между двумя районами
            for (int i = 0; i < n; i++)
            {
                int N1 = area1 * n + i; // Номер строки в первом районе
                int N2 = area2 * n + i; // Номер строки во втором районе

                // Обмен строк
                for (int j = 0; j < PlayingField.GetLength(1); j++)
                {
                    int temp = PlayingField[N1, j];
                    PlayingField[N1, j] = PlayingField[N2, j];
                    PlayingField[N2, j] = temp;
                }
            }
        }

		/// <summary>
		/// Обмен двух районов по вертикали
		/// </summary>
		private void SwapColumnsArea()
		{
			Transpose(); // Транспонируем таблицу (столбцы становятся строками)
			SwapRowsArea(); // Обмениваем районы по горизонтали (бывшие столбцы)
			Transpose(); // Транспонируем обратно (строки снова становятся столбцами)
		}

		/// <summary>
		/// Случайное перемешивание таблицы
		/// </summary>
		/// <param name="amt">Количество перемешиваний</param>
		public void Mix(int amt = 10)
		{
			// Список функций для перемешивания
			var mixFunctions = new List<Action>
		    {
			    Transpose,
			    SwapRowsSmall,
			    SwapColumnsSmall,
			    SwapRowsArea,
			    SwapColumnsArea
		    };

			// Случайное применение функций перемешивания
			for (int i = 0; i < amt; i++)
			{
				int idFunc = random.Next(0, mixFunctions.Count); // Случайный выбор функции
				mixFunctions[idFunc](); // Вызов выбранной функции
			}
		}

		/// <summary>
		/// Подготовка таблицы к игре в зависимости от уровня сложности
		/// </summary>
		/// <param name="difficulty">Уровень сложности</param>
		public void PrepareToPlay(DifficultyLevel difficulty)
		{
			var targetClues = 30;

			switch (difficulty)
			{
				case DifficultyLevel.Easy:
					targetClues = (int)(Size * Size * 0.45);
					break;

				case DifficultyLevel.Medium:
					targetClues = (int)(Size * Size * 0.35);
					break;

				case DifficultyLevel.Hard:
					targetClues = (int)(Size * Size * 0.25);
					break;
			}
			int maxIterations = 5; // Максимум 5 проходов по всем ячейкам
			int removed = 0;

			for (int iter = 0; iter < maxIterations; iter++)
			{
				var candidates = GenerateShuffledCells();
				bool changed = false;

				foreach (var (i, j) in candidates)
				{
					if (CountClues() <= targetClues) break;
					if (PlayingField[i, j] == 0) continue;

					int original = PlayingField[i, j];
					PlayingField[i, j] = 0;

					if (CountSolutionsFast() == 1)
					{
						removed++;
						changed = true;
					}
					else
					{
						PlayingField[i, j] = original;
					}
				}

				if (!changed) break; // Выход если не было изменений
			}
		}

		private List<(int i, int j)> GenerateShuffledCells()
		{
			var cells = new List<(int, int)>();
			for (int i = 0; i < Size; i++)
				for (int j = 0; j < Size; j++)
					cells.Add((i, j));

			// Фишер-Йейтс shuffle
			for (int i = cells.Count - 1; i > 0; i--)
			{
				int j = random.Next(i + 1);
				(cells[i], cells[j]) = (cells[j], cells[i]);
			}
			return cells;
		}

		private int CountClues()
		{
			int count = 0;
			for (int i = 0; i < Size; i++)
				for (int j = 0; j < Size; j++)
					if (PlayingField[i, j] != 0) count++;
			return count;
		}

		private int CountSolutionsFast()
		{
			int[,] tempGrid = (int[,])PlayingField.Clone();
			int solutionCount = 0;
			SolveRecursive(tempGrid, ref solutionCount);
			return solutionCount;
		}

		private bool SolveRecursive(int[,] grid, ref int count)
		{
			for (int row = 0; row < Size; row++)
			{
				for (int col = 0; col < Size; col++)
				{
					if (grid[row, col] != 0) continue;

					Span<int> available = stackalloc int[Size];
					GetAvailableNumbers(grid, row, col, available);

					foreach (int num in available)
					{
						if (num == 0) break;

						if (IsValidPlacement(grid, row, col, num))
						{
							grid[row, col] = num;
							if (SolveRecursive(grid, ref count))
							{
								// Обнаружено более одного решения
								grid[row, col] = 0;
								return true;
							}
							grid[row, col] = 0;
						}
					}
					return false;
				}
			}
			count++;
			return count > 1;
		}

		private void GetAvailableNumbers(int[,] grid, int row, int col, Span<int> buffer)
		{
			bool[] used = new bool[Size + 1];

			// Проверяем строку и столбец
			for (int i = 0; i < Size; i++)
			{
				used[grid[row, i]] = true;
				used[grid[i, col]] = true;
			}

			// Проверяем регион 3x3
			int startRow = row / N * N;
			int startCol = col / N * N;
			for (int i = 0; i < N; i++)
				for (int j = 0; j < N; j++)
					used[grid[startRow + i, startCol + j]] = true;

			int index = 0;
			for (int num = 1; num <= Size; num++)
				if (!used[num]) buffer[index++] = num;

			// Заполняем оставшиеся нулями
			while (index < Size) buffer[index++] = 0;
		}

		private bool IsValidPlacement(int[,] grid, int row, int col, int num)
		{
			// Проверка строки и столбца
			for (int i = 0; i < Size; i++)
			{
				if (grid[row, i] == num) return false;
				if (grid[i, col] == num) return false;
			}

			// Проверка региона 3x3
			int startRow = row / N * N;
			int startCol = col / N * N;
			for (int i = 0; i < N; i++)
				for (int j = 0; j < N; j++)
					if (grid[startRow + i, startCol + j] == num)
						return false;

			return true;
		}

		
	}
}
