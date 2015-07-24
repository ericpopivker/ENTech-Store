using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ENTech.Store.Services.UnitTests
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
