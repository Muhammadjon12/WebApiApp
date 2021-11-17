using CreateApiEmty.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateApiEmty.Server
{
    public class PersonContext:DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> contextOptions):base(contextOptions)
        {
            Database.EnsureCreated();
        }

        public DbSet<Person> Persons { get; set; }
    }
}
