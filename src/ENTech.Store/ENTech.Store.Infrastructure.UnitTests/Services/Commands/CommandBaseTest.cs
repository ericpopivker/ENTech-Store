using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Errors.ResponseErrors;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Infrastructure.UnitTests.Services.Commands
{

	[TestFixture]
	public class CommandBaseTest
	{
		private Mock<IDtoValidatorFactory> _dtoValidatorFactorykMock = new Mock<IDtoValidatorFactory>();
	
		

		[Test]
		public void Validate_When_called_Then_ValidateRequest_is_called_first()
		{
			var command = new CommandStub(_dtoValidatorFactorykMock.Object, failValidateRequest: true, failValidateOperation: true);
			var validateResult = command.Validate(new RequestStub());

			Assert.IsFalse(validateResult.IsValid);
			Assert.IsTrue(validateResult.ResponseError is InvalidArgumentsResponseError);
			Assert.IsTrue(((InvalidArgumentsResponseError)validateResult.ResponseError).ArgumentErrors[0].ArgumentError is ArgumentErrorStub);
		}


		[Test]
		public void Validate_When_ValidateRequest_is_valid_Then_ValidateOperation_is_called()
		{
			var command = new CommandStub(_dtoValidatorFactorykMock.Object, failValidateRequest: false, failValidateOperation: true);
			var validateResult = command.Validate(new RequestStub());

			Assert.IsFalse(validateResult.IsValid);
			Assert.IsTrue(validateResult.ResponseError is ResponseErrorStub);
		}


		[Test]
		public void Validate_When_ValidateRequest_is_valid_and_ValidateOperation_is_valid_Then_Validate_is_valid()
		{
			var command = new CommandStub(_dtoValidatorFactorykMock.Object, failValidateRequest: false, failValidateOperation: false);
			var validateResult = command.Validate(new RequestStub());

			Assert.IsTrue(validateResult.IsValid);
		}



		public class RequestStub : IRequest
		{
			public string UserToken { get; set; }

			public string ApiKey { get; set; }

			public string SomeValue { get; set; }
		}

		public class ResponseStub : IResponse
		{

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


		public class ResponseErrorStub : ResponseError
		{
			public ResponseErrorStub()
				: base(2)
			{
			}

			protected override string ErrorMessageTemplate
			{
				get { return "ResponseErrorStub"; }
			}
		}


		public class CommandStub : CommandBase<RequestStub, ResponseStub>
		{
			public bool _failValidateRequest;
			public bool _failValidateOperation;

			public CommandStub(IDtoValidatorFactory dtoValidatorFactory, bool failValidateRequest, bool failValidateOperation)
				: base(dtoValidatorFactory, false)
			{
				_failValidateRequest = failValidateRequest;
				_failValidateOperation = failValidateOperation;
			}

			protected override void ValidateRequestInternal(RequestStub request, ValidateRequestResult<RequestStub> validateRequestResult)
			{
				if (_failValidateRequest)
					validateRequestResult.AddArgumentError(req => req.SomeValue, new ArgumentErrorStub());
			}


			protected override ValidateOperationResult ValidateOperationInternal(RequestStub request)
			{
				if (_failValidateOperation)
					return ValidateOperationResult.Invalid(new ResponseErrorStub());

				return ValidateOperationResult.Valid();
			}


			public override ResponseStub Execute(RequestStub request)
			{
				throw new System.NotImplementedException();
			}
		}
	}
}
