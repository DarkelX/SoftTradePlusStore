using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTradePlusStore.Models
{
    internal class Manager : IHaveIdName
    {
        public int Id { get; private set; }
        public string Name { get; set; }

        public Manager() { }
        public Manager(string name)
        {
            Name = name;
        }
    }
}
