using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace Slapper.Tests
{
    public class TestBase
    {
        [TestCleanup]
        public void TearDown()
        {
            Slapper.AutoMapper.Cache.ClearAllCaches();
        }
    }
}