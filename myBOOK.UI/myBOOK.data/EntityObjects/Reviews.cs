using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myBOOK.data
{
    public class Reviews
    {
        public int ID { get; set; }
        public Users User { get; set; }
        public Books Book { get; set; }
        public string ReviewText{ get; set; }
    }
}
