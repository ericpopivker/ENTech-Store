using System;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services.Commands
{
	public abstract class InternalCommandBase<TDbContext, TInternalRequest,TInternalResponse> : IInternalCommand<TInternalRequest, TInternalResponse>
		where TInternalRequest : class, IInternalRequest
		where TInternalResponse : class, IResponse, new()
	{
		private readonly TDbContext _dbContext;

		private TInternalRequest _request;
		private TInternalResponse _response;


		protected virtual TDbContext DbContext { get { return _dbContext; } }

		protected virtual TInternalRequest Request { get { return _request; } }

		protected virtual TInternalResponse Response { get { return _response; } }

		public InternalCommandBase(TDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		
		protected abstract void Execute();

		public virtual void ValidateRequestCustom()
		{
		}

		public virtual TInternalResponse Execute(TInternalRequest request)
		{
			_response = new TInternalResponse();
			_response.ArgumentErrors = new ArgumentErrorsCollection();

			_request = request;

			if (!Authorize())
			{
				return ErrorResponse(new Error(CommonErrorCode.UserNotAuthorized));
			}

			if (ValidateRequest())
			{
				try
				{
					Response.IsSuccess = true;
					Execute();
				}
				catch (Exception e)
				{
					//ErrorLogUtils.AddError(e);
					return ErrorResponse(new Error(CommonErrorCode.InternalServerError, e.Message));
				}
			}
			else
			{
				return ErrorResponse(new Error(CommonErrorCode.ArgumentErrors));
			}

			return Response;
		}

		protected virtual bool ValidateRequest()
		{
			DtoValidator.VisitAndValidateProperties(_request, Response.ArgumentErrors as ArgumentErrorsCollection);

			if (Response.ArgumentErrors.Count == 0)
				ValidateRequestCustom();

			return Response.ArgumentErrors.Count == 0;
		}

		protected virtual bool Authorize()
		{
			return true;
		}

		protected TInternalResponse ErrorResponse(Error error, ArgumentErrorsCollection argumentErrors = null,
			string extraMessage = null)
		{
			Response.IsSuccess = false;

			Response.Error = error;

			if (argumentErrors != null)
			{
				foreach (var argumentError in argumentErrors)
				{
					Response.ArgumentErrors[argumentError.ArgumentName] = argumentError;
				}
			}

			if (extraMessage != null)
				Response.Error.ErrorMessage += Environment.NewLine + extraMessage;

			return Response;
		}

	}
}