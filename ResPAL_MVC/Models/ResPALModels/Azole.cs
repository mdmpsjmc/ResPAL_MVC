using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResPAL_MVC.Models.ResPALModels
{
    public class Azole
    {
        public int AzoleID { get; set; }
        public int PatientID { get; set; }
        public string AzoleName { get; set; }
        public string AzoleDose { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string AzoleNotes { get; set; }

        public Patient Patient { get; set; }
    }
}
