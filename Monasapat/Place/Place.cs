using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monasapat.Models
{
    public class Place
    {
        public int ID { get; set; }

        [RegularExpression("([a-zA-Z]{3,})", ErrorMessage = "name should be Alpha and More than three letter at least ")]
        public string Name { get; set; }

        [Range(1, 15000000, ErrorMessage = "price should be more than 0")]
        public int Price { get; set; }

        //public int NumberOfChair { get; set; }

        [RegularExpression("([a-zA-Z]{3,})", ErrorMessage = "name should be Alpha  ")]
        public string Type { get; set; }
        [ForeignKey("PlaceOwner")]
        public int PlaceOwnerID { get; set; }
        [ForeignKey("city")]
        public int? CityID { get; set; }


        //navigation
        public virtual PlaceOwner PlaceOwner { get; set; }
        public virtual ICollection<PlaceImage> PlaceImages { get; set; }
        public virtual ICollection<SuggestedDesign> SuggestedDesigns { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual City city { get; set; }


        [NotMapped]
        [FileExtensions(Extensions = "jpg,jpeg,png")]
        public List<IFormFile>? Images { get; set; }

    }
}
