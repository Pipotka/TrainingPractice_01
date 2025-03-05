using System.Collections.Generic;

namespace ANM_Task_02.ChessPieces
{
	/// <summary>
	/// Ладья
	/// </summary>
	internal class Castle : IChessPiece
	{
		public ChessPosition Position { get; set; }

		Queue<ChessPosition> IChessPiece.GetAttackedPositions(ChessPosition chessPiecePosition)
		{
			throw new System.NotImplementedException();
		}

		string IChessPiece.GetName() => "Ладья";

		List<ChessPosition> IChessPiece.TakeTheNextStep(ChessPosition chessPiecePosition)
		{
			throw new System.NotImplementedException();
		}
	}
}
