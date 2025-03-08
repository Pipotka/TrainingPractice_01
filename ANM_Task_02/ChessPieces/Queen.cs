using System.Collections.Generic;

namespace ANM_Task_02.ChessPieces
{
	/// <summary>
	/// Ферзь
	/// </summary>
	internal class Queen : IChessPiece
	{
        public const int Number = 1004;

        public ChessPosition Position { get; set; }

		string IChessPiece.GetName() => "Ферзь";

        ChessPosition[] IChessPiece.GetAttackedPositions(ChessPosition chessPiecePosition, int[,] chessboard)
		{
			var result = new Queue<ChessPosition>();

			// Все 8 направлений движения ферзя
			int[,] directions = new int[,]
			{
				{ -1,  0 }, // Вверх
				{  1,  0 }, // Вниз
				{  0, -1 }, // Влево
				{  0,  1 }, // Вправо
				{ -1, -1 }, // Вверх-влево
				{ -1,  1 }, // Вверх-вправо
				{  1, -1 }, // Вниз-влево
				{  1,  1 }  // Вниз-вправо
			};

			// Перебираем все направления
			for (int i = 0; i < directions.GetLength(0); i++)
			{
				int rowStep = directions[i, 0];
				int colStep = directions[i, 1];

				int currentRow = chessPiecePosition.Row + rowStep;
				int currentCol = chessPiecePosition.Column + colStep;

				// Двигаемся по направлению до края доски или препятствия
				while (currentRow >= 0 && currentRow < 8 && currentCol >= 0 && currentCol < 8)
				{
					int cellValue = chessboard[currentRow, currentCol];

					// Если встретили фигуру (cellValue > 0) - останавливаемся
					if (cellValue > 0)
						break;

					// Добавляем позицию (включая клетки с отрицательными значениями)
					result.Enqueue(new ChessPosition
					{
						Row = currentRow,
						Column = currentCol
					});

					// Переход к следующей клетке
					currentRow += rowStep;
					currentCol += colStep;
				}
			}

			return result.ToArray();
		}
	}
}
