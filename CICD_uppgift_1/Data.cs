using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICD_uppgift_1
{
    public class Data
    {
        public List<IUser> userList;
        public Data()
        {
            userList = new List<IUser>();
            userList.Add(new Admin("admin1", "admin1234", Roles.Manager));
            userList.Add(new User("oskar", "kling", Roles.Boss));
            userList.Add(new User("Christopher", "brizet", Roles.Boss));
        }
    }
}
