using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANM_Task_03
{
	internal class Player
	{
		public int HP { get; private set; } = 500;
		public bool IsAlive => HP > 0;

		public bool TakeDamage(int damage)
		{
			if (HP <= damage)
			{
				HP = 0;
				return false;
			}

			HP -= damage;
			return true;
		}

		public void Heal(int amount) => HP = Math.Min(HP + amount, 500);
	}
}
