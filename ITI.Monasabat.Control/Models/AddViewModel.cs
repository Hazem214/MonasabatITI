using Monasapat.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITI.Monasabat.Control.Models
{
    public class AddViewModel
    {
        [Required,Display(Name="Name")]
        [RegularExpression("(([a-zA-Z]{3,})\\s){1,}" , ErrorMessage = "name should be Alpha and More than three letter at least ")]
        public string Name { get; set; }
        [Required,  Display(Name = "Price")]

        [Range(1, 15000000, ErrorMessage = "price should be more than 0")]
        public int Price { get; set; }

        [Required,Display(Name="Type")]


        [RegularExpression("([a-zA-Z]{3,})", ErrorMessage = "name should be Alpha  ")]
        public string Type { get; set; }
        [Required,Display(Name= "Place Owner ")]

        public int PlaceOwnerID { get; set; }
        [Required,Display(Name= "City")]

        public int? CityID { get; set; }



        [Required]


        [NotMapped]
        //[FileExtensions(Extensions = "jpg,jpeg,png")]
        public List<IFormFile>? Images { get; set; }

    }
}
