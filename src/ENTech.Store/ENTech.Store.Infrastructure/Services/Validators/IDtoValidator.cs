
namespace ENTech.Store.Infrastructure.Services.Validators
{

	public interface IDtoValidator
	{
		IDtoValidatorResult Validate(object dto);
	}

	public interface IDtoValidator<TDto> : IDtoValidator
	{
		DtoValidatorResult<TDto> Validate(TDto dto);
	}

}