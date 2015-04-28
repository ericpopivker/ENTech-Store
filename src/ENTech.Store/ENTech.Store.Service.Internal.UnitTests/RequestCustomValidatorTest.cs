using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ENTech.Store.Infrastructure.Attributes;
using ENTech.Store.Infrastructure.Services;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.Internal.PartnerModule.Validators;
using ENTech.Store.Services.Internal.StoreModule.Validators;
using NUnit.Framework;

namespace ENTech.Store.Services.Internal.UnitTests
{
	[TestFixture]
	public class RequestCustomValidatorTest
	{
		[Test]
		public void RequestCustomValidatorErrorCode_When_all_have_unique_value_Then_test_passed()
		{

			RequestValidatorErrorMessagesDictionary.Register<ProductRequestCustomValidatorErrorCode>();
			RequestValidatorErrorMessagesDictionary.Register<PartnerRequestCustomValidatorErrorCode>();
			RequestValidatorErrorMessagesDictionary.Register<RequestValidatorErrorCode>();
			RequestValidatorErrorMessagesDictionary.Register<CommonErrorCode>();

			var types = new List<Type>();
			types.Add(typeof(ProductRequestCustomValidatorErrorCode));
			types.Add(typeof(PartnerRequestCustomValidatorErrorCode));
			types.Add(typeof(RequestValidatorErrorCode));
			types.Add(typeof (CommonErrorCode));

			var codeConsts =
				types.SelectMany(t => t.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
										.Where(f => f.IsLiteral && f.FieldType == typeof(int) && f.DeclaringType == t)).ToList();

			Assert.AreEqual(codeConsts.Count, codeConsts.Select(s => (int)s.GetValue(null)).Distinct().Count(), "Error codes are not unique");
			Assert.AreEqual(codeConsts.Count, RequestValidatorErrorMessagesDictionary.Instance.Count, "Fetched error codes count is not correct");
			Assert.Pass(codeConsts.Count+" unique error codes");
		}

		[Test]
		public void RequestCustomValidatorErrorCode_When_all_have_stringValueAttribute_Then_test_passed()
		{
			RequestValidatorErrorMessagesDictionary.Register<ProductRequestCustomValidatorErrorCode>();
			RequestValidatorErrorMessagesDictionary.Register<PartnerRequestCustomValidatorErrorCode>();
			RequestValidatorErrorMessagesDictionary.Register<RequestValidatorErrorCode>();
			RequestValidatorErrorMessagesDictionary.Register<CommonErrorCode>();

			var types = new List<Type>();
			types.Add(typeof(ProductRequestCustomValidatorErrorCode));
			types.Add(typeof(PartnerRequestCustomValidatorErrorCode));
			types.Add(typeof(RequestValidatorErrorCode));
			types.Add(typeof(CommonErrorCode));

			var codeConsts =
				types.SelectMany(t => t.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
										.Where(f => f.IsLiteral && f.FieldType == typeof(int) && f.DeclaringType == t)).ToList();

			foreach (var codeConst in codeConsts)
			{
				Assert.IsTrue(codeConst.CustomAttributes
					.SingleOrDefault(s => s.AttributeType == typeof(StringValueAttribute)) != null);
			}

		}

	}
}
