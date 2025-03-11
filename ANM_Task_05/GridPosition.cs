namespace ANM_Task_05
{
	internal struct GridPosition
	{
		public int Row { get; set; }

		public int Column { get; set; }

		public GridPosition(int row, int col)
		{
			Row = row;
			Column = col;
		}
	}
}
