using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointCustomSystemDataMVC.ViewModels
{
    public class StudentDetailViewModel
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "HoitoPvm")]
        public DateTime? Date { get; set; }

        [Display(Name = "Alkaen klo")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:MM}", ApplyFormatInEditMode = true)]
        public DateTime? Start { get; set; }
        [Display(Name = "Loppuu klo")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:MM}", ApplyFormatInEditMode = true)]
        public DateTime? End { get; set; }

        public string TreatmentName { get; set; }

        [Display(Name = "Hoitoaika min.")]
        public string TreatmentTime { get; set; }
        [Display(Name = "Palvelun hinta")]
        public string TreatmentPrice { get; set; }


        [Display(Name = "Asiakas Etunimi")]
        public string FirstNameA { get; set; }
        [Display(Name = "Asiakas Sukunimi")]
        public string LastNameA { get; set; }

        [Display(Name = "Asiakas")]
        public string FullNameA
        {
            get { return FirstNameA + " " + LastNameA; }
        }


        [Display(Name = "Hoitaja Etunimi")]
        public string FirstNameH { get; set; }
        [Display(Name = "Hoitaja Sukunimi")]
        public string LastNameH { get; set; }
        [Display(Name = "Hoitaja")]
        public string FullNameH
        {
            get { return FirstNameH + " " + LastNameH; }
        }

        [Display(Name = "Tiedot")]
        public string Notes { get; set; }

        public virtual ICollection<StudentDetailViewModel> Studentreservations { get; set; }
    }
}