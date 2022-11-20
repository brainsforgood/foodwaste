using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodwaste
{
    public class Product
    {
        public string Name { get; set; }
        public DateTime Expiry { get; set; }
        public string ImageUrl { get; set; }
    }
}
