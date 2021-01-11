using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeModel.Models
{
    public class Service
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<DoctorService> DoctorServices { get; set; }


    }
}
