using System;
using System.Collections.Generic;

namespace ENTech.Store.Infrastructure.Mapping
{
	public interface IMapper
	{
		TResult Map<TSource, TResult>(TSource source);
		IEnumerable<TTarget> MapCollection<TSource, TTarget>(IEnumerable<TSource> collection);
		object Map(Type sourceType, Type destinationType, object source);
	}
}