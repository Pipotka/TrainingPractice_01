using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ANM_Task_02
{
	internal struct ChessPosition
	{
		public static readonly Dictionary<int, char> ColumnCharDictionary = new Dictionary<int, char>
		{
			{0, 'a'},
			{1, 'b'},
			{2, 'c'},
			{3, 'd'},
			{4, 'e'},
			{5, 'f'},
            {6, 'g'},
            {7, 'h'},
        };

		public static readonly Dictionary<char, int> CharColumnDictionary = new Dictionary<char, int>
        {
            {'a', 0},
            {'b' , 1},
            {'c' , 2},
            {'d' , 3},
            {'e' , 4},
            {'f' , 5},
            {'g' , 6},
            {'h' , 7},
        };

        public int Row { get; set; }

		public int Column { get; set; }

		public override string ToString()
		{
			return $"{ColumnCharDictionary[Column]}{Row}";
		}

		/// <summary>
		/// Возвращает разницу в расстоянии по столбцам и строкам в клетках
		/// </summary>
		/// <param name="position">позициия с которой происходит сравнение</param>
		public int Difference(ChessPosition position)
			=> Math.Abs(Row - position.Row) + Math.Abs(Column - position.Column);

        /// <summary>
        /// Возвращает <see cref="ChessPosition"/> из позиции, заданной в шахматной аннотации
        /// </summary>
        public static ChessPosition FromTheChessAnnotation(string position)
		{
			var column = CharColumnDictionary[position.ToLower()[0]];
			var row = int.Parse(position.Substring(1, 1));
			return new ChessPosition
			{
				Row = row,
				Column = column
			};
        }
    }
}
