using ENTech.Store.Infrastructure.Services.Factories;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.UnitOfWork;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services.Commands
{
	public abstract class InternalCommandBase<TInternalRequest, TInternalResponse> : IInternalCommand<TInternalRequest, TInternalResponse>
		where TInternalRequest : class, IInternalRequest
		where TInternalResponse : class, IResponse, new()
	{
		private TInternalRequest _request;
		private TInternalResponse _response;

		
		protected virtual TInternalRequest Request { get { return _request; } }

		protected virtual TInternalResponse Response { get { return _response; } }

		protected abstract void Execute();

		public virtual void ValidateRequestCustom()
		{
		}

		public TInternalResponse Execute(TInternalRequest request)
		{
			_response = new TInternalResponse();
			_response.ArgumentErrors = new ArgumentErrorsCollection();

			_request = request;

			if (ValidateRequest())
			{
				Response.IsSuccess = true;

				Execute();
			}

			return Response;
		}

		protected virtual bool ValidateRequest()
		{
			DtoValidator.VisitAndValidateProperties(_request, Response.ArgumentErrors);
			ValidateRequestCustom();
			return Response.ArgumentErrors.Count==0;
		}
	}
}