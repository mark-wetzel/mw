using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace mw.Models
{
    public class Project : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }

    public class ProjectContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities) {
                var e = (BaseEntity)entity.Entity;

                if (entity.State == EntityState.Added) {
                    e.DateCreated = DateTime.Now;
                }

                e.DateModified = DateTime.Now;

            }

            return base.SaveChanges();
        }
    }
}