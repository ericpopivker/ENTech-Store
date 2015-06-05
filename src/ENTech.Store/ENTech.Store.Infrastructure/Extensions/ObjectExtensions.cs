using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ENTech.Store.Infrastructure.Extensions
{
	public static class ObjectExtensions
	{
		public static TProp Decode<TObj, TProp>(this TObj obj, Func<TObj, TProp> prop)
			where TObj : class
		{
			return obj == null ? default(TProp) : prop(obj);
		}

		public static TProp Decode<TObj, TProp>(this TObj obj, Func<TObj, TProp> prop, TProp defaultObj)
			where TObj : class
		{
			return obj == null ? defaultObj : prop(obj);
		}

		public static object Decode<TObj, TProp>(this TObj obj, Func<TObj, TProp?> prop, object defaultObj)
			where TProp : struct
		{
			var propValue = prop(obj);

			return propValue.HasValue ? propValue.Value : defaultObj;
		}

		public static object Decode<TObj, TProp>(this TObj obj, Func<TObj, TProp?> prop, Func<TProp, object> decoded, object defaultObj)
			where TProp : struct
		{
			var propValue = prop(obj);

			return propValue.HasValue ? decoded(propValue.Value) : defaultObj;
		}

		public static TObj GetNotNull<TObj>(this TObj obj) where TObj : class
		{
			if (obj == null)
				throw new NullReferenceException();

			return obj;
		}

		public static T GetNotNull<T>(this T? obj)
			where T : struct
		{
			if (obj.HasValue == false)
				throw new NullReferenceException();

			return obj.Value;
		}

		public static bool IsEqualToOrAlsoNull(this object left, object right)
		{
			if (left == null)
				return right == null;

			return left.Equals(right);
		}

		public static MemoryStream SerializeToBinaryStream(this object graph)
		{
			var formatter = new BinaryFormatter();
			var outgoingStream = new MemoryStream();

			formatter.Serialize(outgoingStream, graph);

			outgoingStream.Seek(0, SeekOrigin.Begin);

			return outgoingStream;
		}
	}
}