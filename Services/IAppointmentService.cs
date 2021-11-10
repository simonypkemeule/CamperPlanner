using CamperPlanner.Models;
using CamperPlanner.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CamperPlanner.Services
{
    public interface IAppointmentService
    {
        public List<Voertuigen> GetVoertuigenList();
        public Task<int> AddUpdate(AppointmentViewModel model);
    }
}
