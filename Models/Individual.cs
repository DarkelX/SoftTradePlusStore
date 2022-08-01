using SoftTradePlusStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTradePlusStore.Models
{
    internal class Individual : Client
    {
        public Individual() { }

        public Individual(string name, ClientStatus status, Manager manager, List<Product> products) : base(name, status, manager, products)
        {
        }
    }
}
