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
        [Display(Name = "ContractId")]
        public int ContractId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Startdatum")]
        public DateTime StartDatum { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Einddatum")]
        public DateTime EindDatum { get; set; }
        public int VoertuigId { get; set; }
        public Voertuigen Voertuig { get; set; }
    }
}
