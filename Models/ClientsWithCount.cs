using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTradePlusStore.Models
{
    internal class ClientsWithCount : Client
    {
        public int Count { get; private set; }

        public ClientsWithCount(Client client)
        {
            Name = client.Name;
            Status = client.Status;
            Manager = client.Manager;
            Count = 1;
        }

        public void IncreaseCount()
        {
            Count++;
        }
    }
}
