using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace mw.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }        
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class EntryContext : DbContext
    {
        public DbSet<Entry> Entries { get; set; }
    }
}