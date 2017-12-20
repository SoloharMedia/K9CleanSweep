using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace K9CleanSweep.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [Required(ErrorMessage = "Please Enter A Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please Enter A Review")]
        public string Message { get; set; }

        public string Author { get; set; }

        [Required]
        [Range(1, 5)]
        public int starRating { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ReviewDate { get; set; }

        [ForeignKey("ClientID")]
        public int ClientID { get; set; }

    }
}
