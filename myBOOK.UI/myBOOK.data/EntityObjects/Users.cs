using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Key]
        public string Login { get; set; }
        public string FullName { get; set; }
        public Guid Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Gender Gender { get; set; }
    }
}
