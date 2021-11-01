/*using CamperPlanner.Data;
using CamperPlanner.Models.ViewModels;
using CamperPlanner.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*namespace CamperPlanner.Services
{
    public class AppointmentServices : IAppointmentService
    {
        ApplicationDbContext _db;
        public AppointmentServices(ApplicationDbContext db)
        {
            _db = db;
        }

      /*  public List<DoctorViewModel> GetDoctorList()
        {
           

            var doctors = (from user in _db.Users
                           join userRole in _db.UserRoles on user.Id equals userRole.UserId
                           join role in _db.Roles.Where(x => x.Name == Helper.Doctor) on userRole.RoleId equals role.Id
                           select new DoctorViewModel
                           {
                               Id = user.Id,
                               Name = string.IsNullOrEmpty(user.MiddleName) ?
                           user.FirstName + " " + user.LastName :
                           user.FirstName + " " + user.MiddleName + " " + user.LastName
                           }
                           ).OrderBy(u => u.Name).ToList();
            return doctors;
        }

        public List<PatientViewModel> GetPatientList()
        {
            throw new NotImplementedException();
        }
    }
}
