using ENTech.Store.Infrastructure.Attributes;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	/// <summary>
	/// Base class for all ErrorCode classes, values of int consts should be unique
	/// </summary>
	public sealed class ErrorCode : ErrorCodeBase
	{

		//Dto Validation Default Messages

		[StringValue("")]
		public const int DtoValidationFailed = 10049;
		
		//We introduce ranges for error codes: default range is 10001-10100
		//For all future modules: Module range is 1000
		//For all future services: Service range is 100
		//All classes with error code int constants should derive RequestValidatorErrorCodeBase class
		[StringValue("Store with given id not found")]
		public const int StoreNotFound = 10050;

		[StringValue("Internal error occured")]
		public const int InternalError = 10051;

		[StringValue("Id is not default")]
		public const int NotNewId = 10052;
	}
}