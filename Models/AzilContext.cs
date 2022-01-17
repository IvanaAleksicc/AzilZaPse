
using Microsoft.EntityFrameworkCore;

namespace Models{

    public class AzilContext: DbContext
    {
        public DbSet<Azil> Azili {get;set;}
        public DbSet<Pas> Psi {get;set;}
        public DbSet<Rasa> Rase {get;set;}
        public DbSet<Udomitelj> Udomitelji {get;set;}

        public AzilContext(DbContextOptions options) : base(options)
        {


        }

        
    }
}