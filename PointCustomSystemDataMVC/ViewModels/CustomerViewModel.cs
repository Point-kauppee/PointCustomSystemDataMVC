﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointCustomSystemDataMVC.ViewModels
{
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {
            this.ReservationViewModel = new HashSet<ReservationViewModel>();
            this.PersonnelViewModel = new HashSet<PersonnelViewModel>();
            this.StudentViewModel = new HashSet<StudentViewModel>();
            this.TreatmentOfficeViewModel = new HashSet<TreatmentOfficeViewModel>();
            this.CustomerParentViewModel = new HashSet<CustomerParentViewModel>();
        }


        public int? Personnel_id { get; set; }
        public int Customer_id { get; set; }
        public int? Student_id { get; set; }
        public string Studentx { get; set; }

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

        [Display(Name = "Syntymäaika")]
        public string Identity { get; set; }
        [Display(Name = "Tiedot")]
        public string Notes { get; set; }
        [Display(Name = "Sähköposti")]
        public string Email { get; set; }
        [Display(Name = "Osoite")]
        public string Address { get; set; }
        [Display(Name = "Tiedot")]
        public string Note { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy\\-MM\\-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hlötieto tallennettu pvm")]
        public DateTime? CreatedAt { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy\\-MM\\-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Muokattu pvm")]
        public DateTime? LastModifiedAt { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy\\-MM\\-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Arkistointi pvm")]
        public DateTime? DeletedAt { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy\\-MM\\-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Asiakasluvan tallennus pvm")]
        public DateTime? PermissionCheckDate { get; set; }

        
        [Display(Name = "Tila")]
        public bool? Active { get; set; }

        [Display(Name = "Lupatieto")]
        public bool? Permission { get; set; }
        

        [Display(Name = "Tiedot")]
        public string Information { get; set; }

        public int? Post_id { get; set; }
        [Display(Name = "PostiNro")]
        public string PostalCode { get; set; }
        [Display(Name = "Postiosoite")]
        public string PostOffice { get; set; }
        public string PostOffices { get; set; }

        public string Reservation { get; set; }

        public string Reservations { get; set; }
        public int? Reservation_id { get; set; }

      
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH\\:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Alkaen klo")]
        public DateTime? Start { get; set; }
      
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH\\:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Loppuu klo")]
        public DateTime? End { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy\\-MM\\-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "PalveluPvm")]
        public DateTime? Date { get; set; }

        public int? Phone_id { get; set; }
        public string Phone { get; set; }
        [Display(Name = "PuhNro")]
        public string PhoneNum_1 { get; set; }

        public int? User_id { get; set; }
        public string User{ get; set; }

        [Required(ErrorMessage = "Käyttäjätunnus tallennettava")]
        [Display(Name = "Käyttäjätunnus")]
        public string UserIdentity { get; set; }

        [Display(Name = "Salasana")]
        public string Password { get; set; }


        [Display(Name = "Palveluaika min.")]
        public string TreatmentTime { get; set; }

        [Display(Name = "Palvelun hinta")]
        public string TreatmentPrice { get; set; }

        [Display(Name = "Palvelu")]
        public string TreatmentName { get; set; }
        public string TreatmentReport { get; set; }    
        public string TreatmentReportName { get; set; }

        [Display(Name = "Palvelu suoritettu")]
        public bool? TreatmentCompleted { get; set; }

        [Display(Name = "Maksettu")]
        public bool? TreatmentPaid { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH\\:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Palvelu maksettu pvm")]
        public DateTime? TreatmentPaidDate { get; set; }

        [Display(Name = "Palveluraportti")]
        public string TreatmentReportTexts { get; set; }
        public int? TreatmentReport_id { get; set; }

        public virtual ICollection<ReservationViewModel> ReservationViewModel { get; set; }
      
        public virtual ICollection<PersonnelViewModel> PersonnelViewModel { get; set; }

        public virtual ICollection<StudentViewModel> StudentViewModel { get; set; }

        public virtual ICollection<TreatmentOfficeViewModel> TreatmentOfficeViewModel { get; set; }

        public virtual ICollection<CustomerParentViewModel> CustomerParentViewModel { get; set; }
        public virtual ICollection<CustomerViewModel> Customreservations { get; set; }

    }
}