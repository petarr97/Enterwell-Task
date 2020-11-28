using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnterwellTask.Models
{
    public class Stavka
    {
        [Key]
        public int StavkaID { get; set; }
        public string Opis { get; set; }
        public float Cijena { get; set; }
        public virtual ICollection<StavkeFakture> StavkeFakture { get; set; }
        
        public Stavka()
        {
            StavkeFakture = new HashSet<StavkeFakture>();
        }
    }
}