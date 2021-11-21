using System;

namespace CICD_uppgift_1
{
    class Program
    {
        static void Main()
        {
            Core core = new Core();
            Admin admin = new Admin("admin1", "admin1234", Roles.Manager);
            admin.Salary = 200000;
            User user = new User("oskar", "kling", Roles.Boss);
            user.Salary = 400000;
            User user1 = new User("Christopher", "brizet", Roles.Boss);
            user1.Salary = 800000;
            //admin1 och psw: admin1234.
            core.userList.Add(admin);
            core.userList.Add(user);
            core.userList.Add(user1);

            core.Run();
        }
    }
}
