using ResPAL_MVC.Models;
using ResPAL_MVC.Models.ResPALModels;
using ResPAL_MVC.Data;
using System;
using System.Linq;

namespace ResPAL_MVC.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ResPALContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Patients.Any())
            {
                return;   // DB has been seeded
            }

            var patients = new Patient[]
            {
            new Patient{FirstName="Carson",LastName="Alexander",DOB=DateTime.Parse("2005-09-01")},
            new Patient{FirstName="Meredith",LastName="Alonso",DOB=DateTime.Parse("2002-09-01")},
            new Patient{FirstName="Arturo",LastName="Anand",DOB=DateTime.Parse("2003-09-01")},
            new Patient{FirstName="Gytis",LastName="Barzdukas",DOB=DateTime.Parse("2002-09-01")},
            };
            foreach (Patient s in patients)
            {
                context.Patients.Add(s);
            }
            context.SaveChanges();

            var diseases = new PrimaryDisease[]
           {
            new PrimaryDisease{PatientID=1,DiseaseGroup="CPA"},
            new PrimaryDisease{PatientID=1,DiseaseGroup="ABPA"},
            new PrimaryDisease{PatientID=1,DiseaseGroup="SAFS"},
            new PrimaryDisease{PatientID=2,DiseaseGroup="Aspergillosis" }
           };
            foreach (PrimaryDisease d in diseases)
            {
                context.Diseases.Add(d);
            }
            context.SaveChanges();


        }
    }
}
