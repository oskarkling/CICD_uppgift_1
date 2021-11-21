namespace CICD_uppgift_1
{
    public class Admin : Account, IUser
    {
        public int Salary { get; set; }
        public Roles Role { get; set; }

        public Admin(string username, string password, Roles role) : base(username, password)
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
    }
}