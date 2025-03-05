using ANM_Task_02.ChessPieces;
using System;
using System.Collections.Generic;

namespace ANM_Task_02
{
	internal class Program
	{
		private static int[,] _chessboard = new int[8, 8];

		static void Main(string[] args)
		{
			PrintChessboard();
		}

		private static void PrintChessboard()
		{
			Console.Write("  ");
			for (var colNumber = 0; colNumber < _chessboard.GetLength(0); colNumber++)
			{
				Console.Write($"{ChessPosition.ColumnCharDictionary[colNumber]}");
			}
			Console.WriteLine();

			for (var i = 0; i < _chessboard.GetLength(0); i++)
			{
				Console.Write($"{i + 1} ");
				for (var j = 0; j < _chessboard.GetLength(1); j++)
				{
					// Отрисовка шахматной доски. Необходимо реализовать отрисовку для каждого вида фигур и пустых клеток
					switch (_chessboard[i, j])
					{
						case 0:
							Console.Write('П');
							break;
					}
				}
				Console.Write($" {i + 1}");
				Console.WriteLine();
			}
			Console.Write("  ");
			for (var colNumber = 0; colNumber < _chessboard.GetLength(0); colNumber++)
			{
				Console.Write($"{ChessPosition.ColumnCharDictionary[colNumber]}");
			}
			Console.WriteLine();
		}

		private bool FindThePath(ChessPosition currentPosition, ChessPosition finalPosition, IChessPiece chessPiece, Stack<ChessPosition> result)
		{
			//Рекурсивный случай
			foreach (var position in chessPiece.TakeTheNextStep(currentPosition))
			{
				if (_chessboard[position.Row, position.Column] != 0)
				{
					return false;
				}

				if (position.Equals(finalPosition))
				{
					result.Push(position);
					return true;
				}

				result.Push(position);
				if (!FindThePath(position, finalPosition, chessPiece, result))
				{
					result.Pop();
					return false;
				}
				return true;
			}
			return false;
			//Базовый случай

		}
	}
}
