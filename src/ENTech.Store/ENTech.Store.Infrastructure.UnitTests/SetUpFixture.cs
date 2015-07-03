using NUnit.Framework;

namespace ENTech.Store.Infrastructure.Tests
{
	[SetUpFixture]
	public class MySetUpClass
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
