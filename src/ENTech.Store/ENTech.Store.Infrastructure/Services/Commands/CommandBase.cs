using System;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Errors.ResponseErrors;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services.Commands
{
	public abstract class CommandBase<TRequest, TResponse> : ICommand<TRequest, TResponse>
		where TRequest : IRequest
		where TResponse : IResponse, new()
	{
		private readonly bool _requiresTransaction;
		private readonly IDtoValidatorFactory _dtoValidatorFactory;

		protected CommandBase(IDtoValidatorFactory dtoValidatorFactory, bool requiresTransaction)
		{
			_requiresTransaction = requiresTransaction;
			_dtoValidatorFactory = dtoValidatorFactory; 
		}

		public abstract TResponse Execute(TRequest request);

		public bool RequiresTransaction
		{
			get { return _requiresTransaction; }
		}

		public ValidateCommandResult Validate(TRequest request)
		{
			var validateRequestResult = new ValidateRequestResult<TRequest>();

			ValidateRequest(request, validateRequestResult);

			if (!validateRequestResult.IsValid)
				return ValidateCommandResult.Invalid(new InvalidArgumentsResponseError(validateRequestResult.ArgumentErrors));
			
				
			var validateOperationResult = ValidateOperation(request);
			if (!validateOperationResult.IsValid)
				return ValidateCommandResult.Invalid(validateOperationResult.ResponseError);
			
			return ValidateCommandResult.Valid();
		}

		private void ValidateRequest(TRequest request, ValidateRequestResult<TRequest> validateRequestResult)
		{
			var dtoValidator = _dtoValidatorFactory.TryCreate<TRequest>();
			if (dtoValidator != null)
		{
				var dtoValidatorResult = dtoValidator.Validate(request);
				if (!dtoValidatorResult.IsValid)
					validateRequestResult.ArgumentErrors.AddRange(dtoValidatorResult.ArgumentErrors);
			}

			ValidateRequestInternal(request, validateRequestResult);
		}

		protected virtual void ValidateRequestInternal(TRequest request, ValidateRequestResult<TRequest> validateRequestResult)
		{
		}
			
		private ValidateOperationResult ValidateOperation(TRequest request)
		{
			return ValidateOperationInternal(request);
		}

		protected virtual ValidateOperationResult ValidateOperationInternal(TRequest request)
		{
			return ValidateOperationResult.Valid();
		}

		
		
		public virtual void NotifyExecuted(TRequest request, TResponse response)
		{
			//
		}

		protected TResponse InternalServerError()
		{
			throw new Exception();
		} 
	}
}