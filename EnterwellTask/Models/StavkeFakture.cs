using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EnterwellTask.Models
{
    public class StavkeFakture
    {
        [Key,Column(Order = 0),ForeignKey("Faktura")]
        public int FakturaID { get; set; }
        [Key, Column(Order = 1), ForeignKey("Stavka")]
        public int StavkaID { get; set; }
        public int Kolicina { get; set; }
        public Stavka Stavka { get; set; }
        public Faktura Faktura { get; set; }
    }
}