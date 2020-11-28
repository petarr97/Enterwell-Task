using EnterwellTask.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;

namespace EnterwellTask.Extensions
{
    [Export(typeof(IPorez))]
    public class PorezKalkulator : IPorez
    {
        private static Dictionary<String, double> dictionary = vratiPorez();

        public double IzracunajCijenu(double cijena, Faktura.PorezDrzave porez)
        {
            double taxValue = dictionary[porez.ToString()];
            return Calculate(cijena,taxValue);
        }
        private static Dictionary<String, double> vratiPorez()
        {
            Dictionary<String, double> dict = new Dictionary<string, double>();
            dict["Hrvatska"] = 0.25;
            dict["Bosna"] = 0.18;

            return dict;
        }
        private static double Calculate(double cijena, double porez)
        {
            return (cijena += cijena * porez);
        }
    }
}
   