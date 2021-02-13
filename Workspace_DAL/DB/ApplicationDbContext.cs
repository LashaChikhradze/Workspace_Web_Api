using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Workspace_Models;

namespace Workspace_DAL.DB
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=FUJI\\CHIKHRA;Database=WorkspaceTestDb;Trusted_Connection=True;MultipleActiveResultSets=True");
        }
    }
}
