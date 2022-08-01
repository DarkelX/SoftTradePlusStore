using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTradePlusStore.Models
{
    internal class Product : IHaveIdName
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public ProductType Type { get; set; }
        public SubscriptionTerm Term { get; set; }


        public enum ProductType
        {
            Subscription,
            License
        }

        public enum SubscriptionTerm
        {
            Month,
            Quarter,
            Year
        }
        public Product() { }

        public Product(string name, float price, ProductType type, SubscriptionTerm term)
        {
            Name = name;
            Price = price; 
            Type = type;
            Term = term;
        }

    }
}
