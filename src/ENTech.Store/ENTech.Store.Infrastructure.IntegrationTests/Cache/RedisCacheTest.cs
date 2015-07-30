using System;
using System.Collections.Generic;
using ENTech.Store.Infrastructure.Cache;
using ENTech.Store.Infrastructure.Services;
using NUnit.Framework;
using ProtoBuf;

namespace ENTech.Store.Infrastructure.IntegrationTests.Cache
{
	[TestFixture]
	public class RedisCacheTest
	{

		private RedisCache _redisCache;

		[SetUp]
		public void SetUp()
		{
		}

		[TearDown]
		public void TearDown()
		{	
			_redisCache.RemoveAll();
		}


		public RedisCacheTest()
		{
			_redisCache = new RedisCache();
		}


		[Test]
		public void Set_When_set_text_and_get_Then_works()
		{
			_redisCache.Set("Key1", "Value1");

			string value = _redisCache.Get<string>("Key1");

			Assert.AreEqual("Value1", value);
		}

		//Can do it like: http://stackoverflow.com/questions/12308196/protobuf-net-serialization-without-annotation
		[Test]
		public void Set_When_set_simple_entity_and_get_Then_works()
		{
			var customerDto = new CustomerDtoBuilder().Build();
			_redisCache.Set("Key1", customerDto);

			var customer2Dto  = _redisCache.Get<CustomerDto>("Key1");

			Assert.AreEqual(customerDto.FirstName, customer2Dto.FirstName);
			Assert.AreEqual(customerDto.Children.Count, customer2Dto.Children.Count);
			Assert.AreEqual(customerDto.Children[0].Age, customer2Dto.Children[0].Age);

		}



		[Test]
		public void FindByIds_When_key_does_not_exist_Then_returns_null_for_that_key()
		{
			_redisCache.Set("Key1", "Value1");

			var dic = _redisCache.FindByKeys<string>(new [] {"Key1", "Key2"});

			Assert.AreEqual("Value1", dic["Key1"]);
			Assert.AreEqual(null, dic["Key2"]);
		}



		private class CustomerDtoBuilder : BuilderBase<CustomerDto, CustomerDtoBuilder>
		{
			private string _firstName;
			private string _lastName;

			public CustomerDtoBuilder()
			{
				_firstName = "Josh";
				_lastName = "Smith";
			}

			public CustomerDtoBuilder WithFirstName(string firstName)
			{
				_firstName = firstName;

				return this;
			}

			public CustomerDtoBuilder WithLastName(string lastName)
			{
				_lastName = lastName;

				return this;
			}

			public override CustomerDto Build()
			{
				var custmerDto = new CustomerDto
				{
					FirstName = _firstName,
					LastName = _lastName,

					Address = new AddressDto
					{
						Street = "Main Street",
						City = "New York"
					},

					Children = new List<ChildDto>
					{
						new ChildDto{FirstName ="Leonardo", Age=3},
						new ChildDto{FirstName ="Evelyne", Age =1}
					}
				};

				return custmerDto;

			}
		}


		[ProtoContract]
		private class CustomerDto
		{
			[ProtoMember(1)]
			public string FirstName { get; set; }

			[ProtoMember(2)]
			public string LastName { get; set; }

			[ProtoMember(3)]
			public DateTime DateOfBirth { get; set; }

			[ProtoMember(4)]
			public AddressDto Address { get; set; }

			[ProtoMember(5)]
			public IList<ChildDto> Children { get; set; }

		}

		[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
		private class ChildDto
		{
			public string FirstName { get; set; }

			public int Age { get; set; }
		}

		[ProtoContract]
		private class AddressDto
		{
			[ProtoMember(1)]
			public string Street { get; set; }

			[ProtoMember(2)]
			public string City { get; set; }

		}

	}
}
