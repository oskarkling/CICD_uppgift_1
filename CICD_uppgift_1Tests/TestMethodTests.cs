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

            Assert.AreEqual(true, input.IsMenuInputValid(stringInput, out validnr, out errormsg, 16));
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
            string errormsg = "";
            int validNr;

            Assert.AreEqual(true, input.IsNumberInputValid(nrInput, out validNr, out errormsg, false));
        }

        [TestMethod()]
        public void SetUserTest()
        {
            IUser user = new User("oskar", "kling", Roles.Boss);
            UserManager manager = new UserManager();
            manager.SetUser(user);

            Assert.IsNotNull(manager.CurrentUser);
        }

        [TestMethod()]
        public void SetAdminTest()
        {
            IUser user = new Admin("admin1", "admin1234", Roles.Manager);
            UserManager manager = new UserManager();
            manager.SetUser(user);

            Assert.IsNotNull(manager.CurrentAdmin);
        }

        // Integration Test _____________________________
        Core core = new Core();

        [TestMethod()]
        public void SetAdminIntegrationTest()
        {
            core.userManager.SetUser(core.data.userList[0]);

            Assert.IsTrue(core.userManager.UserIsAdmin);
        }

        [TestMethod()]
        public void AdminMainMenuIntegrationTest()
        {
            string errormsg;
            int nrOfMenuChoices = 6;

            Assert.IsTrue(core.input.IsMenuInputValid("4", out int menuChoice, out errormsg, nrOfMenuChoices));
        }

        [TestMethod()]
        public void AddUserIntegrationTest()
        {
            string username = "Rolf";
            string password = "Karlsson";
            Roles role = Roles.FloorWorker;
            bool userexist = false;

            core.AddUser(username, password, role);
            foreach(var user in core.data.userList)
            {
                if(user is User)
                {
                    if(user.Username == username && user.Password == password)
                    {
                        userexist = true;
                    }
                }
            }

            Assert.IsTrue(userexist);
        }
    }
}