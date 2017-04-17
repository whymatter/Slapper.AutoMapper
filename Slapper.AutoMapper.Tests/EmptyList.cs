﻿using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace Slapper.Tests
{
    [TestClass]
    public class EmptyListe : TestBase
    {
        public class Customer
        {
            public int Id;
            public string FirstName;
            public string LastName;
            public IList<Order> Orders;
        }

        public class Order
        {
            public int Id;
            public decimal OrderTotal;
        }

        [TestMethod]
        public void Can_Handle_Mapping_An_Empty_List()
        {
            // Arrange
            dynamic dynamicCustomer = new ExpandoObject();
            dynamicCustomer.Id = 1;
            dynamicCustomer.FirstName = "Bob";
            dynamicCustomer.LastName = "Smith";
            dynamicCustomer.Orders_Id = null;
            dynamicCustomer.Orders_OrderTotal = null;

            // Act
            var customer = Slapper.AutoMapper.MapDynamic<Customer>( dynamicCustomer ) as Customer;

            // Assert
            Assert.NotNull( customer );

            // Empty list
            Assert.That(customer.Orders != null);
            Assert.That(customer.Orders.Count == 0);
        }

    }
}