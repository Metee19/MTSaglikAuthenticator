using Microsoft.EntityFrameworkCore;
using MTSaglikAuthenticator.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.DataAccess.Concrete.EntityFramework.Context
{
    public class MTSaglikAuthenticatorDemoContext: DbContext
    {
        public MTSaglikAuthenticatorDemoContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<Title> Titles { get; set; }
    }
}
