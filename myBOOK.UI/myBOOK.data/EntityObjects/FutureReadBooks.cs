﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myBOOK.data
{
    public class FutureReadBooks:UserBooks
    {
        public int ID { get; set; }
        public Users User { get; set; }
        public Books Book { get; set; }
    }
}