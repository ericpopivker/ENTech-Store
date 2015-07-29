using System;
using ENTech.Store.Infrastructure.Expandable;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Responses.Statuses;
using ENTech.Store.Services.CommandService.Definition;

namespace ENTech.Store.Services.Expandable
{
	public abstract class DtoServiceLoader<TServiceResponse, TServiceDto, T> : IDtoLoader<T>
		where TServiceResponse : GetByIdResponse<TServiceDto>, new()
	{
		private readonly IExternalCommandService _externalCommandService;
		private readonly IMapper _mapper;

		public DtoServiceLoader(IExternalCommandService externalCommandService, IMapper mapper)
		{
			_externalCommandService = externalCommandService;
			_mapper = mapper;
		}

		public T Load(int id)
		{
			var request = GetLoadRequest();

			var data = _externalCommandService.Execute(request);

			if (data == null || data is ErrorResponseStatus<TServiceResponse>)
				throw new Exception("Expander failed to load data");

			var okResponse = data as OkResponseStatus<TServiceResponse>;
			var item = okResponse.Response.Item;

			return _mapper.Map<TServiceDto, T>(item);
		}

		protected abstract IRequest<TServiceResponse> GetLoadRequest();
	}
}