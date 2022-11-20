using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monasapat.Models
{
    public class User: IdentityUser
    {
        [Required, MaxLength(20)]
        [RegularExpression("([a-zA-Z]{3,})", ErrorMessage = "name should be Alpha and More than three letter at least ")]
        public string FirstName { get; set; }

        [RegularExpression("([a-zA-Z]{3,})", ErrorMessage = "name should be Alpha and More than three letter at least ")]
        [Required, MaxLength(20)]
        public string LastName { get; set; }
        public bool IsBlook { get; set; }

        //navigation
        public virtual ICollection<Comments>Comments { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }


    }
}
