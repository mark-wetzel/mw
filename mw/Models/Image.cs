using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mw.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }

        [Required]
        public virtual Project Project { get; set; }
    }
}