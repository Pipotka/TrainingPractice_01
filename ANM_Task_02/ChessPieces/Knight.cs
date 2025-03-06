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

        ChessPosition[] IChessPiece.GetAttackedPositions(ChessPosition chessPiecePosition)
		{
			throw new System.NotImplementedException();
		}
	}
}
