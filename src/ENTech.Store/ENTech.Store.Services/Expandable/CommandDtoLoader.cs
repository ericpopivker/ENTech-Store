using System;
using System.Collections.Generic;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Responses.Statuses;
using ENTech.Store.Services.CommandService.Definition;

namespace ENTech.Store.Services.Expandable
{
	public abstract class CommandDtoLoader<TServiceResponse, TServiceMultipleResponse, TServiceDto, T> : IDtoLoader<T>
		where TServiceResponse : GetByIdResponse<TServiceDto>, new()
		where TServiceMultipleResponse : FindResponse<TServiceDto>, new()
	{
		private readonly IExternalCommandService _externalCommandService;
		private readonly IMapper _mapper;

		public CommandDtoLoader(IExternalCommandService externalCommandService, IMapper mapper)
		{
			_externalCommandService = externalCommandService;
			_mapper = mapper;
		}

		public T Load(int id)
		{
			var request = GetLoadRequest(id);

			var data = _externalCommandService.Execute(request);

			if (data == null || data is ErrorResponseStatus<TServiceResponse>)
				throw new Exception("Expander failed to load data");

			var okResponse = data as OkResponseStatus<TServiceResponse>;
			var item = okResponse.Response.Item;

			return _mapper.Map<TServiceDto, T>(item);
		}

		public IEnumerable<T> LoadMultiple(IEnumerable<int> ids)
		{
			var request = GetLoadMultipleRequest(ids);

			var data = _externalCommandService.Execute(request);

			if (data == null || data is ErrorResponseStatus<TServiceMultipleResponse>)
				throw new Exception("Expander failed to load data");

			var okResponse = data as OkResponseStatus<TServiceMultipleResponse>;
			var item = okResponse.Response.Items;

			return _mapper.MapCollection<TServiceDto, T>(item);
		}

		protected abstract IRequest<TServiceMultipleResponse> GetLoadMultipleRequest(IEnumerable<int> ids);

		protected abstract IRequest<TServiceResponse> GetLoadRequest(int id);
	}
}