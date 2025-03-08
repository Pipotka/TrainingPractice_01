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

			var chessPieceNumberDictionary = new Dictionary<Type, int>()
            {
                {typeof(Bishop), Bishop.Number},
                {typeof(Castle), Castle.Number},
                {typeof(King), King.Number},
                {typeof(Knight), Knight.Number},
                {typeof(Queen), Queen.Number},
            };

            Console.Write("Введите исходные данные: ");
			var parsedInput = Console.ReadLine().ToLower().Split(' ');

			var index = 0;
			var movingChessPiece = nameChessPiecesDictionary[parsedInput[index++]];
			movingChessPiece.Position = ChessPosition.FromTheChessAnnotation(parsedInput[index++]);
            var attackingChessPiece = nameChessPiecesDictionary[parsedInput[index++]];
            attackingChessPiece.Position = ChessPosition.FromTheChessAnnotation(parsedInput[index++]);
			var finalPosition = ChessPosition.FromTheChessAnnotation(parsedInput[index++]);
            
			_chessboard[attackingChessPiece.Position.Row, attackingChessPiece.Position.Column] = chessPieceNumberDictionary[attackingChessPiece.GetType()];
			foreach (var position in attackingChessPiece.GetAttackedPositions(attackingChessPiece.Position, _chessboard))
			{
				_chessboard[position.Row, position.Column] -= 1;
            }
			_chessboard[movingChessPiece.Position.Row, movingChessPiece.Position.Column] = chessPieceNumberDictionary[movingChessPiece.GetType()];

			var resultPath = new Stack<ChessPosition>();
			if (FindThePath(movingChessPiece.Position, finalPosition, movingChessPiece, resultPath))
			{
				Console.WriteLine($"{movingChessPiece.GetName()} дойдет до {finalPosition}");
				Console.Write($"Путь от {movingChessPiece.Position} до {finalPosition}: ");
				var path = resultPath.ToList();
				path.Reverse();
				foreach (var step in path)
				{
					Console.Write($"{step} ");
				}
			}
			else
			{
				Console.WriteLine($"{movingChessPiece.GetName()} не дойдет до {finalPosition}");
			}
			Console.WriteLine();
			Console.WriteLine();
			ClearChessboard(1);
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

			for (var row = 0; row < _chessboard.GetLength(0); row++)
			{
				Console.Write($"{row + 1} ");
				for (var col = 0; col < _chessboard.GetLength(1); col++)
				{
					if (_chessboard[row, col] <= 0)
					{
						if (((row % 2 == 1 && col % 2 == 0)
						|| (row % 2 == 0 && col % 2 == 1)))
						{
							if (_chessboard[row, col] < 0)
							{
								Console.BackgroundColor = ConsoleColor.Red;
							}
							Console.Write(' ');
							Console.BackgroundColor = ConsoleColor.Black;

							continue;
						}

						if (_chessboard[row, col] < 0)
						{
							Console.ForegroundColor = ConsoleColor.Red;
						}
						Console.Write('П');
						Console.ForegroundColor = ConsoleColor.White;
					}
					else
					{
						switch (_chessboard[row, col])
						{
							case Bishop.Number:
								Console.Write('С');
								break;

							case Castle.Number:
								Console.Write('Л');
								break;

							case King.Number:
								Console.Write('К');
								break;

							case Knight.Number:
								Console.Write('Г');
								break;

							case Queen.Number:
								Console.Write('Ф');
								break;
						}
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

        private static void ClearChessboard(int numberToDelete)
		{
			for (var row = 0; row < _chessboard.GetLength(0); row++)
			{
                for (var col = 0; col < _chessboard.GetLength(0); col++)
                {
					if (_chessboard[row, col] == numberToDelete)
					{
						_chessboard[row, col] = 0;
					}
				}
            }
		}

        private static bool FindThePath(ChessPosition currentPosition, ChessPosition finalPosition, IChessPiece chessPiece, Stack<ChessPosition> result)
		{
			//Рекурсивный случай
			foreach (var position in chessPiece.GetAttackedPositions(currentPosition, _chessboard)
				.Where(x => x.Difference(finalPosition) <= currentPosition.Difference(finalPosition))
				.OrderBy(x => x.Difference(finalPosition)))
			{
                //Базовый случай
                if (RecursiveAlgorithm(position))
				{
					return true;
				}
                //Базовый случай
            }

            foreach (var position in chessPiece.GetAttackedPositions(currentPosition, _chessboard)
                .Where(x => x.Difference(finalPosition) > currentPosition.Difference(finalPosition))
				.OrderBy(x => x.Difference(finalPosition)))
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
