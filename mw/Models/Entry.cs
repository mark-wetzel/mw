using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace mw.Models
{
    public class BaseEntity
    {
        [Column(TypeName = "datetime2")]
        public DateTime? DateCreated { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? DateModified { get; set; }
    }

    public class Entry : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
    }

    public class EntryContext : DbContext
    {
        public DbSet<Entry> Entries { get; set; }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var e = (BaseEntity)entity.Entity;

                if (entity.State == EntityState.Added)
                {
                    e.DateCreated = DateTime.Now;
                }

                e.DateModified = DateTime.Now;

            }

            return base.SaveChanges();
        }
    }
}