using ENTech.Store.Entities;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.SharedModule.Commands
{
	public abstract class DbContextCommandBase<TRequest, TResponse> : CommandBase<TRequest, TResponse>
	
		where TRequest : IRequest
		where TResponse : IResponse
	{
		private readonly IDbContext _dbContext;

		protected IDbContext DbContext
		{
			get { return _dbContext; }
		}

		protected DbContextCommandBase(IDbContext dbContext, IDtoValidatorFactory dtoValidatorFactory, bool requiresTransaction)
			: base(dtoValidatorFactory, requiresTransaction)
		{
			_dbContext = dbContext;
		}
	}
}