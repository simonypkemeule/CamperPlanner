using CamperPlanner.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CamperPlanner.Services
{
    public interface IAppointmentService
    {
        public List<Voertuigen> GetVoertuigenList();
    }
}
