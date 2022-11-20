using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monasapat.Models
{
    public class Governorate
    {
     
        public int ID { get; set; }
        public string Name { get; set; }

        //navigation
        public virtual ICollection<City> Cities { get; set; }
    }
}
