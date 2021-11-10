using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CamperPlanner.Models.ViewModels
{
    public class EditUserViewModel
    {
        public string UserId { get; set; }

        public string Voornaam { get; set; }

        public string Achternaam { get; set; }

        public DateTime Geboortedatum { get; set; }

        public string Postcode { get; set; }

        public int Huisnummer { get; set; }

        public string Bankrekening { get; set; }

        public string Email { get; set; }

        public string Straatnaam { get; set; }

        public string PhoneNumber { get; set; }

        public string RoleName { get; set; }

    }
}
