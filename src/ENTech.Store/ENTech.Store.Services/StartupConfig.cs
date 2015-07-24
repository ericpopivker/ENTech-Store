using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.ProductModule.Validators.DtoValidators;
using FluentValidation;

namespace ENTech.Store.Services
{
	public static class StartupConfig
	{
		public static void RegisterComponents()
		{
			Infrastructure.StartupConfig.RegisterComponents();

			DtoValidatorFactory.ScanAssemblyAndRegisterAllDtoValidators(typeof(StartupConfig).Assembly);
		}
	}
}
