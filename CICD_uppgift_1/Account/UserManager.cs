using System;

namespace CICD_uppgift_1
{
    //Class if for managing the current user
    public class UserManager
    {
        public User CurrentUser { get; set; }
        public object SelectedUser { get; set; }
        public Admin CurrentAdmin{ get; set; }
        public bool UserIsAdmin {get; set; }
        public bool HasUser { get; set; }

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
            HasUser = true;
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

        public void SelectUser(IUser user)
        {
            SelectedUser = user;
        }
    }
}