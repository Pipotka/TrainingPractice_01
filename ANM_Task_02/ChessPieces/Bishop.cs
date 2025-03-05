using System.Collections.Generic;

namespace ANM_Task_02.ChessPieces
{
	/// <summary>
	/// Слон
	/// </summary>
	internal class Bishop : IChessPiece
	{
		public ChessPosition Position { get; set; }

		string IChessPiece.GetName() => "Слон";

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
