using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANM_Task_03
{
	internal class Boss
	{
		public int HP { get; private set; }
		public bool IsAlive => HP > 0;
		private List<Effect> effects = new List<Effect>();

		public Boss(int hp) => HP = hp;

		public void TakeDamage(int damage)
		{
			HP = Math.Max(HP - damage, 0);
			Console.WriteLine($"Босс получает {damage} урона!");

			// Обработка эффектов
			effects.ForEach(e => e.Apply(this));
			effects.RemoveAll(e => e.IsExpired);
		}

		public void AddEffect(Effect effect) => effects.Add(effect);
	}
}
