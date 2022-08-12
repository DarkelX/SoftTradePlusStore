
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftTradePlusStore.Models.Products;

namespace SoftTradePlusStore.Models.Clients
{
    internal abstract class Client : IHaveIdName
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public ClientStatus Status { get; set; }
        public Manager Manager { get; set; }
        public ObservableCollection<BoughtProduct> Products { get; set; }

        public enum ClientStatus
        {
            Key,
            Regular
        }

        public Client()
        {
            Products = new ObservableCollection<BoughtProduct>();
        }
        public Client(bool newItem) : this()
        {
            if (newItem)
                Id = -1;
        }

        public Client(string name, ClientStatus status, Manager manager, ObservableCollection<BoughtProduct> products) : this()
        {
            Name = name;
            Status = status;
            Manager = manager;
            Products = products;
        }
    }
}
