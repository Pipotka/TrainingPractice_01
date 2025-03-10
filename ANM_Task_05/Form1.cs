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
			this.FindForm
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
