using System;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public interface IDtoValidatorFactory
	{
		IDtoValidator<TDto> TryCreate<TDto>();
		IDtoValidator TryCreate(Type dtoType);
	}
}