namespace ENTech.Store.Infrastructure.Services.Validators
{

	public interface IDtoValidator
	{
		IDtoValidatorResult Validate(object dto, string propertyParentPath = null);
	}

	public interface IDtoValidator<TDto> : IDtoValidator
	{
		DtoValidatorResult<TDto> Validate(TDto dto, string propertyParentPath=null);
	}


	
}