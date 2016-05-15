using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace mw.Models
{
    public class Project
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
    }
}