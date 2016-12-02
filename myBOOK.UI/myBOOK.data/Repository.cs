using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myBOOK.data
{
    public class Repository
    {
        public bool IsUserDataCorrect(string login, string password)
        {
            using (Context c = new Context())
            {
                var result = (from s in c._User
                              where s.Login == login && s.Password == password
                              select s).ToList();

                if (result != null)
                {
                    return true;
                }
                else
                    return false;
            }

        }

        public bool IsLoginRepeated (string login)
        {
            using (Context c = new Context())
            {
                var result = (from s in c._User
                              where s.Login == login
                              select s).ToList();

                if (result != null)
                {
                    return false;
                }
                else
                    return true;
            }
        }



    }
}
