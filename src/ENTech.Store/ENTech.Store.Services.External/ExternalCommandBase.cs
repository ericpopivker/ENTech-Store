using System;
using System.Collections.Generic;
using AutoMapper;
using ENTech.Store.Entities;
using ENTech.Store.Infrastructure;
using ENTech.Store.Infrastructure.Services;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;



namespace ENTech.Store.Services.External
{
	public abstract class ExternalCommandBase<TExternalRequest, TExternalResponse, TInternalRequest, TInternalResponse> :
		IExternalCommand<TExternalRequest, TExternalResponse, TInternalRequest, TInternalResponse>
		where TExternalRequest : ExternalRequestBase, IExternalRequest
		where TExternalResponse : ExternalResponse, new()
		where TInternalRequest : class, IInternalRequest
		where TInternalResponse : InternalResponse
	{
		protected virtual bool RequireTransaction { get { return false; } }

		protected virtual TExternalRequest Request { get; private set; }

		protected virtual TExternalResponse Response { get; private set; }

		protected IUnitOfWork UnitOfWork { get; private set; }

	
		public virtual TExternalResponse Execute(TExternalRequest externalRequest)
		{
			Request = externalRequest;
			Response = new TExternalResponse();
			Response.ArgumentErrors = new ArgumentErrorsCollection();

			try
			{
				UnitOfWork = IoC.Resolve<IUnitOfWork>();

				if (!Authenticate())
				{
					return ErrorResponse(new Error(CommonErrorCode.UserNotAuthenticated));
				}

				LimitDbContext(UnitOfWork.DbContext);

				if (RequireTransaction)
				{
					UnitOfWork.BeginTransaction();
				}

				try
				{
					TInternalRequest internalRequest = CreateInternalRequestFromRequest();

					TInternalResponse internalResponse = ExecuteInternal(internalRequest);

					UnitOfWork.SaveChanges();

					if (RequireTransaction)
					{
						UnitOfWork.CompleteTransaction();
					}

					if (!internalResponse.IsSuccess)
					{

						return InternalErrorResponse(internalResponse.Error, internalResponse.ArgumentErrors);
					}

					FillResponseFromInternalResponse(internalResponse);
				}
				catch (Exception e)
				{
					if (RequireTransaction)
					{
						UnitOfWork.RollbackTransaction();
					}
	
					return ErrorResponse(new Error(CommonErrorCode.InternalServerError),  extraMessage: e.Message + Environment.NewLine + e.StackTrace);
				}
			}
			catch (Exception e)
			{
				return ErrorResponse(new Error(CommonErrorCode.InternalServerError),
					extraMessage: e.Message + Environment.NewLine + e.StackTrace);
			}
			finally
			{
				if (UnitOfWork != null)
					UnitOfWork.Dispose();
			}

			return SuccessResponse();
		}

		protected abstract void LimitDbContext(IDbContext dbContext);

		private TExternalResponse InternalErrorResponse(Error error, IEnumerable<ArgumentError> argumentErrors)
		{
			Response.Error = error;
			Response.IsSuccess = false;

			if (argumentErrors != null)
			{
				foreach (var argumentError in argumentErrors)
				{
					Response.ArgumentErrors[argumentError.ArgumentName] = argumentError;
				}
			}

			return Response;
		}

		protected virtual TInternalRequest CreateInternalRequestFromRequest()
		{
			TInternalRequest internalRequest = Mapper.Map<TExternalRequest, TInternalRequest>(Request);
			return internalRequest;
		}


		protected virtual void FillResponseFromInternalResponse(TInternalResponse internalResponse)
		{
			Mapper.Map(internalResponse, Response);
		}

		public abstract TInternalResponse ExecuteInternal(TInternalRequest internalRequest);


		protected abstract bool Authenticate();

		protected TExternalResponse ErrorResponse(Error error, ArgumentErrorsCollection argumentErrors = null,
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

		protected TExternalResponse SuccessResponse()
		{
			Response.IsSuccess = true;
			return Response;
		}
	}
}

