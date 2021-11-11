using Microsoft.VisualStudio.TestTools.UnitTesting;
using CICD_uppgift_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICD_uppgift_1.Tests
{
    [TestClass()]
    public class TestMethodTests
    {
        [TestMethod()]
        public void AddIntTogetherTest()
        {
            TestMethod test = new TestMethod();
            int ans = test.AddIntTogether(2, 2);
            Assert.AreEqual(2, ans);
        }
    }
}