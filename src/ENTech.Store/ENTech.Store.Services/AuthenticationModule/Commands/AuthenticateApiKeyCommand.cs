﻿using System.Linq;
using AutoMapper;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.AuthenticationModule.Dtos;
using ENTech.Store.Services.AuthenticationModule.Errors;
using ENTech.Store.Services.AuthenticationModule.Errors.ResponseErrors;
using ENTech.Store.Services.AuthenticationModule.Responses;
using ENTech.Store.Services.SharedModule.Commands;

namespace ENTech.Store.Services.AuthenticationModule.Commands
{
	public class AuthenticateApiKeyCommand<TRequest> : DbContextCommandBase<TRequest, AuthenticateApiKeyResponse> 
		where TRequest : IRequest
	{
		public AuthenticateApiKeyCommand(IUnitOfWork unitOfWork, IDtoValidatorFactory dtoValidatorFactory)
			: base(unitOfWork.DbContext, dtoValidatorFactory, false)
		{
		}

		public override AuthenticateApiKeyResponse Execute(TRequest request)
		{
			var apiKey = request.ApiKey;
			if (string.IsNullOrEmpty(apiKey))
			{
				return new AuthenticateApiKeyResponse
				{
					IsAuthenticated = false,
					Error = new ApiKeyRequiredResponseError()
				};
			}

			var partner =  DbContext.Partners.FirstOrDefault(x => x.Key == apiKey);
			if (partner == null)
				return new AuthenticateApiKeyResponse
				{
					IsAuthenticated = false,
					Error = new ApiKeyInvalidResponseError()
				};

			return new AuthenticateApiKeyResponse
			{
				IsAuthenticated = true,
				Partner = Mapper.Map<PartnerDto>(partner)
			};
		}
	}
}