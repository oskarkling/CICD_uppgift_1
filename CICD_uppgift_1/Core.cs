using System;
using System.Collections.Generic;

namespace CICD_uppgift_1
{
    public class Core
    {
        private string errormsg;
        public Input input;
        public UserManager userManager;
        public Data data;
        private ConsoleReadlineRetriever retriever;

        //Init in constructor
        public Core()
        {
            errormsg = "";
            input = new Input();
            data = new Data();
            userManager = new UserManager();
            retriever = new ConsoleReadlineRetriever();
        }

        //Star point for the core application
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

                var stringInput = retriever.GetStringInput();
                if (input.IsMenuInputValid(stringInput, out int menuChoice, out errormsg, nrOfMenuChoices))
                {
                    switch (menuChoice)
                    {
                        case 1:
                            LoginInput();
                            break;
                        case 0:
                            runMenu = false;
                            
                            Console.WriteLine("bye bye");
                            Environment.Exit(0);
                            break;
                    }
                }
                else
                {
                    
                    Console.WriteLine(errormsg);
                }
            }
        }

        //Handle input for login
        public void LoginInput()
        {
            Console.WriteLine("Enter username");
            var inputUsername = retriever.GetStringInput();
            Console.WriteLine("Enter password");
            var inputPassword = retriever.GetStringInput();
            Login(inputUsername, inputPassword);
        }

        //Processes input for login
        public void Login(string userName, string password)
        {
            // input.DeletePrevConsoleLine();
            bool wrongPassword = false;
            if (input.IsStringInputValid(userName, out errormsg))
            {

                if (input.IsStringInputValid(password, out errormsg))
                {
                    foreach (IUser user in data.userList)
                    {
                        if (user is User)
                        {
                            if (((User)user).Username == userName)
                            {
                                if (((User)user).Password == password)
                                {

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
                            if (((Admin)user).Username == userName)
                            {
                                if (((Admin)user).Password == password)
                                {

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
                        Console.WriteLine("Wrong password");
                    }
                    else
                    {
                        if (!userManager.HasUser)
                        {
                            Console.WriteLine("user does not exist");
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
                var stringInput = retriever.GetStringInput();
                //input.DeletePrevConsoleLine();
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
                    
                    Console.WriteLine(errormsg);
                }
            }
        }

        //Remove own account if user, adds possibility to remove other users if admin
        private void RemoveAccount()
        {
            
            if (userManager.UserIsAdmin)
            {
                Console.WriteLine("Select account to remove:");
                ShowAllUsers();
                int nbrOfAlternatives = data.userList.Count;
                string numInput = retriever.GetStringInput();

                if (input.IsMenuInputValid(numInput, out int choice, out errormsg, nbrOfAlternatives))
                {
                    switch (choice)
                    {
                        case 1:
                            userManager.SelectUser(data.userList[choice - 1]);
                            break;
                        case 2:
                            userManager.SelectUser(data.userList[choice - 1]);
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine();
                IUser userToRemove = (IUser)userManager.SelectedUser;
                Console.WriteLine("Enter username and password to confirm account removal");
                Console.Write("Username: ");
                string inputUsername = retriever.GetStringInput();
                Console.Write("Password: ");
                string inputPassword = retriever.GetStringInput();
                Console.WriteLine();
                if (inputUsername == userToRemove.Username && inputPassword == userToRemove.Password)
                {
                    Console.WriteLine("You are about to delete account with username: " + userToRemove.Username);
                    Console.WriteLine("Are you sure? Yes - No");
                    Console.WriteLine();
                    string confirmation = retriever.GetStringInput();
                    bool open = true;
                    while (open)
                    {
                        if (confirmation.ToLower() == "yes" || confirmation.ToLower() == "y")
                        {
                            data.userList.Remove(userToRemove);
                            Console.WriteLine("User removed");
                            retriever.GetStringInput();
                            open = false;
                        }
                        else
                        {
                            Console.WriteLine("invalid input");
                        }
                    }
                }
                
            }
            else
            {
                Console.WriteLine("Enter you username and password for removal of your account");
                Console.Write("Username: ");
                string inputUsername = retriever.GetStringInput();
                Console.Write("Password: ");
                string inputPassword = retriever.GetStringInput();
                if (userManager.CurrentUser.Username == inputUsername)
                {
                    if (userManager.CurrentUser.Password == inputPassword)
                    {
                        Console.WriteLine("username and password was ok");
                        User userToRemove = (User)userManager.GetUser();
                        data.userList.Remove(userToRemove);
                      
                        userManager = new UserManager();
                        Console.WriteLine("user is removed");
                    }
                    else
                    {
                        Console.WriteLine("wrong password");
                    }
                }
                else
                {
                    Console.WriteLine("wrong username");
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
            
            Console.WriteLine("Your role in the company is: " + role);
        }

        public void AdminMainMenu()
        {
            
            bool runMenu = true;
            while (runMenu && userManager.HasUser)
            {
                int nrOfMenuChoices = 6;
                Console.WriteLine("Admin Menu");
                Console.WriteLine("1. Show your salary\n2. Show your role in company\n3. Remove account\n4. Show all users\n5. Add a new user\n0. Logout");
                var stringInput = retriever.GetStringInput();
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
                            AddUserMenu();
                            break;
                        case 0:
                            runMenu = false;
                            userManager = new UserManager();
                            break;
                    }
                }
                else
                {
                    
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
            
            Console.WriteLine("Your salary is: " + salary);

        }

        private void ShowAllUsers()
        {
            
            foreach (var user in data.userList)
            {
                Console.WriteLine(data.userList.IndexOf(user) + 1 + ". " + user.Username + " : " + user.Password);
            }
            Console.WriteLine();
        }

        public void AddUserMenu()
        {
            Console.WriteLine("Type a username and password for this new user");
            Console.Write("Username: ");
            string inputUserName = retriever.GetStringInput();
            Console.Write("Password: ");
            string inputPassword = retriever.GetStringInput();
            Console.WriteLine();
            bool runMenu = true;
            var role = Roles.UnAssigned;
            while (runMenu)
            {
                Console.WriteLine("Choose a role for this user");
                Console.WriteLine("1. Floorworker\n2. Manager\n3. Boss\n4. Unassigned");
                int nrOfMenuChoices = 4;
                var stringInput = retriever.GetStringInput();
                
                if (input.IsMenuInputValid(stringInput, out int menuChoice, out errormsg, nrOfMenuChoices))
                {
                    switch (menuChoice)
                    {
                        case 1:
                            role = Roles.FloorWorker;                            
                            runMenu = false;
                            break;
                        case 2:
                            role = Roles.Manager;                            
                            runMenu = false;
                            break;
                        case 3:
                            role = Roles.Boss;
                            runMenu = false;
                            break;                        
                        default:
                            break;
                    }
                }
            }

            AddUser(inputUserName, inputPassword, role);
            Console.WriteLine("User added successfully");
            retriever.GetStringInput();
        }

        public void AddUser(string userName, string password, Roles role)
        {
            User user = new User(userName, password, role);
            data.userList.Add(user);
        }
    }
}