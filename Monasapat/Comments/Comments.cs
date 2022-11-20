using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monasapat.Models
{
    public class Comments
    {
        public int ID { get; set; }
        public string CommentText { get; set; }
        public int? commentRate { get; set; }
        [ForeignKey("Place")]
        public int PlaceID { get; set; }
        [ForeignKey("User")]
        public string UserID { get; set; }

        //navigation
        public virtual Place Place { get; set; }
        public virtual User User { get; set; }

    }
}
