using System;
using System.Collections;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public class DtoValidatorVisitor
	{
		private IDtoValidatorFactory _dtoValidatorFactory;

		public DtoValidatorVisitor(IDtoValidatorFactory dtoValidatorFactory)
		{
			_dtoValidatorFactory = dtoValidatorFactory;
		}


		public IDtoValidatorResult Validate(object dto)
		{
			var dtoValidator = _dtoValidatorFactory.TryCreate(dto.GetType());

			if (dtoValidator == null)
				return new DtoValidatorResult<object>();

			var dependentDtosValidatorResult = new DtoValidatorResult<object>();
			
			foreach (var propertyInfo in dto.GetType().GetProperties())
			{
				if (!(propertyInfo.PropertyType.IsPrimitive ||
					propertyInfo.PropertyType == typeof(String) ||
					propertyInfo.PropertyType == typeof(Decimal) ||
					propertyInfo.PropertyType == typeof(Decimal?) ||
					propertyInfo.PropertyType == typeof(DateTime) ||
					propertyInfo.PropertyType == typeof(DateTime?)))
				{
					var val = propertyInfo.GetValue(dto);
					var propertyName = propertyInfo.Name;
				
					if (val != null)
					{
						if (val.GetType().GetInterface("IEnumerable") != null)
						{
							VisitEnumerable(propertyName, val, dependentDtosValidatorResult);
						}
						else
						{
							VisitSingle(propertyName, val, dependentDtosValidatorResult);
						}
					}
				}
			}

			var dtoValidatorResult = dtoValidator.Validate(dto);
			dtoValidatorResult.AddArgumentErrors(dependentDtosValidatorResult.ArgumentErrors);


			return dtoValidatorResult;
		}


		private void VisitEnumerable(string propertyName, object propertyVal, IDtoValidatorResult dtoValidatorResult)
		{
			IDtoValidator dtoValidator;
			var enumerator = ((IEnumerable) propertyVal).GetEnumerator();

			int index = 0;
			while (enumerator.MoveNext())
			{
				if (index == 0)
				{
					dtoValidator = _dtoValidatorFactory.TryCreate(enumerator.Current.GetType());
					if (dtoValidator == null)
						break; //For now assume that types in Enumerable are all of same type
				}

				var result = Validate(enumerator.Current);

				if (!result.IsValid)
				{
					dtoValidatorResult.AddArgumentErrors(result.ArgumentErrors);
					result.AddPrefixToArgumentErrorNames(propertyName + "[" + index + "]" + ".");
				}


				index++;
			}
		}


		private void VisitSingle(string propertyName, object propertyValue, IDtoValidatorResult dtoValidatorResult)
		{
			var result = Validate(propertyValue);

			if (!result.IsValid)
			{
				dtoValidatorResult.AddArgumentErrors(result.ArgumentErrors);

				dtoValidatorResult.AddPrefixToArgumentErrorNames(propertyName + ".");
			}
		}

	}
}
