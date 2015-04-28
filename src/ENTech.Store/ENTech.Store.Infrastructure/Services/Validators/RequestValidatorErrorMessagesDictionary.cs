using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ENTech.Store.Infrastructure.Attributes;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public class RequestValidatorErrorMessagesDictionary
	{
		internal struct ErrorMessage
		{
			public string Message { get; set; }

			public string DeclaringTypeName { get; set; }
		}

		private readonly Dictionary<int, ErrorMessage> _errorMessages;

		public RequestValidatorErrorMessagesDictionary()
		{
			_errorMessages = new Dictionary<int, ErrorMessage>();
		}
 
		public static RequestValidatorErrorMessagesDictionary Instance = new Lazy<RequestValidatorErrorMessagesDictionary>().Value;

		public string this[int code]
		{
			get
			{
				if (!_errorMessages.ContainsKey(code))
					throw new Exception(
						"RequestValidatorErrorMessagesDictionary does not contain error message for code " + code +
						". Possibly you missed to run initialize one of error codes enumeration classes.");
				return _errorMessages[code].Message;
			}
		}

		public int Count
		{
			get { return _errorMessages.Count; }
		}

		public static void Register<T>() where T : ErrorCodeBase
		{
			var type = typeof (T);
			var consts = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Where(f=>f.FieldType==typeof(int)).ToList();

			foreach (var codeConst in consts)
			{
				var code = (int) codeConst.GetValue(null);
				if (Instance._errorMessages.ContainsKey(code))
				{
					if (Instance._errorMessages[code].DeclaringTypeName != type.Name)
						throw new Exception("RequestValidatorErrorMessagesDictionary already has code " + code + " definded in type " +
											Instance._errorMessages[code].DeclaringTypeName);
					continue;
				}
				Instance._errorMessages[code] =
					new ErrorMessage
						{
							Message = codeConst.GetCustomAttribute<StringValueAttribute>().StringValue,
							DeclaringTypeName = type.Name
						};
			}
		}
	}
}