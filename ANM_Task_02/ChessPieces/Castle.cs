using System.Collections.Generic;

namespace ANM_Task_02.ChessPieces
{
	/// <summary>
	/// Ладья
	/// </summary>
	internal class Castle : IChessPiece
	{
        public const int Number = 1001;

        public ChessPosition Position { get; set; }

		string IChessPiece.GetName() => "Ладья";

        ChessPosition[] IChessPiece.GetAttackedPositions(ChessPosition chessPiecePosition)
        {
            var result = new Queue<ChessPosition>();

            // Поля по горизонтали
            for (var col = 0; col < 8; col++)
            {
                if (col != chessPiecePosition.Column)
                {
                    result.Enqueue(new ChessPosition
                    {
                        Column = col,
                        Row = chessPiecePosition.Row
                    });
                }
            }

            // Поля по вертикали
            for (var row = 0; row < 8; row++)
            {
                if (row != chessPiecePosition.Row)
                {
                    result.Enqueue(new ChessPosition
                    {
                        Column = chessPiecePosition.Column,
                        Row = row
                    });
                }
            }

            return result.ToArray();
        }
    }
}
