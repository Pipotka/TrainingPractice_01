using ANM_Task_02.ChessPieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ANM_Task_02
{
	internal class Program
	{
		private static int[,] _chessboard = new int[8, 8];

		static void Main(string[] args)
		{
			PrintChessboard();
			var nameChessPiecesDictionary = new Dictionary<string, IChessPiece>()
			{
				{"слон",  new Bishop()},
				{"ладья",  new Castle()},
				{"король",  new King()},
				{"конь",  new Knight()},
				{"ферзь",  new Queen()},
			};

			var numberChessPiecesDictionary = new Dictionary<Type, int>()
            {
                {typeof(Bishop), 1000},
                {typeof(Castle), 1001},
                {typeof(King), 1002},
                {typeof(Knight), 1003},
                {typeof(Queen), 1004},
            };

            Console.Write("Введите исходные данные: ");
			var parsedInput = Console.ReadLine().ToLower().Split(' ');
			var index = 0;
			var movingChessPiece = nameChessPiecesDictionary[parsedInput[index++]];
			movingChessPiece.Position = ChessPosition.FromTheChessAnnotation(parsedInput[index++]);
            var attackingChessPiece = nameChessPiecesDictionary[parsedInput[index++]];
            attackingChessPiece.Position = ChessPosition.FromTheChessAnnotation(parsedInput[index++]);
			var finalPosition = ChessPosition.FromTheChessAnnotation(parsedInput[index++]);
			_chessboard[movingChessPiece.Position.Row, movingChessPiece.Position.Column] = numberChessPiecesDictionary[movingChessPiece.GetType()];
            _chessboard[attackingChessPiece.Position.Row, attackingChessPiece.Position.Column] = numberChessPiecesDictionary[attackingChessPiece.GetType()];
			foreach (var position in attackingChessPiece.GetAttackedPositions(attackingChessPiece.Position))
			{
				_chessboard[position.Row, position.Column] -= 1;
            }

            if (FindThePath())
			{

			}
        }

		private static void PrintChessboard()
		{
			Console.Write("  ");
			for (var colNumber = 0; colNumber < _chessboard.GetLength(0); colNumber++)
			{
				Console.Write($"{ChessPosition.ColumnCharDictionary[colNumber]}");
			}
			Console.WriteLine();

			for (var row = 0; row < _chessboard.GetLength(0); row++)
			{
				Console.Write($"{row + 1} ");
				for (var col = 0; col < _chessboard.GetLength(1); col++)
				{
					// Отрисовка шахматной доски. Необходимо реализовать отрисовку для каждого вида фигур и пустых клеток
					switch (_chessboard[row, col])
					{
						case 0:
							Console.Write('П');
							break;
					}
				}
				Console.Write($" {row + 1}");
				Console.WriteLine();
			}
			Console.Write("  ");
			for (var colNumber = 0; colNumber < _chessboard.GetLength(0); colNumber++)
			{
				Console.Write($"{ChessPosition.ColumnCharDictionary[colNumber]}");
			}
			Console.WriteLine();
		}

        private static void ClearChessboard()
		{
			for (var row = 0; row < _chessboard.GetLength(0); row++)
			{
                for (var col = 0; col < _chessboard.GetLength(0); col++)
                {
					_chessboard[row, col] = 0;
                }
            }
		}

        private static bool FindThePath(ChessPosition currentPosition, ChessPosition finalPosition, IChessPiece chessPiece, Stack<ChessPosition> result)
		{
			//Рекурсивный случай
			foreach (var position in chessPiece.GetSteps(currentPosition)
				.Where(x => x.Difference(finalPosition) >= currentPosition.Difference(finalPosition)))
			{
                //Базовый случай
                if (RecursiveAlgorithm(position))
				{
					return true;
				}
                //Базовый случай
            }

            foreach (var position in chessPiece.GetSteps(currentPosition)
                .Where(x => x.Difference(finalPosition) < currentPosition.Difference(finalPosition)))
            {
                //Базовый случай
                if (RecursiveAlgorithm(position))
                {
                    return true;
                }
                //Базовый случай
            }
            //Рекурсивный случай

            return false;


            bool RecursiveAlgorithm(ChessPosition position)
			{
                //Базовый случай
                if (_chessboard[position.Row, position.Column] != 0)
                {
                    return false;
                }

                if (position.Equals(finalPosition))
                {
                    result.Push(position);
                    return true;
                }
                //Базовый случай

                //Рекурсивный случай
                _chessboard[position.Row, position.Column] += 1;
                result.Push(position);
                if (!FindThePath(position, finalPosition, chessPiece, result))
                {
                    result.Pop();
                    return false;
                }
                return true;
                //Рекурсивный случай
            }
        }
	}
}
