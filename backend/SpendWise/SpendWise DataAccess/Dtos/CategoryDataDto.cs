﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise_DataAccess.Dtos
{
    public class CategoryDataDto
    {
        public int id { get; set; }
        public string Name { get; set; }
        public double TotalPrice { get; set; }
    }
}
