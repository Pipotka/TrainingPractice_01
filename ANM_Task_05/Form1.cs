using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ANM_Task_05
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			var grid = new Grid();
			grid.GenerateBaseGrid();
			for (int row = 0; row < 9; row++)
			{
				for (int col = 0; col < 9; col++)
				{
					var name = $"tB{row}{col}";
					var control = Controls.Find(name, true).FirstOrDefault();
					if (control != null && control is TextBox tb)
					{
						tb.Text = grid.PlayingField[row, col].ToString();
                    }
				}
			}
		}

		private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (char.IsDigit(e.KeyChar) && e.KeyChar != '0')
			{
				if (sender is TextBox tb)
				{
					tb.Text = e.KeyChar.ToString();
				}
			}
		}
	}
}
