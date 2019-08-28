using PeopleSearchApp.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

namespace PeopleSearchApp.DataAccessLayer.DBContexts
{
    public class PeopleDBContext : DbContext
    {
        public PeopleDBContext(DbContextOptions<PeopleDBContext> options) : base(options) {}

        public DbSet<Person> People { get; set; }
    }
}
