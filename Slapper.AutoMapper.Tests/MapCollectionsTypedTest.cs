﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Slapper.Tests
{
    using NUnit.Framework;

    [TestClass]
    public class MapCollectionsTypedTest : TestBase
    {
        public class Customer
        {
            public int CustomerId;
            public IList<int> OrdersIds;
        }

        public class CustomerNames
        {
            public int CustomerNamesId;
            public IList<string> Names;
        }

        [TestMethod]
        public void I_Can_Map_Value_PrimitiveTyped_Collection()
        {
            // Arrange
            var dictionary = new Dictionary<string, object>
                         {
                             { "CustomerId", 1 },
                             { "OrdersIds_$", 3 },
                         };

            var dictionary2 = new Dictionary<string, object>
                         {
                             { "CustomerId", 1 },
                             { "OrdersIds_$", 5 }
                         };

            var list = new List<IDictionary<string, object>> { dictionary, dictionary2 };

            // Act
            var customers = Slapper.AutoMapper.MapDynamic<Customer>(list).ToList();

            // Assert

            // There should only be a single customer
            Assert.That(customers.Count == 1);

            // There should be two values in OrdersIds, with the correct values
            Assert.That(customers.Single().OrdersIds.Count == 2);
            Assert.That(customers.Single().OrdersIds[0] == 3);
            Assert.That(customers.Single().OrdersIds[1] == 5);
        }


        [TestMethod]
        public void I_Can_Map_Value_SpecialStringTyped_Collection()
        {
            // Arrange
            var dictionary = new Dictionary<string, object>
                         {
                             { "CustomerNamesId", 1 },
                             { "Names_$", "Name 1" },
                         };

            var dictionary2 = new Dictionary<string, object>
                         {
                             { "CustomerNamesId", 1 },
                             { "Names_$", "Name 2" }
                         };

            var list = new List<IDictionary<string, object>> { dictionary, dictionary2 };

            // Act
            var customers = Slapper.AutoMapper.MapDynamic<CustomerNames>(list).ToList();

            // Assert

            // There should only be a single customer
            Assert.That(customers.Count == 1);

            // There should be two values in OrdersIds, with the correct values
            Assert.That(customers.Single().Names.Count == 2);
            Assert.That(customers.Single().Names[0] == "Name 1");
            Assert.That(customers.Single().Names[1] == "Name 2");
        }

        public class Merchant
        {
            public Merchant()
            {
                Addresses = new HashSet<MerchantAddress>();
            }

            public long Id { set; get; }
            public string Name { set; get; }

            public ICollection<MerchantAddress> Addresses { set; get; }
        }

        public class MerchantAddress
        {
            public long Id { set; get; }
            public string AddressLine { set; get; }
            public long MerchantId { set; get; }
        }

        [TestMethod]
        public void I_Can_Map_Any_Typed_ICollection()
        {
            // this strings was received from database (or another flat storage)
            List<Dictionary<string, object>> flat = new List<Dictionary<string, object>>()
            {
                new Dictionary<string, object>()
                {
                    { "Id", 1 } ,
                    {"Name", "Merchant name" } ,
                    { "Addresses_Id", 1} ,
                    { "Addresses_AddressLine", "Address line 1"} ,
                    { "Addresses_MerchantId", 1}
                },
                new Dictionary<string, object>()
                {
                    { "Id", 1 } ,
                    {"Name", "Merchant name" } ,
                    { "Addresses_Id", 2} ,
                    { "Addresses_AddressLine", "Address line 2"} ,
                    { "Addresses_MerchantId", 1}
                },
                new Dictionary<string, object>()
                {
                    { "Id", 1 } ,
                    {"Name", "Merchant name" } ,
                    { "Addresses_Id", 3} ,
                    { "Addresses_AddressLine", "Address line 3"} ,
                    { "Addresses_MerchantId", 1}
                },
            };
            Slapper.AutoMapper.Configuration.AddIdentifiers(typeof(Merchant), new [] { "Id" });
            Slapper.AutoMapper.Configuration.AddIdentifiers(typeof(MerchantAddress), new[] { "Id" });
            var result = Slapper.AutoMapper.MapDynamic<Merchant>(flat);
            Assert.That(result.Count() == 1);
            var merchant = result.First();
            Assert.That(merchant.Addresses.Count == 3);
            Assert.AreEqual("Address line 1", merchant.Addresses.First().AddressLine);
            Assert.AreEqual("Address line 2", merchant.Addresses.Skip(1).First().AddressLine);
            Assert.AreEqual("Address line 3", merchant.Addresses.Skip(2).First().AddressLine);
        }

    }
}
