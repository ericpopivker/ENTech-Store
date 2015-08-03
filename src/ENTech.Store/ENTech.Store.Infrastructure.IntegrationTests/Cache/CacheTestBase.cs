using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTech.Store.Infrastructure.Cache;
using ENTech.Store.Infrastructure.Services;
using NUnit.Framework;
using ProtoBuf;

namespace ENTech.Store.Infrastructure.IntegrationTests.Cache
{
	[TestFixture]
	public abstract class CacheTestBase 
	{
		private ICache _cache;

		[SetUp]
		public void SetUp()
		{
		}

		[TearDown]
		public void TearDown()
		{
			_cache.RemoveAll();
		}


		public CacheTestBase(ICache cache)
		{
			_cache = cache;
		}


		[Test]
		public void Set_When_set_text_and_get_Then_works()
		{
			_cache.Set("Key1", "Value1");

			string value = _cache.Get<string>("Key1");

			Assert.AreEqual("Value1", value);
		}

		//Can do it like: http://stackoverflow.com/questions/12308196/protobuf-net-serialization-without-annotation
		[Test]
		public void Set_When_set_simple_entity_and_get_Then_works()
		{
			var customerDto = new CustomerDtoBuilder().Build();
			_cache.Set("Key1", customerDto);

			var customer2Dto = _cache.Get<CustomerDto>("Key1");

			Assert.AreEqual(customerDto.FirstName, customer2Dto.FirstName);
			Assert.AreEqual(customerDto.Children.Count, customer2Dto.Children.Count);
			Assert.AreEqual(customerDto.Children[0].Age, customer2Dto.Children[0].Age);

		}

		[Test]
		public void FindByKeys_When_keys_exist_Then_returns_objects_with_correct_data()
		{
			var firstId = 1;
			var firstCustomerDto = new SuspiciousDto
			{
				Id = firstId,
				Name = "Test",
				CategoryId = 1,
				CreatedAt = DateTime.UtcNow,
				LastUpdatedAt = DateTime.UtcNow
			};
			var firstKey = string.Format("Suspicious_{0}", firstId);
			
			
			var secondId = 128; // 128 is the first key that fails to deserialize. Is he deserializing it as byte?!
			var secondCustomerDto = new SuspiciousDto
			{
				Id = secondId,
				Name = "Test",
				CategoryId = 1,
				CreatedAt = DateTime.UtcNow,
				LastUpdatedAt = DateTime.UtcNow
			};
			var secondKey = string.Format("Suspicious_{0}", secondId);
			_cache.Set(new List<Tuple<string, SuspiciousDto>>
			{
				new Tuple<string, SuspiciousDto>(firstKey, firstCustomerDto),
				new Tuple<string, SuspiciousDto>(secondKey, secondCustomerDto)
			});

			var result = _cache.FindByKeys<SuspiciousDto>(new[] { firstKey, secondKey });

			var firstItem = result[firstKey];
			Assert.AreEqual(firstCustomerDto.Id, firstItem.Id);

			var secondItem = result[secondKey];
			Assert.AreEqual(secondCustomerDto.Id, secondItem.Id);
		}


		[Test]
		public void FindByIds_When_key_does_not_exist_Then_returns_null_for_that_key()
		{
			_cache.Set("Key1", "Value1");

			var dic = _cache.FindByKeys<string>(new[] { "Key1", "Key2" });

			Assert.AreEqual("Value1", dic["Key1"]);
			Assert.AreEqual(null, dic["Key2"]);
		}



		private class CustomerDtoBuilder : BuilderBase<CustomerDto, CustomerDtoBuilder>
		{
			private int _id;
			private string _firstName;
			private string _lastName;

			public CustomerDtoBuilder()
			{
				_id = 1;
				_firstName = "Josh";
				_lastName = "Smith";
			}

			public CustomerDtoBuilder WithId(int id)
			{
				_id = id;

				return this;
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
					Id = _id,
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
		private class SuspiciousDto
		{
			[ProtoMember(1)]
			public int Id { get; set; }

			[ProtoMember(2)]
			public string Name { get; set; }

			[ProtoMember(3)]
			public int CategoryId { get; set; }

			public DateTime CreatedAt { get; set; }

			public DateTime LastUpdatedAt { get; set; }
		}

		[ProtoContract]
		private class CustomerDto
		{
			[ProtoMember(1)]
			public int Id { get; set; }

			[ProtoMember(2)]
			public string FirstName { get; set; }

			[ProtoMember(3)]
			public string LastName { get; set; }

			[ProtoMember(4)]
			public DateTime DateOfBirth { get; set; }

			[ProtoMember(5)]
			public AddressDto Address { get; set; }

			[ProtoMember(6)]
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
