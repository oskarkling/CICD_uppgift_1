using System;

namespace CICD_uppgift_1
{
    public class UserManager
    {
        public User CurrentUser { get; set; }
        public Admin CurrentAdmin{ get; set; }
        public bool UserIsAdmin {get; set; }

        public object GetUser()
        {
            if(UserIsAdmin)
            {
                return CurrentAdmin;
            }
            else
            {
                return CurrentUser;
            }
        }

        public void SetUser(IUser user)
        {
            if(user is User)
            {
                CurrentUser = (User)user;
                UserIsAdmin = false;
            }
            if(user is Admin)
            {
                CurrentAdmin = (Admin)user;
                UserIsAdmin = true;
            }
        }
    }
}