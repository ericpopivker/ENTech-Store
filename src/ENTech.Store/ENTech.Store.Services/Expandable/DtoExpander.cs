using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses.Statuses;
using ENTech.Store.Services.CommandService.Definition;

namespace ENTech.Store.Services.Expandable
{
	public class DtoExpander : IDtoExpander
	{
		private readonly IDtoExpanderEngine _engine;
		private readonly IExternalCommandService _externalCommandService;
		private readonly IMapper _mapper;

		public DtoExpander(IDtoExpanderEngine engine, IExternalCommandService externalCommandService, IMapper mapper)
		{
			_engine = engine;
			_externalCommandService = externalCommandService;
			_mapper = mapper;
		}

		public T LoadAndExpand<T>(int entityId, IEnumerable<ExpandOption<T>> expandOptions) where T : IExpandableDto
		{
			var getRootCommandResultObject = LoadAndExpandPrivate(_engine.GetConfiguration(typeof(T)), entityId);

			return (T) getRootCommandResultObject;
		}

		private object LoadAndExpandPrivate(ExpandConfiguration configuration, int entityId) 
		{
			var dtos = LoadOne(configuration.RootFluentConfiguration, entityId);
			var getRootCommandResultObject = _mapper.Map(dtos.GetType(), configuration.RootFluentConfiguration.RootType, dtos);
			var propertyConfigs = configuration.PropertyConfigurations;
			foreach (var expandPropertyConfiguration in propertyConfigs)
			{
				var idPropertyInfo = expandPropertyConfiguration.SourceProperty.PropertyInfo;
				var targetPropertyInfo = expandPropertyConfiguration.TargetProperty.PropertyInfo;

				var idPropertyType = idPropertyInfo.PropertyType;
				var targetPropertyType = targetPropertyInfo.PropertyType;

				if (typeof (IEnumerable).IsAssignableFrom(idPropertyType))
				{
					var principalIds = (IEnumerable<int>) idPropertyInfo.GetValue(getRootCommandResultObject);
					if (principalIds != null)
					{
						var listType = typeof (List<>);
						var genericArgs = targetPropertyType.GetGenericArguments();
						var concreteType = listType.MakeGenericType(genericArgs);
						var newList = Activator.CreateInstance(concreteType);

						var addMethod = concreteType.GetMethod("Add");

						var loadedDtos = LoadMultiple(expandPropertyConfiguration, principalIds);

						var elementType = genericArgs.Single();

						var mapperMethod = typeof(IMapper).GetMethod("MapCollection");
						
						var genericMethod = mapperMethod.MakeGenericMethod(loadedDtos.GetType().GetGenericArguments().Single(), elementType);
						
						var loadedPropertyValues = (IEnumerable<object>)genericMethod.Invoke(_mapper, new[] { loadedDtos.AsEnumerable() });

						foreach (var loadedPropertyValue in loadedPropertyValues)
						{
							if (loadedPropertyValue is IExpandableDto)
							{
								var idProperty = loadedPropertyValue.GetType().GetProperty("Id");
								var value = (int) idProperty.GetValue(loadedPropertyValue);
								var propertyConfig = _engine.GetConfiguration(expandPropertyConfiguration.TargetProperty.PropertyInfo.PropertyType.GetGenericArguments().Single());
								var reloadedPropertyValue = LoadAndExpandPrivate(propertyConfig, value);
								var mapped = _mapper.Map(loadedPropertyValue.GetType(), expandPropertyConfiguration.TargetProperty.PropertyInfo.PropertyType.GetGenericArguments().Single(), reloadedPropertyValue);
								addMethod.Invoke(newList, new[] { reloadedPropertyValue });
							}
							else
							{
								addMethod.Invoke(newList, new[] {loadedPropertyValue});
							}
						}

						targetPropertyInfo.SetValue(getRootCommandResultObject, newList);
					}
				}
				else
				{
					var principalId = (int) idPropertyInfo.GetValue(getRootCommandResultObject);

					if (typeof(IExpandableDto).IsAssignableFrom(expandPropertyConfiguration.TargetProperty.PropertyInfo.PropertyType))
					{
						var propertyConfig = _engine.GetConfiguration(expandPropertyConfiguration.TargetProperty.PropertyInfo.PropertyType);
						var objectResult = LoadAndExpandPrivate(propertyConfig, principalId);
						targetPropertyInfo.SetValue(getRootCommandResultObject, objectResult);
					}
					else
					{
						var loadedPropertyValue = LoadOne(expandPropertyConfiguration, principalId);
						var mapped = _mapper.Map(loadedPropertyValue.GetType(), expandPropertyConfiguration.TargetProperty.PropertyInfo.PropertyType, loadedPropertyValue);
						targetPropertyInfo.SetValue(getRootCommandResultObject, mapped);
					}
				}
			}
			return getRootCommandResultObject;
		}

		private object LoadOne(LoadableConfiguration expandConfiguration, int entityId)
		{
			var getByIdRequestType = expandConfiguration.GetByIdRequestType;
			var interfaces = getByIdRequestType.GetInterfaces();
			var genericInterfaceDtoType = interfaces.First(x => x.Name == typeof (IRequest<>).Name);
			var genericResponseType = genericInterfaceDtoType.GetGenericArguments().Single();

			var request = Activator.CreateInstance(getByIdRequestType);

			PropertyInfo pi = getByIdRequestType.GetProperty("Id");
			
			pi.SetValue(request, entityId, null);

			MethodInfo method = typeof(IExternalCommandService).GetMethod("Execute");
			
			MethodInfo generic = method.MakeGenericMethod(genericResponseType);
			
			var result = generic.Invoke(_externalCommandService, new[] { request });

			
			if (result.GetType().GetGenericTypeDefinition() == typeof(ErrorResponseStatus<>))
				throw new Exception("Failed to load root object");

			PropertyInfo responsePropertyInfo = result.GetType().GetProperty("Response");

			var response = responsePropertyInfo.GetValue(result);

			PropertyInfo dtoPropertyInfo = genericResponseType.GetProperty("Item");

			var returnedDto = dtoPropertyInfo.GetValue(response);

			return returnedDto;
		}

		private IEnumerable<object> LoadMultiple(LoadableConfiguration expandConfiguration, IEnumerable<int> entityIds)
		{
			var findByIdsRequestType = expandConfiguration.FindByIdsRequestType;
			var interfaces = findByIdsRequestType.GetInterfaces();
			var genericInterfaceDtoType = interfaces.First(x => x.Name == typeof(IRequest<>).Name);
			var genericResponseType = genericInterfaceDtoType.GetGenericArguments().Single();

			var request = Activator.CreateInstance(findByIdsRequestType);

			PropertyInfo pi = findByIdsRequestType.GetProperty("Ids");

			pi.SetValue(request, entityIds, null);

			MethodInfo method = typeof(IExternalCommandService).GetMethod("Execute");

			MethodInfo generic = method.MakeGenericMethod(genericResponseType);

			var result = generic.Invoke(_externalCommandService, new[] { request });


			if (result.GetType().GetGenericTypeDefinition() == typeof(ErrorResponseStatus<>))
				throw new Exception("Failed to load root object");

			PropertyInfo responsePropertyInfo = result.GetType().GetProperty("Response");

			var response = responsePropertyInfo.GetValue(result);

			PropertyInfo dtoPropertyInfo = genericResponseType.GetProperty("Items");

			var returnedDto = (IEnumerable<object>) dtoPropertyInfo.GetValue(response);

			return returnedDto;
		}
	}
}