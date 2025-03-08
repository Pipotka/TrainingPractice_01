using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANM_Task_04
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Управление:");
			Console.WriteLine("WASD - движение");
			Console.WriteLine("P - показать/скрыть путь");
			Console.WriteLine("-----------------------\n");

			Console.Write("Введите путь к карте: ");
			string path = Console.ReadLine();
			Console.WriteLine();
			Console.Write("Введите количество врагов: ");
			var enemyCount = 0;
			while (!int.TryParse(Console.ReadLine(), out enemyCount) || enemyCount < 0)
			{
				Console.WriteLine("Ошибка ввода. Попробуйте ещё раз.");
				Console.Write("Введите количество врагов: ");
			}

			try 
			{ 
				var game = new Game(path, enemyCount);
				Console.Clear();
				game.Run();
			}
			catch (Exception ex) 
			{
				Console.WriteLine(ex.Message); 
			}
		}
	}
}
