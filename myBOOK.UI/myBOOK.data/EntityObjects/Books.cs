using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myBOOK.data
{
    public class Books
    {
        //public int ID { get; set; }
        [Key]
        [Column(Order = 1)]
        public string BookName { get; set; }
        [Key]
        [Column(Order = 2)]
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string LoadingLink { get; set; }
    }
}
