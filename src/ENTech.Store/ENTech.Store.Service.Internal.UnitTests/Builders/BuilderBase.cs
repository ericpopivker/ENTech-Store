namespace ENTech.Store.Services.Internal.UnitTests.Builders
{
	internal abstract class BuilderBase<T, TBuilder>
		where T : new() where TBuilder : new()
	{
		internal BuilderBase()
		{
		}

		public static TBuilder Create()
		{
			return new TBuilder();
		}

		public abstract T Build();
	}
}
