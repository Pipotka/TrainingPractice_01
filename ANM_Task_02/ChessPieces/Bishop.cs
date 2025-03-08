using System.Collections.Generic;

namespace ANM_Task_02.ChessPieces
{
	/// <summary>
	/// Слон
	/// </summary>
	internal class Bishop : IChessPiece
	{
		public const int Number = 1000;

		public ChessPosition Position { get; set; }

		string IChessPiece.GetName() => "Слон";

        ChessPosition[] IChessPiece.GetAttackedPositions(ChessPosition chessPiecePosition, int[,] chessboard)
		{
			var result = new Queue<ChessPosition>();

			// Направления движения слона (4 диагонали)
			int[,] directions = new int[,]
			{
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

				// Двигаемся по направлению, пока не выйдем за пределы доски
				while (currentRow >= 0 && currentRow < 8 && currentCol >= 0 && currentCol < 8)
				{
					// Проверяем значение клетки
					int cellValue = chessboard[currentRow, currentCol];

					// Если клетка занята фигурой (положительное значение) - останавливаемся
					if (cellValue > 0)
						break;

					// Добавляем позицию в результат (0 или отрицательное значение)
					result.Enqueue(new ChessPosition
					{
						Row = currentRow,
						Column = currentCol
					});

					// Переходим к следующей клетке
					currentRow += rowStep;
					currentCol += colStep;
				}
			}

			return result.ToArray();
        }
    }
}
