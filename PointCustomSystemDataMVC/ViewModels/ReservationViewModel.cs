using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointCustomSystemDataMVC.ViewModels
{
    public class ReservationViewModel
    {
        public int? Reservation_id { get; set; }

        public string Customer { get; set; }

        public int? Customer_id { get; set; }
        public string Identity { get; set; }
        [Display(Name = "Sähköposti")]
        public string Email { get; set; }
        [Display(Name = "Huomiot")]
        public string Note { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hoito maksettu pvm")]
        public DateTime? TreatmentPaidDate { get; set; }
        [Display(Name = "Maksettu")]
        public bool? TreatmentPaid { get; set; }

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
        public string FullNameH2 { get; set; }
        [Display(Name = "Hoitaja")]
        public string FullNameH
        {
            get { return FirstNameH + " " + LastNameH; }
            set { FullNameH2 = value; }
        }

        [Display(Name = "Henkilökunta Etunimi")]
        public string FirstNameP { get; set; }

        [Display(Name = "Henkilökunta Sukunimi")]
        public string LastNameP { get; set; }

        [Display(Name = "Henkilökunta")]
        public string FullNameP
        {
            get { return FirstNameP + " " + LastNameP; }
        }


        public string CalendarTitle
        {
            get { return FullNameA + " " + Start + " " + TreatmentName + " " + FullNameH2; }
            set { CalendarTitle = value; }
        }

        //public IEnumerable<StudentViewModel> Studentx { get; set; }
        public IEnumerable<StudentViewModel> Customers { get; set; }
        //public IEnumerable<StudentViewModel> Treatment { get; set; }

        //Lisätty päivämäärämääritykset reservation.cs

       

        [Display(Name = "Alkaen klo")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:MM}", ApplyFormatInEditMode = true)]
        public DateTime? Start { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:MM}", ApplyFormatInEditMode = true)]
        [Display(Name = "Päättyen klo")]
        public DateTime? End { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Pvm")]
        public DateTime? Date { get; set; }

        public string TreatmentplaceName { get; set; }

        public int? User_id { get; set; }

        [Display(Name = "Asiakastunnus")] 
        public string User { get; set; }

        //public int? User_id { get; set; }
        [Display(Name = "Asiakastunnus")]
        public string UserIdentity { get; set; }
        public string Password { get; set; }

        [Display(Name = "Hoito")]
        public string Treatment { get; set; }
        public int? Treatment_id { get; set; }
        [Display(Name = "Palvelu")]
        public string TreatmentTime { get; set; }

        [Display(Name = "Hoito")]
        public string TreatmentName { get; set; }
        public string TreatmentPrice { get; set; }

        public string TretamentOffice { get; set; }
        public int? TreatmentOffice_id { get; set; }    
        public string TreatmentOfficeName { get; set; }

        [Display(Name = "Hoitopaikka")]
        public string TreatmentPlace { get; set; }
        public int? Treatmentplace_id { get; set; }
        [Display(Name = "Hoitopaikka")]
        public string TreatmentPlaceName { get; set; }
        public string TreatmentPlaceNumber { get; set; }

        public string Phone { get; set; }
        public int? Phone_id { get; set; }
        public string PhoneNum_1 { get; set; }


        public string PostOffices { get; set; }
        public int? Post_id { get; set; }
        [Display(Name = "PostiNro")]
        public string PostalCode { get; set; }
        [Display(Name = "Postiosoite")]
        public string PostOffice { get; set; }
        [Display(Name = "Hoitaja")]
        public string Studentx { get; set; }
        public int? Student_id { get; set; }
        [Display(Name = "Huomiot")]
        public string Notes{get; set;}
       
        public string DataDateField { get; set; }

        public string EventManager { get; set; }
       
      
    }
}