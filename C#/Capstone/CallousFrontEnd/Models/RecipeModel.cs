﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallousFrontEnd.Models
{
    public class RecipeModel
    {
        public string Name { get; set; }
        public float Servings { get; set; }
        public string Url { get; set; }
        public string SiteName { get; set; }
    }
}