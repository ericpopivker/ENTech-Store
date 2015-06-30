namespace ENTech.Store.Infrastructure.Services.Validators
{

	public interface IDtoValidator
	{
		DtoValidatoResult Validate(object dto, string propertyParentPath = null);
	}

	public interface IDtoValidator<TDto> : IDtoValidator
	{
		DtoValidatoResult Validate(TDto dto, string propertyParentPath=null);
	}


	
}