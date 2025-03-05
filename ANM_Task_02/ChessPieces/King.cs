using System.Collections.Generic;

namespace ANM_Task_02.ChessPieces
{
	/// <summary>
	/// Король
	/// </summary>
	internal class King : IChessPiece
	{
		public ChessPosition Position { get; set; }

        List<ChessPosition> IChessPiece.GetAttackedPositions(ChessPosition chessPiecePosition)
		{
			throw new System.NotImplementedException();
		}

		string IChessPiece.GetName() => "Король";

		List<ChessPosition> IChessPiece.GetSteps(ChessPosition chessPiecePosition)
		{
			throw new System.NotImplementedException();
		}
	}
}
