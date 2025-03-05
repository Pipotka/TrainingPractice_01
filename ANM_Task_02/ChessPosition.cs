using System.Collections.Generic;

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

		public int Row { get; set; }

		public int Column { get; set; }

		public override string ToString()
		{
			return $"{ColumnCharDictionary[Column]}{Row}";
		}
	}
}
