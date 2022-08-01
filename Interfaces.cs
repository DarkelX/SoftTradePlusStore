using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTradePlusStore
{
    public interface IHaveIdName
    {
        int Id { get; }
        string Name { get; set; }
    }
}
