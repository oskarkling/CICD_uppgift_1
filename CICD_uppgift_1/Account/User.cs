namespace CICD_uppgift_1
{
    public class User : Account, IUser
    {
        public int Salary { get; set; }
        public Roles Role { get; set; }
        public User(string username, string password, Roles role) : base(username, password)
        {
            Role = role;
        }

        private bool isLoggedIn;
        public bool IsLoggedIn
        {
            get
            {
                return isLoggedIn;
            }
            set
            {
                isLoggedIn = value;
            }
        }


        //Login

        //Logout

        //Check current salary

        //Check current company role

        //Delete account
    }
}