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
        /// Возвращает позиции, которые фигура может атаковать
        /// </summary>
        /// <param name="position">позиция фигуры</param>
        ChessPosition[] GetAttackedPositions(ChessPosition position);

		/// <summary>
		/// Возвращает название фигуры
		/// </summary>
		string GetName();
	}
}
