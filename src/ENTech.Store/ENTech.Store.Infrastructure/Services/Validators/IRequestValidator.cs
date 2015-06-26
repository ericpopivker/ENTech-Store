using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public interface IRequestValidator
	{
		bool TryValidate<TEntity, TMember>(RequestValidatorAction<TEntity, TMember> action,
										TEntity entity,
										Errors.InvalidArgumentsError existingErrors);
	}
}
