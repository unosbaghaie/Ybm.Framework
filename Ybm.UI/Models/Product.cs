﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ybm.UI.Models
{
    public class Product
    {
        public int ProductId { set; get; }
        public string ProductName { set; get; }
        public string ProductCode { set; get; }
        public string ReleaseDate { set; get; }
        public decimal Price { set; get; }
        public string Description { set; get; }
        public double StarRating { set; get; }
        public string ImageUrl { set; get; }
    }
}