using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResPAL_MVC.Models.ResPALModels
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string RESID { get; set; }
        public string RM2Number { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
      
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyy}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }
        public string PatientStatus { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        public ICollection<PrimaryDisease> Diseases { get; set; }
        public ICollection<Azole> Azoles { get; set; }

    }
}
