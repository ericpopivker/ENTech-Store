using NUnit.Framework;

namespace ENTech.Store.Infrastructure.IntegrationTests
{
	[SetUpFixture]
	public class SetUpFixture
	{
		[SetUp]
		public void SetUp()
		{
			StartupConfig.RegisterComponents();
		}

		[TearDown]
		public void TearDown()
		{
		}
	}
}
