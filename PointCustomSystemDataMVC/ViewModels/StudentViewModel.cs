using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointCustomSystemDataMVC.ViewModels
{
    public class StudentViewModel
    {
        public int Student_id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Opintojen aloitus pvm")]
        public DateTime? EnrollmentDateIN { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Valmistunut pvm")]
        public DateTime? EnrollmentDateOUT { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Opinnot keskeytyneet pvm")]
        public DateTime? EnrollmentDateOFF { get; set; }
       
        public int? Personnel_id { get; set; }
        public string Personnel { get; set; }
        public int? Customer_id { get; set; }
        [Display(Name = "Etunimi")]
        public string FirstName { get; set; }
        [Display(Name = "Sukunimi")]
        public string LastName { get; set; }
        public string Identity { get; set; }
        [Display(Name = "Huomiot")]
        public string Notes { get; set; }
        [Display(Name = "Sähköposti")]
        public string Email { get; set; }
        [Display(Name = "Osoite")]
        public string Address { get; set; }

       

        public int? Post_id { get; set; }
        [Display(Name = "PostiNro")]
        public string PostalCode { get; set; }
        [Display(Name = "Postiosoite")]
        public string PostOffice { get; set; }
        public string PostOffices { get; set; }


        public int? Phone_id { get; set; }
        public string Phone { get; set; }
        public string PhoneNum_1 { get; set; }

        public int? User_id { get; set; }
        public string User { get; set; }
        public string UserIdentity { get; set; }

        public string Password { get; set; }
        

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


        [Display(Name = "Alkaen klo")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime Start { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Loppuu klo")]
        public DateTime End { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Pvm")]
        public DateTime Date { get; set; }
    }
}
