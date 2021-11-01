using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CamperPlanner.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Voornaam")]
        public string Voornaam { get; set; }


        [Required]
        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Achternaam")]
        public string Achternaam { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Bankrekening")]
        public string Bankrekening { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Postcode")]
        public string Postcode { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Straatnaam")]
        public string Straatnaam { get; set; }

        [Required]
        [Column(TypeName = "int")]
        [Display(Name = "Huisnummer")]
        public int Huisnummer { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        [Display(Name = "Geboortedatum")]
        public DateTime Geboortedatum { get; set; }


    }
}
