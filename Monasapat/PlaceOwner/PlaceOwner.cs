using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monasapat.Models
{
    public class PlaceOwner
    {
       public int ID { get; set; }

        [RegularExpression("([a-zA-Z]{3,})", ErrorMessage = "name should be Alpha and More than three letter at least ")]
        public string Name { get; set; }

        //navigation
        public virtual ICollection<Place> Places { get; set; }

    }
}
