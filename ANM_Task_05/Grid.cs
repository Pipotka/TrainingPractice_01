using System.Drawing;
using System.Windows.Forms;

namespace ANM_Task_05
{
	internal class Grid
	{
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
		public int[,] PlayingField { get; } = new int[9, 9];

		public Grid()
		{
			for (var row = 0; row < PlayingField.GetLength(0); row++)
			{
				for (var col = 0; col < PlayingField.GetLength(1); col++)
				{

				}
			}
		}
	}
}
