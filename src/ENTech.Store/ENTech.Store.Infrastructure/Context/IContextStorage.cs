namespace ENTech.Store.Infrastructure.Context
{
	public interface IContextStorage
	{
		T Get<T>(string propertyName);
		void Store<T>(string propertyName, T value);
		void Clear(string propertyName);
		bool Contains(string propertyName);
	}
}