﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileLibrary.DataAccess
{
    public class Car
    {
        public int CarId { get; set; }
        public string? CarName { get; set; } 
        public string? Manufacturer { get; set; }
        public decimal Price { get; set; }
        public int ReleasedYear { get; set; }
    }
}
