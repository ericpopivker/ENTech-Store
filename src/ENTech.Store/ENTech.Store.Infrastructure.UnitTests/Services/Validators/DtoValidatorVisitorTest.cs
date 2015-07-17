using System;
using System.Collections.Generic;
using ENTech.Store.Infrastructure.Services;
using ENTech.Store.Infrastructure.Services.Validators;
using FluentValidation;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Infrastructure.UnitTests.Services.Validators
{
	[TestFixture]
	public class DtoValidatorVisitorTest
	{

		private Mock<IDtoValidatorFactory> _dtoValidatorFactorykMock = new Mock<IDtoValidatorFactory>();
	
		
		private CustomerDtoValidatorStub _customerDtoValidator;
		private DtoValidatorVisitor _dtoValidatorVisitor;
		
		
		[SetUp]
		public void SetUp()
		{
			_customerDtoValidator = new CustomerDtoValidatorStub();
			_dtoValidatorVisitor = new DtoValidatorVisitor(_dtoValidatorFactorykMock.Object);

			_dtoValidatorFactorykMock.ResetCalls();

			_dtoValidatorFactorykMock.Setup(factory => factory.TryCreate(It.Is<Type>(type => type == typeof(CustomerDto)))).Returns(new CustomerDtoValidatorStub());
			_dtoValidatorFactorykMock.Setup(factory => factory.TryCreate(It.Is<Type>(type => type == typeof(AddressDto)))).Returns(new AddressDtoValidator());
			_dtoValidatorFactorykMock.Setup(factory => factory.TryCreate(It.Is<Type>(type => type == typeof(ChildDto)))).Returns(new ChildDtoValidator());

		}


		[Test]
		public void Validate_When_property_in_top_level_dto_is_invalid_Then_returns_errors_with_correct_argument_names_in_result()
		{
			var customerDto = new CustomerDtoBuilder()
								.WithFirstName(null)
								.Build();

			var result = _dtoValidatorVisitor.Validate(customerDto);

			Assert.IsFalse(result.IsValid);
			Assert.IsTrue(result.ArgumentErrors[0].ArgumentName == "FirstName");
		}


		[Test]
		public void Validate_When_two_properties_in_top_level_dto_is_invalid_Then_returns_errors_with_correct_argument_names_in_result()
		{
			var customerDto = new CustomerDtoBuilder()
								.WithFirstName(null)
								.WithLastName(null)
								.Build();

			var result = _dtoValidatorVisitor.Validate(customerDto);

			Assert.IsFalse(result.IsValid);

			Assert.IsTrue(result.ArgumentErrors[0].ArgumentName == "FirstName");
			Assert.IsTrue(result.ArgumentErrors[1].ArgumentName == "LastName");
		}



		[Test]
		public void Validate_When_property_on_second_level_is_invalid_Then_returns_error_with_correct_argument_name_in_result()
		{
			var customerDto = new CustomerDtoBuilder()
								.Build();

			customerDto.Address.Street = null;
			var result = _dtoValidatorVisitor.Validate(customerDto);

			Assert.IsFalse(result.IsValid);

			Assert.IsTrue(result.ArgumentErrors[0].ArgumentName == "Address.Street");
		}




		[Test]
		public void Validate_When_enumerable_property__is_invalid_Then_returns_error_with_correct_argument_name_in_result()
		{
			var customerDto = new CustomerDtoBuilder()
								.Build();

			customerDto.Children = new List<ChildDto>
			{
				new ChildDto {FirstName = null, Age = 5},
				new ChildDto {FirstName = null, Age = 6}
			};

			var result = _dtoValidatorVisitor.Validate(customerDto);

			Assert.IsFalse(result.IsValid);

			Assert.IsTrue(result.ArgumentErrors[0].ArgumentName == "Children[0].FirstName");
			Assert.IsTrue(result.ArgumentErrors[1].ArgumentName == "Children[1].FirstName");

		}



		[Test]
		public void Validate_When_properties_on_each_level_are_invalid_Then_returns_error_with_correct_argument_name_in_result()
		{
			var customerDto = new CustomerDtoBuilder()
								.WithFirstName(null)
								.Build();

			customerDto.Address.Street = null;
			
			customerDto.Children = new List<ChildDto>
			{
				new ChildDto {FirstName = null, Age = 5},
				new ChildDto {FirstName = null, Age = 6}
			};
			
			var result = _dtoValidatorVisitor.Validate(customerDto);

			Assert.IsFalse(result.IsValid);

			Assert.IsTrue(result.ArgumentErrors[0].ArgumentName == "FirstName");
			Assert.IsTrue(result.ArgumentErrors[1].ArgumentName == "Address.Street");
			Assert.IsTrue(result.ArgumentErrors[2].ArgumentName == "Children[0].FirstName");
			Assert.IsTrue(result.ArgumentErrors[3].ArgumentName == "Children[1].FirstName");
			
		}



		private class CustomerDtoValidatorStub : DtoValidatorBase<CustomerDto>
		{

			public CustomerDtoValidatorStub()
			{
				RuleFor(dto => dto.FirstName).NotEmpty();
				RuleFor(dto => dto.LastName).NotEmpty();
			}
		}



		private class AddressDtoValidator : DtoValidatorBase<AddressDto>
		{

			public AddressDtoValidator()
			{
				RuleFor(dto => dto.Street).NotEmpty();

				RuleFor(dto => dto.City).NotEmpty();
			}
		}



		private class ChildDtoValidator : DtoValidatorBase<ChildDto>
		{

			public ChildDtoValidator()
			{
				RuleFor(dto => dto.FirstName).NotEmpty();
			}
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
					FirstName=_firstName,
					LastName = _lastName,

					Address = new AddressDto
					{
						Street = "Main Street",
						City ="New York"
					}
				};

				return custmerDto;

			}
		}

		private class CustomerDto
		{
			public string FirstName { get; set; }
		
			public string LastName { get; set; }

			public DateTime DateOfBirth { get; set; }

			public AddressDto Address { get; set; }

			public List<ChildDto> Children { get; set; }

		}

		private class ChildDto
		{
			public string FirstName { get; set; }
		
			public int Age { get; set; }
		}


		private class AddressDto
		{
			public string Street { get; set; }
		
			public string City { get; set; }
	
		}

		
	}



	
}
