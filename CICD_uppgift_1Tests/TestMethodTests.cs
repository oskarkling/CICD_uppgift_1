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
        //Input tests:
        [TestMethod()]
        public void IsMenuInputValidTest()
        {
            Input input = new Input();

            string stringInput = "15";
            
            string errormsg = "";
            int validnr;

            Assert.AreEqual(true, input.IsMenuInputValid(stringInput,out validnr, out errormsg, 16));
        }

        [TestMethod()]
        public void IsStringInputValidTest()
        {
            Input input = new Input();

            string stringInput = "Ost";
            string errormsg = "";

            Assert.AreEqual(true, input.IsStringInputValid(stringInput, out errormsg));
        }

        [TestMethod()]
        public void IsNumberInputValidTest()
        {
            Input input = new Input();

            string nrInput = "1";
            string errormsg ="";
            int validNr;

            Assert.AreEqual(true, input.IsNumberInputValid(nrInput, out validNr, out errormsg, false));
        }
    }
}