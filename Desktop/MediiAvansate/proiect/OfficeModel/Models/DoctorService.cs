using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeModel.Models
{
    public class DoctorService
    {
        public int DoctorID { get; set; }
        public int ServiceID { get; set; }
        public Doctor Doctor { get; set; }
        public Service Service { get; set; }

    }
}
