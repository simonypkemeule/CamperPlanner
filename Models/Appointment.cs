using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CamperPlanner.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "AfspraakId")]
        public int AfspraakId { get; set; }

        public int VoertuigID { get; set; }
        public Voertuigen Voertuig { get; set; }
        public DateTime BeginDatum { get; set; }

    }
}
