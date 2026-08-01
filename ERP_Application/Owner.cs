using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Tls;

namespace ERP_Application
{
    public class Owner
    {
        private string password;
        private string username;

        public Owner(string password)
        {
            this.Password = password;
            this.Username = "Owner";
        }

        public string Password {private get => password; set => password = value; }
        public string Username { get => username; set => username = value; }

        public bool verifyPassword(string verify)
        {
            if(verify == password)
                return true;
            else
                return false;
        }
        public void ChangePassword(string newPassword)
        {
            Password = newPassword;
        }
    }
}