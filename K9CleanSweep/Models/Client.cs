using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace K9CleanSweep.Models
{
    public class Client
    {
        [Key]
        public int ClientID { get; set; }

        [Required(ErrorMessage = "Please Enter Your Username")]
        [MaxLength(50)]
        public string ClientUserName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password")]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Your Email Address")]
        [MaxLength(150)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Your Full Name")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Home Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please Enter Your Postal Code")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Please Enter The Amount Of Dogs")]
        public int AmountOfDogs { get; set; }

        [Required(ErrorMessage = "Please Enter Your Approximate Yard Size")]
        public decimal YardSqFootage { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime JoinDate { get; set; }

        [Required(ErrorMessage = "Please Enter Desired Frequency")]
        [DefaultValue("One-Time")]
        public string Service { get; set; }

        public string ServiceDateTime { get; set; }

        [Required(ErrorMessage = "If You Have No Notes, just enter \"none\"")]
        [MaxLength(400)]
        public string Notes { get; set; }

        public List<Review> reviews { get; set; }
    }
}
