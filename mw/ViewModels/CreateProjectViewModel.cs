﻿using mw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mw.ViewModels
{
    public class CreateProjectViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }
}