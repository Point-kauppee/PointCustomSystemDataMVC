using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointCustomSystemDataMVC.ViewModels
{
    public class CustomerViewModel
    {
        public int? Personnel_id { get; set; }
        public int? Customer_id { get; set; }
        [Display(Name = "Etunimi")]
        public string FirstName { get; set; }
        [Display(Name = "Sukunimi")]
        public string LastName { get; set; }
        [Display(Name = "Syntymäaika")]
        public string Identity { get; set; }
        [Display(Name = "Huomiot")]
        public string Notes { get; set; }
        [Display(Name = "Sähköposti")]
        public string Email { get; set; }
        [Display(Name = "Osoite")]
        public string Address { get; set; }
        [Display(Name = "Hoitokertomus")]
        public string Note { get; set; }

        public int? Post_id { get; set; }
        [Display(Name = "PostiNro")]
        public string PostalCode { get; set; }
        [Display(Name = "Postiosoite")]
        public string PostOffice { get; set; }
        public string PostOffices { get; set; }
        
      
        public int? Phone_id { get; set; }
        public string Phone { get; set; }
        [Display(Name = "PuhNro")]
        public string PhoneNum_1 { get; set; }

        public int? User_id { get; set; }
        public string User{ get; set; }
        [Display(Name = "Käyttäjätunnus")]
        public string UserIdentity { get; set; }

        [Display(Name = "Hoitoaika")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime TreatmentTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "HoitoPvm")]
        public DateTime TreatmentDate { get; set; }

        public string TreatmentReport { get; set; }
        public string TreatmentReportName { get; set; }
        public string TreatmentReportText { get; set; }
        public int? TreatmentReport_id { get; set; }


        [Display(Name = "Asiakas")]
        public string FullNameA
        {
            get { return FirstName + " " + LastName; }
        }
        [Display(Name = "Hoitaja")]
        public string FullNameH
        {
            get { return FirstName + ", " + LastName; }
        }
    }
}