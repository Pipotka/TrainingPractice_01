using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANM_Task_03
{
	internal class CurseEffect : Effect
	{
		public CurseEffect() => Duration = 3;

		public override void Apply(Boss boss)
		{
			boss.TakeDamage(50);
			Duration--;
			Console.WriteLine("Проклятие наносит урон!");
		}
	}
}
