using System.Collections.Generic;
using ENTech.Store.Infrastructure.Helpers;
using NUnit.Framework;

namespace ENTech.Store.Infrastructure.UnitTests.Helpers
{
	[TestFixture]
	public class PropertyHelperTest
	{
		public class Address
		{
			public string Street1 { get; set; }
		}

		public class Order
		{
			public int Amount { get; set; }
		}

		public class OrderHistory
		{
			public List<Order> Orders { get; set; } 
		}

		public class Customer
		{
			
			public string FirstName { get; set; }

			public Address Address { get; set; }

			public OrderHistory OrderHistory { get; set; }

		}

		[Test]
		public void GetName_When_passed_customer_FirstName_expression_Then_returns_name()
		{
			string propertyName = PropertyHelper.GetName<Customer>(c => c.FirstName);

			Assert.AreEqual("FirstName", propertyName);
		}


		[Test]
		public void GetName_When_passed_customer_adress_street1_expression_Then_returns_name()
		{
			string propertyName = PropertyHelper.GetName<Customer>(c => c.Address.Street1);

			Assert.AreEqual("Address.Street1", propertyName);
		}


		[Test]
		public void GetName_When_passed_customer_first_order_amount_expression_Then_returns_name()
		{
			string propertyName = PropertyHelper.GetName<Customer>(c => c.OrderHistory.Orders[0].Amount);

			Assert.AreEqual("OrderHistory.Orders[0].Amount", propertyName);
		} 
	}
}
