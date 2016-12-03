using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myBOOK.data
{
    public enum Gender
    {
        Male,
        Female
    }

    public class Users
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Gender Gender { get; set; }

        public Users(string login, string fullname, string password,Gender gender)
        {
            Login = login;
            FullName = fullname;
            Password = password;
            Gender = gender;
            RegistrationDate = DateTime.Now;
        }
    }
}
