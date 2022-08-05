using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTradePlusStore.Models
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
    internal class Product : BaseProduct
    {
        public Product() { }
        public Product(bool newItem) : base(newItem) { }
    }
    internal class BoughtProduct : BaseProduct
    {
        public DateTime PurchaseDate { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime EndActivationDate => GetEndActivationDate();

        public BoughtProduct() {}

        public BoughtProduct(bool newItem) : base(newItem) { }

        public BoughtProduct(Product product)
        {
            PurchaseDate = DateTime.Now;
            Name = new string(product.Name);
            Price = product.Price;
            Type = product.Type;
            Term = product.Term;
        }

        private DateTime GetEndActivationDate()
        {
            return Term switch
            {
                SubscriptionTerm.Month => ActivationDate.AddMonths(1),
                SubscriptionTerm.Quarter => ActivationDate.AddMonths(3),
                SubscriptionTerm.Year => ActivationDate.AddYears(1),
                _ => ActivationDate,
            };
        }
    }
}
