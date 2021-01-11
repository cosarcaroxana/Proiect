using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosarca_Roxana_Project.Models;
using OfficeModel.Data;
using OfficeModel.Models;

namespace Cosarca_Roxana_Project.Data
{
    public class DbInitializer
    {
        public static void Initialize(OfficeContext context)
        {
            context.Database.EnsureCreated();
            if (context.Services.Any())
            {
                return; // BD a fost creata anterior
            }
            var services = new Service[]
            {
             new Service{Title="Baltagul",Description="Mihail Sadove1anu",Price=Decimal.Parse("22")},
             new Service{Title="Baltul",Description="Mihal Sadoveanu",Price=Decimal.Parse("2")},
             new Service{Title="Baltaul",Description="Mihil Sadoveanu",Price=Decimal.Parse("12")},
            };
            foreach (Service s in services)
            {
                context.Services.Add(s);
            }
            context.SaveChanges();
            var customers = new Customer[]
            {

                  new Customer{CustomerID=1050,Name="Popescu Marcela",BirthDate=DateTime.Parse("1979-09-01")},
                  new Customer{CustomerID=1045,Name="Mihailescu Cornel",BirthDate=DateTime.Parse("1969-07-08")},

 };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();
            var appointments = new Appointment[]
            {
              new Appointment{ServiceID =2,CustomerID=1050},
              new Appointment{ServiceID =3,CustomerID=1045},
              new Appointment{ServiceID =4,CustomerID=1045},
              new Appointment{ServiceID=2,CustomerID=1050},
            };


            foreach (Appointment e in appointments)
            {
                context.Appointments.Add(e);
            }

            var doctors = new Doctor[]
 {

            new Doctor{DoctorName="Humanitas",Specializare="Str. Aviatorilor, nr. 40,Bucuresti" },
            };
            foreach (Doctor p in doctors)
            {
                context.Doctors.Add(p);
            }
            context.SaveChanges();

            var doctorservicess = new DoctorService[]
 {
                   new DoctorService{
 ServiceID = services.Single(c => c.Title == "Baltaul" ).ID,
 DoctorID = doctors.Single(i => i.DoctorName =="Humanitas").ID
 },

 };
            foreach (DoctorService pb in doctorservicess)
            {
                context.DoctorServices.Add(pb);
            }
            context.SaveChanges();

        }
    }
}
