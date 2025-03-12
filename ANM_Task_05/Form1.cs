using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ANM_Task_05
{
	public partial class Form1 : Form
	{
		private Grid userGrid;
		private Grid UserGrid
		{
			get => userGrid;
			set
			{
				userGrid = value;
				positionRedCells.Clear();
				positionGreenCells.Clear();
				DrawGrid(userGrid);
				mainTableLayoutPanel.Refresh();
			}
		}
		private Dictionary<GridPosition, List<GridPosition>> positionRedCells;
		private Dictionary<GridPosition, List<GridPosition>> positionGreenCells;
		private DifficultyLevel difficultyLevel = DifficultyLevel.Easy;
		private Dictionary<DifficultyLevel, string> DifficultyLevelGrids;
		private Color controlColor = Color.FromArgb(255, 240, 240, 240);
		public Form1()
		{
			InitializeComponent();
            positionRedCells = new Dictionary<GridPosition, List<GridPosition>>();
            positionGreenCells = new Dictionary<GridPosition, List<GridPosition>>();
			var mapsDirect = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)));

			DifficultyLevelGrids = new Dictionary<DifficultyLevel, string>()
			{
				{DifficultyLevel.Easy, Path.Combine(mapsDirect, "EasyGrid.txt") },
				{DifficultyLevel.Medium, Path.Combine(mapsDirect, "MediumGrid.txt") },
				{DifficultyLevel.Hard, Path.Combine(mapsDirect, "HardGrid.txt") },
			};

			difficultyLevelComboBox.DataSource = Enum.GetValues(typeof(DifficultyLevel));
			UserGrid = Grid.FromFile(DifficultyLevelGrids[difficultyLevel]);
		}

		private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (char.IsDigit(e.KeyChar) && e.KeyChar != '0')
			{
				if (sender is TextBox tb)
				{
					tb.Text = e.KeyChar.ToString();
					var value = int.Parse(e.KeyChar.ToString());
					var position = new GridPosition(int.Parse(tb.Name[tb.Name.Length - 2].ToString()),
						int.Parse(tb.Name[tb.Name.Length - 1].ToString()));

					UserGrid.PlayingField[position.Row, position.Column] = value;

					if (tb.BackColor == Color.Red)
					{
						tb.BackColor = controlColor;
					}

                    List<GridPosition> oldRed = null;
                    List<GridPosition> oldGreen = null;

                    positionRedCells.TryGetValue(position, out oldRed);
                    positionGreenCells.TryGetValue(position, out oldGreen);

					if (oldRed != null) 
					{
						PaintCells(oldRed, controlColor);
					}

					if (oldGreen != null)
					{
						PaintCells(oldGreen, controlColor);
					}

					positionRedCells.Remove(position);
                    positionGreenCells.Remove(position);

                    ValidateMove(position, UserGrid.PlayingField,
                        out var newRed,
                        out var newGreen);

                    PaintCells(newRed, Color.Red);
                    PaintCells(newGreen, Color.Green);

                    if (newRed.Count > 0)
                        positionRedCells[position] = newRed;

                    if (newGreen.Count > 0)
                        positionGreenCells[position] = newGreen;

                    mainTableLayoutPanel.Refresh();
                }
			}
		}


        private void fromFileButton_Click(object sender, EventArgs e)
        {
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				try
				{
                    UserGrid = Grid.FromFile(openFileDialog1.FileName);
					difficultyLevelComboBox.Enabled = false;
                }
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Ошибка");
					return;
				}
            }
		}

        private void checkButton_Click(object sender, EventArgs e)
        {
			for (var row = 0; row < UserGrid.PlayingField.GetLength(0); row++)
			{
				for (var col = 0; col < UserGrid.PlayingField.GetLength(1); col++)
				{
					var position = new GridPosition(row, col);
					if (!ValidateMove(position, UserGrid.PlayingField, out var red, out var green))
					{
						MessageBox.Show("Неправильно!");
						return;
					}
				}
			}
			MessageBox.Show("Всё верно!");
        }

        private void difficultyLevelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
			difficultyLevel = (DifficultyLevel)difficultyLevelComboBox.SelectedValue;
			UserGrid = Grid.FromFile(DifficultyLevelGrids[difficultyLevel]);
        }

        private void PaintCells(ICollection<GridPosition> cells, Color color)
		{
            foreach (var cell in cells)
            {
                var name = $"tB{cell.Row}{cell.Column}";
                var control = Controls.Find(name, true).FirstOrDefault();
                if (control != null && control is TextBox tbCell)
                {
                    tbCell.BackColor = color;
					tbCell.Invalidate();
                }
            }
        }

        private void PaintCell(GridPosition cell, Color color)
        {
            var name = $"tB{cell.Row}{cell.Column}";
            var control = Controls.Find(name, true).FirstOrDefault();
            if (control != null && control is TextBox tbCell)
            {
                tbCell.BackColor = color;
                tbCell.Invalidate();
            }
        }

        private void DrawGrid(Grid grid)
		{
			ClearGrid();

            for (int row = 0; row < 9; row++)
			{
				for (int col = 0; col < 9; col++)
				{
					var name = $"tB{row}{col}";
					var control = Controls.Find(name, true).FirstOrDefault();
					if (control != null && control is TextBox tb)
					{
						if (grid.PlayingField[row, col] != 0)
						{
							tb.Text = grid.PlayingField[row, col].ToString();
							tb.Enabled = false;
							tb.Invalidate();
						}
					}
				}
			}
            mainTableLayoutPanel.Refresh();
        }

        private void ClearGrid()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    var name = $"tB{row}{col}";
                    var control = Controls.Find(name, true).FirstOrDefault();
                    if (control != null && control is TextBox tb)
                    {
                        tb.Text = string.Empty;
						tb.BackColor = controlColor;
                        tb.Enabled = true;
                        tb.Invalidate();
                    }
                }
            }
            mainTableLayoutPanel.Refresh();
        }

        private bool ValidateMove(
			GridPosition position,
			int[,] grid,
			out List<GridPosition> redPositions,
			out List<GridPosition> greenPositions)
		{
			redPositions = new List<GridPosition>();
			greenPositions = new List<GridPosition>();
			int value = grid[position.Row, position.Column];

			// Проверка на пустую ячейку
			if (value == 0)
				return true;

			// Поиск конфликтов
			FindConflicts(position, grid, value, ref redPositions);

			// Если есть конфликты - возвращаем false
			if (redPositions.Count > 0)
				return false;

			// Проверка заполненных структур
			CheckCompletedStructures(position, grid, ref greenPositions);

			return true;
		}

        private void FindConflicts(
    GridPosition position,
    int[,] grid,
    int value,
    ref List<GridPosition> conflicts)
        {
            // Проверка строки (горизонталь)
            CheckLine(
                fixedRow: position.Row,
                startCol: 0,
                colStep: 1,
                grid: grid,
                value: value,
                conflicts: ref conflicts);

            // Проверка столбца (вертикаль)
            CheckLine(
                fixedCol: position.Column,
                startRow: 0,
                rowStep: 1,
                grid: grid,
                value: value,
                conflicts: ref conflicts);

            // Проверка региона 3x3
            CheckRegion(
                regionRow: position.Row / 3 * 3,
                regionCol: position.Column / 3 * 3,
                grid: grid,
                value: value,
                conflicts: ref conflicts);

            // Удаляем дубликаты и исходную позицию
            conflicts.RemoveAll(p => p.Equals(position));
        }

        private void CheckLine(
			int[,] grid,
			int value,
			ref List<GridPosition> conflicts,
            int? fixedRow = null,
            int? fixedCol = null,
            int startRow = 0,
            int startCol = 0,
            int rowStep = 0,
            int colStep = 1)
        {
            for (int i = 0; i < 9; i++)
            {
                int row = fixedRow ?? (startRow + i * rowStep);
                int col = fixedCol ?? (startCol + i * colStep);

                if (grid[row, col] == value)
                {
                    conflicts.Add(new GridPosition(row, col));
                }
            }
        }

        private void CheckRegion(
			int regionRow,
			int regionCol,
			int[,] grid,
			int value,
			ref List<GridPosition> conflicts)
		{
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					int row = regionRow + i;
					int col = regionCol + j;

					if (grid[row, col] == value)
					{
						conflicts.Add(new GridPosition(row, col));
					}
				}
			}
		}

		private void CheckCompletedStructures(
		GridPosition position,
		int[,] grid,
		ref List<GridPosition> completed)
		{
			// Проверка строки
			if (IsRowComplete(position.Row, grid))
				completed.AddRange(GetRowPositions(position.Row));

			// Проверка столбца
			if (IsColumnComplete(position.Column, grid))
				completed.AddRange(GetColumnPositions(position.Column));

			// Проверка региона
			if (IsRegionComplete(position.Row, position.Column, grid))
				completed.AddRange(GetRegionPositions(position.Row, position.Column));
		}

		private bool IsRowComplete(int row, int[,] grid)
		{
			for (int col = 0; col < 9; col++)
			{
				if (grid[row, col] == 0)
					return false;
			}
			return true;
		}

		private bool IsColumnComplete(int col, int[,] grid)
		{
			for (int row = 0; row < 9; row++)
			{
				if (grid[row, col] == 0)
					return false;
			}
			return true;
		}

		private bool IsRegionComplete(int row, int col, int[,] grid)
		{
			int startRow = row / 3 * 3;
			int startCol = col / 3 * 3;

			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if (grid[startRow + i, startCol + j] == 0)
						return false;
				}
			}
			return true;
		}

		private List<GridPosition> GetRowPositions(int row)
		{
			var positions = new List<GridPosition>();
			for (int col = 0; col < 9; col++)
				positions.Add(new GridPosition(row, col));
			return positions;
		}

		private List<GridPosition> GetColumnPositions(int col)
		{
			var positions = new List<GridPosition>();
			for (int row = 0; row < 9; row++)
				positions.Add(new GridPosition(row, col));
			return positions;
		}

		private List<GridPosition> GetRegionPositions(int row, int col)
		{
			var positions = new List<GridPosition>();
			int startRow = row / 3 * 3;
			int startCol = col / 3 * 3;

			for (int i = 0; i < 3; i++)
				for (int j = 0; j < 3; j++)
					positions.Add(new GridPosition(startRow + i, startCol + j));

			return positions;
		}

    }
}
