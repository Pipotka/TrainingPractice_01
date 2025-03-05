using System.Collections.Generic;

namespace ANM_Task_02
{
	internal interface IChessPiece
	{
		/// <summary>
		/// Текущая позиция фигуры
		/// </summary>
		ChessPosition Position { get; set; }

		/// <summary>
		/// Возвращает позиции, на которые фигура может пойти
		/// </summary>
		/// <param name="position">позиция фигуры</param>
		List<ChessPosition> TakeTheNextStep(ChessPosition position);

		/// <summary>
		/// Возвращает позиции, которые фигура может атаковать
		/// </summary>
		/// <param name="chessPiecePosition"></param>
		/// <returns></returns>
		Queue<ChessPosition> GetAttackedPositions(ChessPosition chessPiecePosition);

		/// <summary>
		/// Возвращает название фигуры
		/// </summary>
		string GetName();
	}
}
