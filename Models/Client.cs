
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTradePlusStore.Models
{
    internal abstract class Client : IHaveIdName
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public ClientStatus Status { get; set; }
        public Manager Manager { get; set; }
        public List<Product> Products { get; set; }

        public enum ClientStatus
        {
            Regular,
            Key
        }

        public Client() { }

        public Client(string name, ClientStatus status, Manager manager, List<Product> products)
        {
            Name = name;
            Status = status;
            Manager = manager;
            Products = products;
        }

        public void Buy(Product product)
        {
            Products?.Add(product);
        }
    }
}
