using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ENTech.Store.Infrastructure.Helpers;

namespace ENTech.Store.Services.Expandable
{
	public abstract class ExpandProfile<T> where T : IExpandableDto
	{
		private ICollection<ExpandPropertyFluentConfiguration<T>> _propertyConfigurations =
			new Collection<ExpandPropertyFluentConfiguration<T>>();

		private ExpandRootFluentConfiguration<T> _rootFluentConfiguration;

		protected ExpandRootFluentConfiguration<T> LoadRoot()
		{
			var config = new ExpandRootFluentConfiguration<T>();

			_rootFluentConfiguration = config;

			return config;
		}

		protected ExpandPropertyFluentConfiguration<T> ExpandProperty(Expression<Func<T, object>> sourceProperty)
		{
			ExpandPropertyFluentConfiguration<T> propertyFluentConfiguration = new ExpandPropertyFluentConfiguration<T>
			{
				Property = sourceProperty
			};

			_propertyConfigurations.Add(propertyFluentConfiguration);

			return propertyFluentConfiguration;
		}

		protected abstract void Configure();

		public ExpandConfiguration GetConfiguration()
		{
			Configure();

			return new ExpandConfiguration
			{
				RootFluentConfiguration = new ExpandRootConfiguration
				{
					GetByIdRequestType = _rootFluentConfiguration.GetByIdRequestType,
					FindByIdsRequestType = _rootFluentConfiguration.FindByIdsRequestType,
					RootType = typeof(T)
				},
				PropertyConfigurations = _propertyConfigurations.Select(x => new ExpandPropertyConfiguration
				{
					TargetProperty = new PropertyMetaData {PropertyInfo = typeof (T).GetProperty(PropertyHelper.GetName(x.Property))},
					SourceProperty =
						new PropertyMetaData {PropertyInfo = typeof (T).GetProperty(PropertyHelper.GetName(x.ExpandIdSourceProperty))},
					GetByIdRequestType = x.RequestsFluentConfiguration.GetByIdRequestType,
					FindByIdsRequestType = x.RequestsFluentConfiguration.FindByIdsRequestType
				})
			};
		}

		protected internal class ExpandUsingCommandsFluentConfiguration<TExpandableDto>
		{
			private Type _getByIdRequestType;
			private Type _findByIdsRequestType;

			public ExpandUsingCommandsFluentConfiguration<TExpandableDto> IfSingle<TGetByIdRequest>()
			{
				_getByIdRequestType = typeof (TGetByIdRequest);
				return this;
			}

			public ExpandUsingCommandsFluentConfiguration<TExpandableDto> IfMultiple<TFindByIdsRequest>()
			{
				_findByIdsRequestType = typeof (TFindByIdsRequest);
				return this;
			}
		}

		protected internal class ExpandRootFluentConfiguration<TExpandableDto>
		{
			private Type _getByIdRequestType;
			private Type _findByIdsRequestType;

			public ExpandRootFluentConfiguration<TExpandableDto> IfSingle<TGetByIdRequest>()
			{
				_getByIdRequestType = typeof (TGetByIdRequest);
				return this;
			}

			public ExpandRootFluentConfiguration<TExpandableDto> IfMultiple<TFindByIdsRequest>()
			{
				_findByIdsRequestType = typeof (TFindByIdsRequest);
				return this;
			}

			internal Type GetByIdRequestType
			{
				get { return _getByIdRequestType; }
			}

			internal Type FindByIdsRequestType
			{
				get { return _findByIdsRequestType; }
			}
		}


		protected internal class ExpandPropertyFluentConfiguration<T> where T : IExpandableDto
		{
			internal Expression<Func<T, object>> Property { get; set; }
			internal Expression<Func<T, object>> ExpandIdSourceProperty { get; set; }
			internal ExpandRootFluentConfiguration<T> RequestsFluentConfiguration { get; set; }

			public ExpandRootFluentConfiguration<T> FromIdentityProperty(Expression<Func<T, object>> func)
			{
				ExpandIdSourceProperty = func;

				RequestsFluentConfiguration = new ExpandRootFluentConfiguration<T>();

				return RequestsFluentConfiguration;
			}
		}
	}

	public class ExpandConfiguration
	{
		public ExpandRootConfiguration RootFluentConfiguration { get; set; }
		public IEnumerable<ExpandPropertyConfiguration> PropertyConfigurations { get; set; }
	}

	public class ExpandPropertyConfiguration : LoadableConfiguration
	{
		public PropertyMetaData TargetProperty { get; set; }
		public PropertyMetaData SourceProperty { get; set; }
	}

	public class ExpandRootConfiguration : LoadableConfiguration
	{
		public Type RootType { get; set; }
	}

	public class LoadableConfiguration
	{
		public Type GetByIdRequestType { get; internal set; }
		public Type FindByIdsRequestType { get; internal set; }
		
	}

	public class PropertyMetaData
	{
		public PropertyInfo PropertyInfo { get; set; }
	}
}