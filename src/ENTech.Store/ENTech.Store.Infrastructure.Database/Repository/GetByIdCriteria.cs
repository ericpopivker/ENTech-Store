namespace ENTech.Store.Infrastructure.Database.Repository
{
	public class GetByIdCriteria<T> : QueryCriteria<T>
		where T : IProjection
	{
		private readonly int _id;
		public int Id { get { return _id; } }

		public GetByIdCriteria(int id) {
			_id = id;
		}
	}
}