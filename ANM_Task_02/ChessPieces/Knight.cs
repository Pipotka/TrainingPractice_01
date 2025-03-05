using System.Collections.Generic;

namespace ANM_Task_02.ChessPieces
{
	/// <summary>
	/// Конь
	/// </summary>
	internal class Knight : IChessPiece
	{
		public ChessPosition Position { get; set; }

		string IChessPiece.GetName() => "Конь";

        List<ChessPosition> IChessPiece.GetAttackedPositions(ChessPosition chessPiecePosition)
		{
			throw new System.NotImplementedException();
		}

		List<ChessPosition> IChessPiece.GetSteps(ChessPosition chessPiecePosition)
		{
			throw new System.NotImplementedException();
		}
	}
}
