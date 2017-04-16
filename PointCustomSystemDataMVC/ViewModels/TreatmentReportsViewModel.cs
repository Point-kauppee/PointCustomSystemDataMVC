using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointCustomSystemDataMVC.ViewModels
{
    public class TreatmentReportsViewModel
    {
        
        public int TreatmentReport_id { get; set; }

        [Display(Name = "Palvelun nimi")]
        public string TreatmentReportName { get; set; }

        [Display(Name = "Palveluraportti")]
        public string TreatmentReportText { get; set; }

     

        [Display(Name = "Palveluaika min.")]
        public string TreatmentTime { get; set; }

        public string Treatment { get; set; }
        public int? Treatment_id { get; set; }

        [Display(Name = "Palvelu")]
        public string TreatmentName { get; set; }

        public int? User_id { get; set; }
        public int? Customer_id { get; set; }
        public int? Student_id { get; set; }
        public int? Personnel_id { get; set; }
        public int? Reservation_id { get; set; }

        [Display(Name = "Käyttäjätunnus")]
        public string UserIdentity { get; set; }
        public string Customer { get; set; }

        public string Studentx { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Password { get; set; }

        //Lisätty yhdistävät nimikentät

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
            set { FullNameH2 = value; }
        }
        public string FullNameH2 { get; set; }


        [Display(Name = "Henkilökunta Etunimi")]
        public string FirstNameP { get; set; }

        [Display(Name = "Henkilökunta Sukunimi")]
        public string LastNameP { get; set; }

        [Display(Name = "Henkilökunta")]
        public string FullNameP
        {
            get { return FirstNameP + " " + LastNameP; }
        }

  
        public virtual ICollection<TreatmentReportsViewModel> TreatmentReservations { get; set; }
    }
}