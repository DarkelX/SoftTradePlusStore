using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTradePlusStore.Models.Products
{
    internal class Product : BaseProduct
    {
        public Product() { }
        public Product(bool newItem) : base(newItem) { }
    }
}
