﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

// ReSharper disable InconsistentNaming
namespace Slapper.Tests
{
    [TestClass]
    public class IdentifierTests : TestBase
    {
        public class IdentifierTestModels
        {
            public class Customer
            {
                public int Id;

                public string FirstName;

                public string LastName;
            }

            public class Person
            {
                public int Person_Id;

                public string FirstName;

                public string LastName;
            }

            public class CustomerWithIdAttribute
            {
                [Slapper.AutoMapper.Id]
                public int CustomerId;

                public string FirstName;

                public string LastName;
            }

            public class CustomerWithMultipleIdAttributes
            {
                [Slapper.AutoMapper.Id]
                public int CustomerId;

                [Slapper.AutoMapper.Id]
                public int CustomerType;

                public string FirstName;

                public string LastName;
            }
        }

        [TestMethod]
        public void Can_Add_An_Identifier()
        {
            // Arrange
            const string identifier = "FirstName";

            // Act
            Slapper.AutoMapper.Configuration.AddIdentifier( typeof( IdentifierTestModels.Customer ), identifier );

            var identifiers = Slapper.AutoMapper.InternalHelpers.GetIdentifiers( typeof( IdentifierTestModels.Customer ) );

            // Assert
            Assert.That( identifiers.First() == identifier );
        }

        [TestMethod]
        public void Can_Add_Multiple_Identifiers()
        {
            // Arrange
            var identifierList = new List<string> { "FirstName", "LastName" };

            // Act
            Slapper.AutoMapper.Configuration.AddIdentifiers( typeof( IdentifierTestModels.Customer ), identifierList );

            var identifiers = Slapper.AutoMapper.InternalHelpers.GetIdentifiers( typeof( IdentifierTestModels.Customer ) );

            // Assert
            foreach ( var identifier in identifierList )
            {
                Assert.That( identifiers.Contains( identifier ) );
            }
        }

        [TestMethod]
        public void Can_Use_Default_Conventions_To_Find_An_Identifier()
        {
            // Act
            var identifiers = Slapper.AutoMapper.InternalHelpers.GetIdentifiers( typeof( IdentifierTestModels.Customer ) );

            //Assert
            Assert.That( identifiers.First() == "Id" );
        }

        [TestMethod]
        public void Can_Use_A_Custom_Convention_To_Find_An_Identifier()
        {
            // Act
            Slapper.AutoMapper.Configuration.IdentifierConventions.Add( type => type.Name + "_Id" );

            var identifiers = Slapper.AutoMapper.InternalHelpers.GetIdentifiers( typeof( IdentifierTestModels.Person ) );

            //Assert
            Assert.That( identifiers.First() == "Person_Id" );
        }

        [TestMethod]
        public void Can_Find_An_Identifier_When_A_Field_Or_Property_Has_An_Id_Attribute()
        {
            // Act
            var identifiers = Slapper.AutoMapper.InternalHelpers.GetIdentifiers( typeof( IdentifierTestModels.CustomerWithIdAttribute ) );

            //Assert
            Assert.That( identifiers.First() == "CustomerId" );
        }

        [TestMethod]
        public void Can_Find_Identifiers_When_Multiple_Fields_Or_Properties_Have_An_Id_Attribute()
        {
            // Act
            var identifiers = Slapper.AutoMapper.InternalHelpers.GetIdentifiers( typeof( IdentifierTestModels.CustomerWithMultipleIdAttributes ) );

            //Assert
            Assert.That( identifiers.Contains( "CustomerId" ) && identifiers.Contains( "CustomerType" ) );
        }
    }
}