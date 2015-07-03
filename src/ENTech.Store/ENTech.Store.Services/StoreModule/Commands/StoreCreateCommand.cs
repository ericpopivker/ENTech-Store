﻿using System;
using ENTech.Store.Entities;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreCreateCommand : DbContextCommandBase<StoreCreateRequest, StoreCreateResponse>
	{
		public StoreCreateCommand(IUnitOfWork unitOfWork, IDtoValidatorFactory dtoValidatorFactory)
			: base(unitOfWork.DbContext, dtoValidatorFactory, false)
		{
		}

		public override StoreCreateResponse Execute(StoreCreateRequest request)
		{
			return new StoreCreateResponse();
		}
	}
}