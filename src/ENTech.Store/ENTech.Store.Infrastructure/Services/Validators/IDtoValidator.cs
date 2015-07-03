namespace ENTech.Store.Infrastructure.Services.Validators
{

	public interface IDtoValidator
	{
		DtoValidatorResult Validate(object dto, string propertyParentPath = null);
	}

	public interface IDtoValidator<TDto> : IDtoValidator
	{
		DtoValidatorResult Validate(TDto dto, string propertyParentPath=null);
	}


	
}