using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Infrastructure
{
    public class LinksContext : DbContext
    {
        public LinksContext() : base("Links")
        {

        }
        public DbSet<Link> Links { get; set; }
    }
}
