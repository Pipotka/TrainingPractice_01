namespace ANM_Task_03
{
	internal abstract class Effect
	{
		public int Duration { get; protected set; }
		public bool IsExpired => Duration <= 0;
		public abstract void Apply(Boss boss);
	}
}
