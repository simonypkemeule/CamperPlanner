using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CamperPlanner.Models
{
    public class Contracten
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ContractIs")]
        public int ContractId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Start datum")]
        public DateTime StartDatum { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Einddatum")]
        public DateTime EindDatum { get; set; }

        public string UserId { get; set; }
        public string VoertuigId { get; set; }
        public Voertuigen Voertuigen { get; set; }
    }
}
