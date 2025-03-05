using System.Collections.Generic;

namespace ANM_Task_02.ChessPieces
{
	/// <summary>
	/// Ферзь
	/// </summary>
	internal class Queen : IChessPiece
	{
		public ChessPosition Position { get; set; }

		string IChessPiece.GetName() => "Ферзь";

		Queue<ChessPosition> IChessPiece.GetAttackedPositions(ChessPosition chessPiecePosition)
		{
			throw new System.NotImplementedException();
		}

		List<ChessPosition> IChessPiece.TakeTheNextStep(ChessPosition chessPiecePosition)
		{
			throw new System.NotImplementedException();
		}
	}
}
