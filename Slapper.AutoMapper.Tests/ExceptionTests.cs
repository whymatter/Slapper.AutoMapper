﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Slapper.Tests
{
    [TestClass]
    public class ExceptionTests : TestBase
    {
        public class Person
        {
            public int PersonId;
            public string FirstName;
            public string LastName;
        }

        [TestMethod]
        public void Will_Throw_An_Exception_If_The_Type_Is_Not_Dynamic()
        {
            // Arrange
            var someObject = new object();

            // Act
            TestDelegate test = () => Slapper.AutoMapper.MapDynamic<Person>( someObject );

            // Assert
            Assert.Throws<ArgumentException>( test );
        }

        [TestMethod]
        public void Will_Not_Throw_An_Exception_If_The_List_Items_Are_Not_Dynamic()
        {
            // Arrange
            var someObjectList = new List<object> { null };

            // Act
            TestDelegate test = () => Slapper.AutoMapper.MapDynamic<Person>( someObjectList );

            // Assert
            Assert.DoesNotThrow( test );
        }

        [TestMethod]
        public void Will_Return_An_Empty_List_Of_The_Requested_Type_When_Passed_An_Empty_List()
        {
            // Arrange
            var someObjectList = new List<object>();

            // Act
            var list = Slapper.AutoMapper.MapDynamic<Person>( someObjectList );

            // Assert
            Assert.NotNull( list );
            Assert.That( list.Count() == 0 );
        }
    }
}