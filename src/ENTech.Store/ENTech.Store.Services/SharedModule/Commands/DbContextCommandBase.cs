using ENTech.Store.Infrastructure.Database.EF6;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Services.SharedModule.Commands
{
	public abstract class DbContextCommandBase<TRequest, TResponse> : CommandBase<TRequest, TResponse>
		where TRequest : IInternalRequest
		where TResponse : InternalResponse, new()
	{
		private readonly IDbContext _dbContext;

		protected IDbContext DbContext
		{
			get { return _dbContext; }
		}

		protected DbContextCommandBase(IDbContext dbContext, bool requiresTransaction) : base(requiresTransaction)
		{
			_dbContext = dbContext;
		}
	}
}