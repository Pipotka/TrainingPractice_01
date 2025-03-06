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

        ChessPosition[] IChessPiece.GetAttackedPositions(ChessPosition chessPiecePosition)
		{
			throw new System.NotImplementedException();
		}
	}
}
