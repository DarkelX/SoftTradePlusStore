using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTradePlusStore.Models.Products
{
    internal abstract class BaseProduct : IHaveIdName, ICloneable
    {
        public int Id { get; protected set; }
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
        public BaseProduct() { }
        public BaseProduct(bool newItem)
        {
            if (newItem)
                Id = -1;
        }

        public BaseProduct(string name, float price, ProductType type, SubscriptionTerm term)
        {
            Name = name;
            Price = price;
            Type = type;
            Term = term;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
