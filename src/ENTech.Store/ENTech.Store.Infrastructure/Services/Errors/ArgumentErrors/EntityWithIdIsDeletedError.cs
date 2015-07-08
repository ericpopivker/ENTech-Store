
using System;
using System.Data.Entity.Core.Metadata.Edm;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services.Errors.ArgumentErrors
{
	public class EntityWithIdIsDeletedError : ArgumentError
	{
		private const string _errorMessageTemplate = "{0} was deleted";

		public string EntityType { get; internal set; }

		public EntityWithIdIsDeletedError(string entityType)
			: base(CommonArgumentErrorCode.EntityWithIdIsDeleted)
		{
			EntityType = entityType;
		}


		protected override string ErrorMessageTemplate
		{
			get { return String.Format(_errorMessageTemplate, EntityType); }
		}
	}
}
