using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monasapat.Models
{
    public class City
    {
      
        public int ID { get; set; }
        public string Name { get; set; }
        [ForeignKey("Governorate")]
        public int? GovernorateID { get; set; }

        //navigation
        public virtual Governorate Governorate { get; set; }
        public virtual ICollection<Place> Places { get; set; }
    }
}
