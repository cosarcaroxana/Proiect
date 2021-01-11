using OfficeModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cosarca_Roxana_Project.Models.OfficeViewModels
{
    public class DoctorIndexData
    {
        public IEnumerable<Doctor> Doctors { get; set; }
        public IEnumerable<Service> Services { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
    }
}
