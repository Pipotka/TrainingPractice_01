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

        ChessPosition[] IChessPiece.GetAttackedPositions(ChessPosition chessPiecePosition)
		{
            var result = new Queue<ChessPosition>();

            // Поля сверху
            for (int col = chessPiecePosition.Column - 1,
                row = chessPiecePosition.Row - 1;
                col >= 0 && col < 8 && row >= 0; col++)
            {
                result.Enqueue(new ChessPosition
                {
                    Column = col,
                    Row = row,
                });
            }

            // Поля снизу не закончено
            for (int col = chessPiecePosition.Column + 1,
                row = chessPiecePosition.Row + 1;
                col >= 0 && col < 8 && row < 8; col--)
            {
                result.Enqueue(new ChessPosition
                {
                    Column = col,
                    Row = row,
                });
            }

            // Поля справа
            for (int col = chessPiecePosition.Column + 1,
                row = chessPiecePosition.Row + 1;
                col >= 0 && col < 8 && row < 8; col--)
            {
                result.Enqueue(new ChessPosition
                {
                    Column = col,
                    Row = row,
                });
            }

            return result.ToArray();
        }

		string IChessPiece.GetName() => "Король";
	}
}
