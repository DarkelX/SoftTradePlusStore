using SoftTradePlusStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTradePlusStore.Models
{
    internal class Entity : Client
    {
        public Individual Individual { get; set; }
        public Entity() { }

        public Entity(string name, ClientStatus status, Manager manager, List<Product> products, Individual individual) : base(name, status, manager, products)
        {
            Individual = individual;
        }
    }
}
