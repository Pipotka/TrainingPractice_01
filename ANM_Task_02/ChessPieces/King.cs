using System.Collections.Generic;

namespace ANM_Task_02.ChessPieces
{
	/// <summary>
	/// Король
	/// </summary>
	internal class King : IChessPiece
	{
        public const int Number = 1002;

        public ChessPosition Position { get; set; }

        ChessPosition[] IChessPiece.GetAttackedPositions(ChessPosition chessPiecePosition, int[,] chessboard)
		{
            var result = new Queue<ChessPosition>();

			// Перебираем все возможные направления движения короля
			for (int rowDelta = -1; rowDelta <= 1; rowDelta++)
			{
				for (int colDelta = -1; colDelta <= 1; colDelta++)
				{
					// Пропускаем текущую позицию (нулевое смещение)
					if (rowDelta == 0 && colDelta == 0)
						continue;

					int newRow = chessPiecePosition.Row + rowDelta;
					int newCol = chessPiecePosition.Column + colDelta;

					// Проверяем, что позиция находится в пределах доски
					if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
					{
						result.Enqueue(new ChessPosition
						{
							Row = newRow,
							Column = newCol
						});
					}
				}
			}

			return result.ToArray();
		}

		string IChessPiece.GetName() => "Король";
	}
}
