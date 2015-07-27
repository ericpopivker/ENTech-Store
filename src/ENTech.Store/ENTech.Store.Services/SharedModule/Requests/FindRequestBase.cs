using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Services.SharedModule.Dtos;

namespace ENTech.Store.Services.SharedModule.Requests
{
	public abstract class FindRequestBase<TResponse, TLoadOptionEnum, TSortField, TFindCriteriaDto> : IRequest<TResponse>
		where TResponse : IResponse
		where TLoadOptionEnum : struct
		where TSortField : struct
		where TFindCriteriaDto : FindCriteriaDtoBase<TSortField> 
	{
		public ICollection<TLoadOptionEnum> LoadOptions { get; set; }

		[Required]
		public TFindCriteriaDto Criteria { get; set; }
	}
}
