using System.Collections.Generic;

namespace ANM_Task_02.ChessPieces
{
	/// <summary>
	/// Конь
	/// </summary>
	internal class Knight : IChessPiece
	{
        public const int Number = 1003;

        public ChessPosition Position { get; set; }

		string IChessPiece.GetName() => "Конь";

        ChessPosition[] IChessPiece.GetAttackedPositions(ChessPosition chessPiecePosition, int[,] chessboard)
		{
			var result = new Queue<ChessPosition>();

			// Все возможные комбинации ходов коня (8 вариантов)
			int[,] knightMoves = new int[,]
			{
				{2, 1}, {2, -1}, {-2, 1}, {-2, -1},
				{1, 2}, {1, -2}, {-1, 2}, {-1, -2}
			};

			// Перебираем все возможные ходы
			for (int i = 0; i < knightMoves.GetLength(0); i++)
			{
				int deltaRow = knightMoves[i, 0];
				int deltaCol = knightMoves[i, 1];

				// Вычисляем новую позицию
				int newRow = chessPiecePosition.Row + deltaRow;
				int newCol = chessPiecePosition.Column + deltaCol;

				// Проверяем, что позиция в пределах доски
				if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
				{
					result.Enqueue(new ChessPosition
					{
						Row = newRow,
						Column = newCol
					});
				}
			}

			return result.ToArray();
		}
	}
}
