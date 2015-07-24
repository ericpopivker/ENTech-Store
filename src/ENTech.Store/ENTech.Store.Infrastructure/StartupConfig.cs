using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Services.ProductModule.Validators.DtoValidators;
using FluentValidation;

namespace ENTech.Store.Infrastructure
{
	public static class StartupConfig
	{

		public static void RegisterComponents()
		{
			//ErrorMessageDictionary.RegisterAll();
			ValidatorOptions.ResourceProviderType = typeof(FluentValidationResources);
		}
	}
}
