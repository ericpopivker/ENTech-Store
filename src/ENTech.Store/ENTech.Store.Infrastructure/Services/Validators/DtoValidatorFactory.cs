using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public class DtoValidatorFactory
	{
		public DtoValidatorFactory()
		{
		}

		public IDtoValidator<TDto> TryCreate<TDto>()
		{
			return (IDtoValidator<TDto>)TryCreate(typeof(TDto));
		}


		public IDtoValidator TryCreate(Type dtoType)
		{
			var genericType = typeof(IDtoValidator<>).MakeGenericType(dtoType);
			return TryResolve(genericType);
		}

		private IDtoValidator TryResolve(Type genericType)
		{
			return IoC.Resolve(genericType) as IDtoValidator;

		}

		public static void ScanAssemblyAndRegisterAllDtoValidators(Assembly assembly)
		{
			var types = assembly.GetExportedTypes();
			var openGenericType = typeof(IDtoValidator<>);

			var query = from type in types
						let interfaces = type.GetInterfaces()
						let genericInterfaces = interfaces.Where(i => IsGenericType(i) && i.GetGenericTypeDefinition() == openGenericType)
						let matchingInterface = genericInterfaces.FirstOrDefault()
						where matchingInterface != null
						select new {MatchingInterface = matchingInterface, ValidatorType = type};


			var unityContainter = IoC.Container;
			foreach (var match in query.ToList())
				unityContainter.RegisterType(match.MatchingInterface, match.ValidatorType);
		}

		private static bool IsGenericType(Type type)
		{
			return type.IsGenericType;
		}
		
	}
}
