namespace ENTech.Store.Infrastructure.Services
{
	public abstract class BuilderBase<T, TBuilder>
		where T : new() where TBuilder : new()
	{
		protected BuilderBase()
		{
		}

		public static TBuilder Create()
		{
			return new TBuilder();
		}

		public abstract T Build();
	}
}
