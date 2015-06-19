using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.Misc;

namespace ENTech.Store.Services.SharedModule.Requests
{
	public abstract class GetByIdRequestBase<TLoadOptionEnum, TSecurity> : GetByIdRequestBase<TSecurity> 
		where TLoadOptionEnum : struct 
		where TSecurity : ISecurityInformation
	{
		public ICollection<TLoadOptionEnum> LoadOptions { get; set; }

	}

	public abstract class GetByIdRequestBase : GetByIdRequestBase<AnonymousSecurityInformation>
	{
	}

	public abstract class GetByIdRequestBase<TSecurity> : SecureRequestBase<TSecurity> 
		where TSecurity : ISecurityInformation
	{
		[Required(ErrorMessage = RequestValidatorErrorMessage.Required)]
		[Range(0, Int32.MaxValue)]
		public int Id { get; set; }
	}
}
