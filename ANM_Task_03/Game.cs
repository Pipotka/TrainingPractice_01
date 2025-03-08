using System;
using System.Threading;

namespace ANM_Task_03
{
	internal class Game
	{
		private Player player = new Player();
		private Boss boss = new Boss(500);
		private bool isShadowSpiritSummoned = false;
		private bool isDimensionRiftActive = false;

		public void Start()
		{
			Console.WriteLine("Темный босс пробудился! Ваша цель - уничтожить его!\n");
			Thread.Sleep(1000);

			while (player.IsAlive && boss.IsAlive)
			{
				Console.Clear();
				DisplayStatus();
				CastSpell(ChooseSpell());
				BossCounterAttack();
			}

			Console.WriteLine(boss.IsAlive ? "Вы пали..." : "Босс повержен! Тень победы!");
		}

		private void DisplayStatus()
		{
			Console.WriteLine($"\nВаше HP: {player.HP} | HP босса: {boss.HP}\n");
			Console.WriteLine("Доступные заклинания:");
			Console.WriteLine("1. Рашамон (100 HP) - Призыв теневого духа");
			Console.WriteLine("2. Хуганзакура (требует духа) - Удар клинками");
			Console.WriteLine("3. Разлом (250 HP) - Восстановление и защита");
			Console.WriteLine("4. Проклятие Тьмы (150 HP) - Постепенный урон");
			Console.WriteLine("5. Клинок Света (200 HP) - Священная атака\n");
		}

		private int ChooseSpell()
		{
			Console.Write("Выберите заклинание (1-5): ");
			return int.Parse(Console.ReadLine());
		}

		private void CastSpell(int spellId)
		{
			switch (spellId)
			{
				case 1:
					Rashamon();
					break;
				case 2:
					Huganzakura();
					break;
				case 3:
					DimensionRift();
					break;
				case 4:
					DarkCurse();
					break;
				case 5:
					LightBlade();
					break;
				default:
					Console.WriteLine("Неизвестное заклинание!");
					break;
			}
		}

		// Заклинание 1: Призыв духа
		private void Rashamon()
		{
			if (player.TakeDamage(100))
			{
				isShadowSpiritSummoned = true;
				Console.WriteLine("Теневой дух призван!");
			}
		}

		// Заклинание 2: Атака духа
		private void Huganzakura()
		{
			if (!isShadowSpiritSummoned)
			{
				Console.WriteLine("Сначала призовите духа!");
				return;
			}

			boss.TakeDamage(100);
			isShadowSpiritSummoned = false;
			Console.WriteLine("Дух атакует босса, и растворяется в воздухе!");
		}

		// Заклинание 3: Защита и лечение
		private void DimensionRift()
		{
			if (player.HP > 0)
			{
				player.Heal(250);
				isDimensionRiftActive = true;
				Console.WriteLine("Вы скрылись в разломе!");
			}
		}

		// Заклинание 4: Проклятие
		private void DarkCurse()
		{
			if (player.TakeDamage(150))
			{
				boss.AddEffect(new CurseEffect());
				Console.WriteLine("Босс проклят!");
			}
		}

		// Заклинание 5: Мощная атака
		private void LightBlade()
		{
			if (player.HP > 50 && player.TakeDamage(200))
			{
				boss.TakeDamage(300);
				Console.WriteLine("Священный клинок испепелил врага!");
			}
		}

		private void BossCounterAttack()
		{
			if (!boss.IsAlive || isDimensionRiftActive)
			{
				isDimensionRiftActive = false;
				return;
			}

			player.TakeDamage(50);
			Console.WriteLine("Босс наносит ответный удар!");
		}
	}
}
