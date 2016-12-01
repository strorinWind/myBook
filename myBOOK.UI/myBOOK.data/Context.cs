using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myBOOK.data
{
    class Context:DbContext
    {
        public DbSet<Reviews> _Review{ get; set; }
        public DbSet<Score> _Score { get; set; }
        public DbSet<Books> _Book { get; set; }
        public DbSet<Users> _User { get; set; }

        public Context()
            : base("localsql")
        {
        }
    }
}
