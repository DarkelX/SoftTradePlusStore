using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTradePlusStore.Models.Products
{
    internal class BoughtProduct : BaseProduct
    {
        public DateTime PurchaseDate { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime EndActivationDate => GetEndActivationDate();

        public BoughtProduct() { }

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
