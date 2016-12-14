using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myBOOK.data;

namespace myBOOK.data.Interfaces
{
   public interface IUserBooks
    {
        //int ID { get; set; }
        Users User { get; }
        Books Book { get; }
    }
}
