using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTech.Store.Infrastructure.Services;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Errors.ArgumentErrors;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Infrastructure.UnitTests.Services.Commands;
using FluentValidation;
using NUnit.Framework;

namespace ENTech.Store.Infrastructure.UnitTests.Services.Validators
{
	[TestFixture]
	public class DtoValidatorTest
	{

		[Test]
		public void Validate_When_fluent_validation_fails_on_required_field_Then_error_is_returned()
		{
			var dto = new CustomerDtoBuilder().WithFirstName(null).Build();

			CustomerDtoValidator dtoValidator = new CustomerDtoValidator();
			var result = dtoValidator.Validate(dto);

			Assert.IsFalse(result.IsValid);
			Assert.AreEqual(1, result.ArgumentErrors.Count);
			Assert.IsTrue(result.ArgumentErrors[0].ArgumentError is RequiredArgumentError);
		}



		[Test]
		public void Validate_When_fluent_validation_fails_on_required_field_on_dependent_object_Then_no_error_is_returned()
		{
			var dto = new CustomerDtoBuilder().Build();
			dto.Address.Street = null;

			CustomerDtoValidator dtoValidator = new CustomerDtoValidator();
			var result = dtoValidator.Validate(dto);

			Assert.IsTrue(result.IsValid);
		}



		[Test]
		public void Validate_When_fluent_validation_fails_on_required_field_Then_the_error_is_passed_to_ValidateInternal()
		{
			var dto = new CustomerDtoBuilder().WithFirstName(null).Build();
			dto.Address.Street = null;

			CustomerDtoValidator dtoValidator = new CustomerDtoValidator();
			var result = dtoValidator.Validate(dto);

			Assert.IsTrue(dtoValidator.FirstNameRequiredErrorIsPassedToValidateInternal);
		}



		[Test]
		public void Validate_When_ValidateInternal_creates_error_Then_error_returned_from_Validate()
		{
			var dto = new CustomerDtoBuilder().WithFirstName("aaaaa").Build();
			dto.Address.Street = null;

			CustomerDtoValidator dtoValidator = new CustomerDtoValidator();
			var result = dtoValidator.Validate(dto);

			Assert.IsTrue(result.ArgumentErrors[0].ArgumentError is ArgumentErrorStub);
		}

		private class CustomerDtoValidator : DtoValidatorBase<CustomerDto>
		{
			public bool FirstNameRequiredErrorIsPassedToValidateInternal { get; set; }

			public CustomerDtoValidator()
			{
				RuleFor(dto => dto.FirstName).NotEmpty();
				RuleFor(dto => dto.LastName).NotEmpty();
			}

			protected override void ValidateInternal(CustomerDto dto, DtoValidatorResult<CustomerDto> dtoValidatorResult)
			{
				FirstNameRequiredErrorIsPassedToValidateInternal = dtoValidatorResult.AnyErrorsForArgument(dt => dt.FirstName);

				if (dtoValidatorResult.NoErrorsForArgument(dt => dt.FirstName))
				{
					if (dto.FirstName.StartsWith("a"))
						dtoValidatorResult.AddArgumentError(dt => dt.FirstName, new ArgumentErrorStub());
				}
			}
		}



		public class ArgumentErrorStub : ArgumentError
		{
			public ArgumentErrorStub()
				: base(1)
			{
			}

			protected override string ErrorMessageTemplate
			{
				get { return "ArgumentErrorStub"; }
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
					FirstName = _firstName,
					LastName = _lastName,

					Address = new AddressDto
					{
						Street = "Main Street",
						City = "New York"
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
