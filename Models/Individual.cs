using SoftTradePlusStore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTradePlusStore.Models
{
    internal class Individual : Client
    {
        public Individual() { }
        public Individual(bool newItem) : base(newItem) { }

        public Individual(string name, ClientStatus status, Manager manager, ObservableCollection<BoughtProduct> products) : base(name, status, manager, products)
        {
        }
    }
}
