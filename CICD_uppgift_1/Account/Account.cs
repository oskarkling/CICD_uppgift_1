namespace CICD_uppgift_1
{
    public abstract class Account
    {
        private string username;
        private string password;

        public Account(string _username, string _password)
        {
            Username = _username;
            Password = _password;
        }

        public string Username 
        { 
            get
            {
                return username;
            }
            set
            {
                username = value;
            } 
        }

        public string Password
        { 
            get
            {
                return password;
            }
            set
            {
                password = value;
            } 
        } 

    }
}