using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myBOOK.data.EntityObjects
{
    public class UserToBook
    {
        public int ID { get; set; }
        public Users User { get; set; }
        public Books Book { get; set; }
        public Categories Category { get; set; }

        public enum Categories
        {
            PastRead,
            FutureRead,
            Favourite
        }
    }
}
