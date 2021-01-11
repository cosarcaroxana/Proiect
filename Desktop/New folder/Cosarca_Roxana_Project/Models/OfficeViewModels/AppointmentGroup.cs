using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Cosarca_Roxana_Project.Models.OfficeViewModels
{
    public class AppointmentGroup
    {
        [DataType(DataType.Date)]
        public DateTime? AppointmentDate { get; set; }
        public int ServiceCount { get; set; }
    }
}
