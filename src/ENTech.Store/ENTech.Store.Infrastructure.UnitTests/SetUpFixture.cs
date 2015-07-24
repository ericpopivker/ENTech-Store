using NUnit.Framework;

namespace ENTech.Store.Infrastructure.UnitTests
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
