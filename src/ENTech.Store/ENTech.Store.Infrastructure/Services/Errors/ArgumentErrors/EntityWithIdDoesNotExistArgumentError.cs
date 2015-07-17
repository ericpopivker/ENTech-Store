
using System;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services.Errors.ArgumentErrors
{
	public class EntityWithIdDoesNotExist : ArgumentError
	{
		private const string _errorMessageTemplate = "{0} does not exist";

		public string EntityType { get; internal set; }


		public EntityWithIdDoesNotExist(string entityType)
			: base(CommonArgumentErrorCode.EntityWithIdDoesNotExist)
		{
			EntityType = entityType;
		}


		protected override string ErrorMessageTemplate
		{
			get { return String.Format(_errorMessageTemplate, EntityType); }
		}
	}
}
