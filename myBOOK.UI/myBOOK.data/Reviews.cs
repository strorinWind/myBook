using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myBOOK.data
{
   public  class Reviews
    {
        public int ID { get; set; }
        public string ReviewText{ get; set; }
        public Users User { get; set; }
        public Books Book { get; set; }
    }
}
