using System;
using System.Globalization;

namespace ANM_Task_01
{
	internal class Program
	{
		private static double _crystalPrice = 45.0;
		static void Main(string[] args)
		{
			Console.Write("Сколько золота вы желаете? -> ");
			var isCorrectInput = false;
			var wallet = 0.0;
			while (!isCorrectInput)
			{
				try
				{
					wallet = double.Parse(Console.ReadLine(),
						NumberStyles.Float,
						CultureInfo.InvariantCulture);
					isCorrectInput = true;
				}
				catch
				{
					Console.WriteLine("Господин, в нашем королевстве золото измеряется в цифрах");
				}
			}
			var crystalCount = 0;
			do
			{
				isCorrectInput = false;
				Console.WriteLine($"Ваш кошелёк полон монет! В нём находится {wallet} золота");
				Console.WriteLine($"Господин, обратите внимание на мой широкий ассортимент кристалов, каждый стоит по {_crystalPrice}");
				Console.Write("Сколько кристалов вы желаете приобрести? -> ");
				while (!isCorrectInput)
				{
					try
					{
						crystalCount = int.Parse(Console.ReadLine());
						isCorrectInput = true;
					}
					catch
					{
						Console.WriteLine("Господин, в нашем королевстве количество измеряется в цифрах");
					}
				}
			} while (crystalCount <= 0 || wallet < _crystalPrice * crystalCount);

			wallet -= _crystalPrice * crystalCount;
			Console.WriteLine($"Ваш кошель опустел. Посчитав монеты вы увидели {wallet} монет, однако теперь в ваших карманах {crystalCount} кристалов");



		}
	}
}
