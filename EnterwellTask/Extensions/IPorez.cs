using EnterwellTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterwellTask.Extensions
{
    interface IPorez
    {
        double IzracunajCijenu(double noVatPrice, Faktura.PorezDrzave porez);
    }
}
