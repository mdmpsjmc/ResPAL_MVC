using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResPAL_MVC.Models.ResPALModels
{
    
    public class PrimaryDisease
    {
        public int PrimaryDiseaseID { get; set; }
        public int PatientID { get; set; }
        public string DiseaseGroup { get; set; }
        public DateTime? Date { get; set; }

        public Patient Patient { get; set; } }
    }

