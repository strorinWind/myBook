using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myBOOK.data
{
    interface UserBooks
    {
        int ID { get; set; }
        Users User { get; set; }
        Books Book { get; set; }
    }
}
