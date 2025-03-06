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

        ChessPosition[] IChessPiece.GetAttackedPositions(ChessPosition chessPiecePosition)
		{
			var result = new Queue<ChessPosition>();

			// Поля по главной диагонали
			var startMainPoss = new ChessPosition
			{
				Column = chessPiecePosition.Column,
				Row = chessPiecePosition.Row,
            };

			while (startMainPoss.Column > 0
				&& startMainPoss.Row > 0)
			{
                startMainPoss.Row--;
				startMainPoss.Column--;
            }

			for (int row = startMainPoss.Row,
                col = startMainPoss.Column;
                row < 8 && col < 8; row++, col++)
			{
				if (row != chessPiecePosition.Row)
				{
                    result.Enqueue(new ChessPosition
                    {
                        Column = col,
                        Row = row
                    });
                }
			}

            // Поля побочной диагонали
            var startSidePoss = new ChessPosition
            {
                Column = chessPiecePosition.Column,
                Row = chessPiecePosition.Row,
            };

            while (startSidePoss.Column < 8
                && startSidePoss.Row > 0)
            {
                startSidePoss.Row--;
                startSidePoss.Column++;
            }

            for (int col = startSidePoss.Column, row = startSidePoss.Row; col >= 0 && row < 8; col--, row++)
			{
				if (row != chessPiecePosition.Row)
				{
                    result.Enqueue(new ChessPosition
                    {
                        Column = col,
                        Row = row
                    });
                }
			}

			return result.ToArray();
        }
    }
}
