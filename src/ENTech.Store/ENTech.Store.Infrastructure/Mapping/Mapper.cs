using System;
using System.Collections.Generic;

namespace ENTech.Store.Infrastructure.Mapping
{
	public class Mapper : IMapper
	{
		public TResult Map<TSource, TResult>(TSource source)
		{
			return AutoMapper.Mapper.Map<TResult>(source);
		}

		public IEnumerable<TResult> MapCollection<TSource, TResult>(IEnumerable<TSource> collection)
		{
			return AutoMapper.Mapper.Map<IEnumerable<TResult>>(collection);
		}

		public object Map(Type sourceType, Type destinationType, object source)
		{
			return AutoMapper.Mapper.Map(source, sourceType, destinationType);
		}
	}
}