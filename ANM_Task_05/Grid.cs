using System;
using System.Drawing;
using System.Windows.Forms;

namespace ANM_Task_05
{
	internal class Grid
	{
        private static readonly Random random = new Random();

        /// <summary>
        /// Позиция начала верхнего левого региона
        /// </summary>
        public static GridPosition UpperLeftRegion { get; } = new GridPosition { Row = 0, Column = 0 };

		/// <summary>
		/// Позиция начала верхнего центрального региона
		/// </summary>
		public static GridPosition UpperCentralRegion { get; } = new GridPosition { Row = 0, Column = 3 };

		/// <summary>
		/// Позиция начала верхнего правого региона
		/// </summary>
		public static GridPosition UpperRightRegion { get; } = new GridPosition { Row = 0, Column = 6 };

		/// <summary>
		/// Позиция начала среднего левого региона
		/// </summary>
		public static GridPosition MiddleLeftRegion { get; } = new GridPosition { Row = 3, Column = 0 };

		/// <summary>
		/// Позиция начала среднего центрального региона
		/// </summary>
		public static GridPosition MiddleCentralRegion { get; } = new GridPosition { Row = 3, Column = 3 };

		/// <summary>
		/// Позиция начала среднего правого региона
		/// </summary>
		public static GridPosition MiddleRightRegion { get; } = new GridPosition { Row = 3, Column = 6 };

		/// <summary>
		/// Позиция начала нижнего левого региона
		/// </summary>
		public static GridPosition LowerLeftRegion { get; } = new GridPosition { Row = 6, Column = 0 };

		/// <summary>
		/// Позиция начала нижнего центрального региона
		/// </summary>
		public static GridPosition LowerCentralRegion { get; } = new GridPosition { Row = 6, Column = 3 };

		/// <summary>
		/// Позиция начала нижнего правого региона
		/// </summary>
		public static GridPosition LowerRightRegion { get; } = new GridPosition { Row = 6, Column = 6 };

		/// <summary>
		/// Игравое поле 9x9
		/// </summary>
		public int[,] PlayingField { get; private set; } = new int[9, 9];

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


    }
}
