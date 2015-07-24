using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.Misc;
using ENTech.Store.Services.SharedModule.Dtos;

namespace ENTech.Store.Services.SharedModule.Requests
{
	public abstract class FindRequestBase<TLoadOptionEnum, TSortField, TFindCriteriaDto, TSecurity> : SecureRequestBase<TSecurity> 
		where TLoadOptionEnum : struct
		where TSortField : struct
		where TFindCriteriaDto : FindCriteriaDtoBase<TSortField> 
		where TSecurity : ISecurityInformation
	{
		public ICollection<TLoadOptionEnum> LoadOptions { get; set; }

		[Required]
		public TFindCriteriaDto Criteria { get; set; }
	}
	
	public abstract class FindRequestBase<TLoadOptionEnum, TSortField, TFindCriteriaDto> : FindRequestBase<TLoadOptionEnum, TSortField, TFindCriteriaDto, AnonymousSecurityInformation>
		where TLoadOptionEnum : struct
		where TSortField : struct
		where TFindCriteriaDto : FindCriteriaDtoBase<TSortField>
	{
	}
}
