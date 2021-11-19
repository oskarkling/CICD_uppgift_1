using System;
using System.Collections.Generic;

namespace CICD_uppgift_1
{
    public class Core
    {
        public List<IUser> userList;
        private string errormsg;
        private Input input;
        private UserManager userManager;

        //Init in constructor
        public Core()
        {
            errormsg = "";
            input = new Input();
            userList = new List<IUser>();
            userManager = new UserManager();
        }
        public void Run()
        {
            WelcomeMenu();
        }
        private void WelcomeMenu()
        {
            bool runMenu = true;
            while (runMenu)
            {

                int nrOfMenuChoices = 2;
                Console.WriteLine("1. Login\n0. Exit");

                var stringInput = Console.ReadLine();
                if (input.IsMenuInputValid(stringInput, out int menuChoice, out errormsg, nrOfMenuChoices))
                {
                    switch (menuChoice)
                    {
                        case 1:
                            Login();
                            break;
                        case 0:
                            runMenu = false;
                            Console.Clear();
                            Console.WriteLine("bye bye");
                            Environment.Exit(0);
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(errormsg);
                }
            }
        }

        private void Login()
        {
            input.DeletePrevConsoleLine();
            bool wrongPassword = false;
            Console.WriteLine("Enter username");
            var inputUsername = Console.ReadLine();
            if (input.IsStringInputValid(inputUsername, out errormsg))
            {
                Console.WriteLine("Enter password");
                var inputPassword = Console.ReadLine();
                if (input.IsStringInputValid(inputPassword, out errormsg))
                {
                    foreach (var user in userList)
                    {
                        if (user is User)
                        {
                            if (((User)user).Username == inputUsername)
                            {
                                if (((User)user).Password == inputPassword)
                                {
                                    Console.Clear();
                                    userManager.SetUser(user);
                                }
                                else
                                {
                                    wrongPassword = true;
                                }
                            }
                        }
                        else if (user is Admin)
                        {
                            if (((Admin)user).Username == inputUsername)
                            {
                                if (((Admin)user).Password == inputPassword)
                                {
                                    Console.Clear();
                                    userManager.SetUser(user);
                                }
                                else
                                {
                                    wrongPassword = true;
                                }
                            }
                        }
                    }
                    if (wrongPassword)
                    {
                        System.Console.WriteLine("Wrong password");
                    }
                    else
                    {
                        if (!userManager.HasUser)
                        {
                            System.Console.WriteLine("user does not exist");
                        }
                        else
                        {
                            if (userManager.UserIsAdmin)
                            {
                                AdminMainMenu();
                            }
                            else
                            {
                                UserMainMenu();
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine(errormsg);
                }
            }
            else
            {
                Console.WriteLine(errormsg);
            }
        }

        private void UserMainMenu()
        {
            bool runMenu = true;
            while (runMenu && userManager.HasUser)
            {
                int nrOfMenuChoices = 4;
                Console.WriteLine("1. Show your salary\n2. Show your role in company\n3. Remove your account\n0. Logout");
                var stringInput = Console.ReadLine();
                input.DeletePrevConsoleLine();
                if (input.IsMenuInputValid(stringInput, out int menuChoice, out errormsg, nrOfMenuChoices))
                {
                    switch (menuChoice)
                    {
                        case 1:
                            ShowSalary();
                            break;
                        case 2:
                            ShowRole();
                            break;
                        case 3:
                            RemoveAccount();
                            break;
                        case 0:
                            runMenu = false;
                            userManager = new UserManager();
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(errormsg);
                }
            }
        }

        private void RemoveAccount()
        {
            Console.Clear();
            if (userManager.UserIsAdmin)
            {
                Console.WriteLine("Select account to remove:");
                ShowAllUsers();
                int nbrOfAlternatives = userList.Count;
                string numInput = Console.ReadLine();

                if (input.IsMenuInputValid(numInput, out int choice, out errormsg, nbrOfAlternatives))
                {
                    switch (choice)
                    {
                        case 1:
                            userManager.SelectUser(userList[choice - 1]);
                            break;
                        case 2:
                            userManager.SelectUser(userList[choice - 1]);
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine();
                input.DeletePrevConsoleLine();
                IUser userToRemove = (IUser)userManager.SelectedUser;
                System.Console.WriteLine("Enter username and password to confirm account removal");
                System.Console.Write("Username: ");
                string inputUsername = Console.ReadLine();
                System.Console.Write("Password: ");
                string inputPassword = Console.ReadLine();
                Console.WriteLine();
                if (inputUsername == userToRemove.Username && inputPassword == userToRemove.Password)
                {
                    System.Console.WriteLine("You are about to delete account with username: " + userToRemove.Username);
                    System.Console.WriteLine("Are you sure? Yes - No");
                    Console.WriteLine();
                    string confirmation = Console.ReadLine();
                    bool open = true;
                    while (open)
                    {
                        if (confirmation.ToLower() == "yes" || confirmation.ToLower() == "y")
                        {
                            input.DeletePrevConsoleLine();
                            userList.Remove(userToRemove);
                            Console.WriteLine("User removed");
                            Console.ReadLine();
                            open = false;
                        }
                        else
                        {
                            Console.WriteLine("invalid input");
                        }
                    }
                }
                Console.Clear();
            }
            else
            {
                System.Console.WriteLine("Enter you username and password for removal of your account");
                System.Console.Write("Username: ");
                string inputUsername = Console.ReadLine();
                System.Console.Write("Password: ");
                string inputPassword = Console.ReadLine();
                if (userManager.CurrentUser.Username == inputUsername)
                {
                    if (userManager.CurrentUser.Password == inputPassword)
                    {
                        System.Console.WriteLine("username and password was ok");
                        User userToRemove = (User)userManager.GetUser();
                        userList.Remove(userToRemove);

                        //reset usermanager
                        userManager = new UserManager();
                        System.Console.WriteLine("user is removed");
                    }
                    else
                    {
                        System.Console.WriteLine("wrong password");
                    }
                }
                else
                {
                    System.Console.WriteLine("wrong username");
                }
            }
        }

        private void ShowRole()
        {
            Roles role;
            if (userManager.UserIsAdmin)
            {
                role = ((Admin)userManager.GetUser()).Role;
            }
            else
            {
                role = ((User)userManager.GetUser()).Role;
            }
            Console.Clear();
            System.Console.WriteLine("Your role in the company is: " + role);
        }

        private void AdminMainMenu()
        {
            Console.Clear();
            bool runMenu = true;
            while (runMenu && userManager.HasUser)
            {
                int nrOfMenuChoices = 6;
                System.Console.WriteLine("Admin Menu");
                Console.WriteLine("1. Show your salary\n2. Show your role in company\n3. Remove account\n4. Show all users\n5. Add a new user\n0. Logout");
                var stringInput = Console.ReadLine();
                if (input.IsMenuInputValid(stringInput, out int menuChoice, out errormsg, nrOfMenuChoices))
                {
                    switch (menuChoice)
                    {
                        case 1:
                            ShowSalary();
                            break;
                        case 2:
                            ShowRole();
                            break;
                        case 3:
                            RemoveAccount();
                            break;
                        case 4:
                            ShowAllUsers();
                            break;
                        case 5:
                            AddUser();
                            break;
                        case 0:
                            runMenu = false;
                            userManager = new UserManager();
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(errormsg);
                }

            }
        }

        private void ShowSalary()
        {
            int salary;
            if (userManager.UserIsAdmin)
            {
                salary = ((Admin)userManager.GetUser()).Salary;
            }
            else
            {
                salary = ((User)userManager.GetUser()).Salary;
            }
            Console.Clear();
            System.Console.WriteLine("Your salary is: " + salary);

        }

        private void ShowAllUsers()
        {
            Console.Clear();
            foreach (var user in userList)
            {
                Console.WriteLine(userList.IndexOf(user) + 1 + ". " + user.Username + " : " + user.Password);
            }
            Console.WriteLine();
        }

        private void AddUser()
        {
            Console.Clear();
            Console.WriteLine("Type a username and password for this new user");
            Console.Write("Username: ");
            string inputUserName = Console.ReadLine();
            Console.Write("Password: ");
            string inputPassword = Console.ReadLine();
            Console.WriteLine();
            bool runMenu = true;
            while (runMenu)
            {
                Console.WriteLine("Choose a role for this user");
                Console.WriteLine("1. Floorworker\n2. Manager\n3. Boss\n4. Unassigned");
                int nrOfMenuChoices = 4;
                var stringInput = Console.ReadLine();
                if (input.IsMenuInputValid(stringInput, out int menuChoice, out errormsg, nrOfMenuChoices))
                {
                    switch (menuChoice)
                    {
                        case 1:
                            userList.Add(new User(inputUserName, inputPassword, Roles.FloorWorker));
                            runMenu = false;
                            break;
                        case 2:
                            userList.Add(new User(inputUserName, inputPassword, Roles.Manager));
                            runMenu = false;
                            break;
                        case 3:
                            userList.Add(new User(inputUserName, inputPassword, Roles.Boss));
                            runMenu = false;
                            break;
                        case 4:
                            userList.Add(new User(inputUserName, inputPassword, Roles.UnAssigned));
                            runMenu = false;
                            break;
                        default:
                            break;
                    }
                }
            }
            input.DeletePrevConsoleLine();
            Console.WriteLine("User added successfully");
            Console.ReadLine();
            Console.Clear();
        }
    }
}