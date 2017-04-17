﻿using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace Slapper.Tests
{
    [TestClass]
    public class SimplyMapTests : TestBase
    {
        public class PersonWithFields
        {
            public int Id;
            public string FirstName;
            public string LastName;
        }

        public class PersonWithProperties
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [TestMethod]
        public void Can_Map_Matching_Field_Names()
        {
            // Arrange
            const int id = 1;
            const string firstName = "Bob";
            const string lastName = "Smith";

            var dictionary = new Dictionary<string, object>
                                 {
                                     { "Id", id },
                                     { "FirstName", firstName },
                                     { "LastName", lastName }
                                 };

            // Act
            var customer = Slapper.AutoMapper.Map<PersonWithFields>( dictionary );

            // Assert
            Assert.NotNull( customer );
            Assert.That( customer.Id == id );
            Assert.That( customer.FirstName == firstName );
            Assert.That( customer.LastName == lastName );
        }

        [TestMethod]
        public void Can_Map_Matching_Property_Names()
        {
            // Arrange
            const int id = 1;
            const string firstName = "Bob";
            const string lastName = "Smith";

            var dictionary = new Dictionary<string, object>
                                 {
                                     { "Id", id },
                                     { "FirstName", firstName },
                                     { "LastName", lastName }
                                 };

            // Act
            var customer = Slapper.AutoMapper.Map<PersonWithProperties>( dictionary );

            // Assert
            Assert.NotNull( customer );
            Assert.That( customer.Id == id );
            Assert.That( customer.FirstName == firstName );
            Assert.That( customer.LastName == lastName );
        }
    }
}