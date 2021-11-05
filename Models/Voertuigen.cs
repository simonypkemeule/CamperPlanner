using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamperPlanner.Models
{
    public class Voertuigen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "VoertuigID")]
        public int VoertuigID { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Kenteken")]
        public string Kenteken { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Type")]
        public string Type { get; set; }

        [Required]
        [Column(TypeName = "int")]
        [Display(Name = "Lengte")]
        public int Lengte { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Merk")]
        public string Merk { get; set; }

        [Display(Name = "Stroomaansluiting")]
        [Column(TypeName = "varchar(4)")]
        public string Stroomaansluiting { get; set; }

        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public Contracten Contract { get; set; }
    }
}
