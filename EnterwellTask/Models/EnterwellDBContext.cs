using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EnterwellTask.Models
{
    public class EnterwellDBContext : DbContext
    {
        public EnterwellDBContext() : base("name=DefaultConnection")
    {
        this.Configuration.ValidateOnSaveEnabled = false;
    }
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
        modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
        modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

        }
    public DbSet<Faktura> Faktura { get; set; }
    public DbSet<Stavka> Stavka { get; set; }
    public DbSet<StavkeFakture> StavkeFakture { get; set; }

}
}