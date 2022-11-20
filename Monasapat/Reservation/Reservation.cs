using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monasapat.Models
{
    public class Reservation
    {
        public int ID { get; set; }
        public string Status { get; set; }
        public DateTime ReservationTime { get; set; }
        [Range(0, 15000000, ErrorMessage = "Paid price should be more than 0")]
        public int? PaidMoney { get;set; }

        [Range(0, 15000000, ErrorMessage = "Remaining price should be more than 0")]
        public int? remainingMoney { get; set; }
        public int? TotalMoney { get; set; }

        [ForeignKey("Place")]
        public int placeID { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }
       

        //navigation
        public virtual Place Place { get; set; }
        public virtual User User { get; set; }
        
    }
}
