using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeModel.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public int CustomerID { get; set; }
        public int ServiceID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Customer Customer { get; set; }
        public Service Service { get; set; }
    }
}
