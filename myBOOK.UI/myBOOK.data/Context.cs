﻿using myBOOK.data.EntityObjects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myBOOK.data
{
    public class Context:DbContext
    {
        public DbSet<Users> User { get; set; }
        public DbSet<Reviews> _Review{ get; set; }
        public DbSet<Score> _Score { get; set; }
        public DbSet<Books> _Book { get; set; }
        //позже убрать
        public DbSet<Favourite> _Favourite { get; set; }
        public DbSet<PastReadBooks> _PastReadBooks { get; set; }
        public DbSet<FutureReadBooks> _FutureReadBooks { get; set; }
        public DbSet<UserToBook> UserToBook { get; set; }

        public Context()
            : base("azuresql")
        {
        }
    }
}
