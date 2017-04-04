using PointCustomSystemDataMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointCustomSystemDataMVC.ViewModels
{
    public class StudentViewModel
    {
        public StudentViewModel()
        {
            this.Phone = new HashSet<Phone>();
            this.PostOffices = new HashSet<PostOffices>();
            this.Reservation = new HashSet<Reservation>();
            //this.StudentGroup = new HashSet<StudentGroup>();
            this.TreatmentReport = new HashSet<TreatmentReport>();
            this.User = new HashSet<User>();

        }

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


        public int? StudentGroup_id { get; set; }
        [Display(Name = "Kurssi")]
        public string StudentGroupName { get; set; }
        public string StudentGroup { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Henkilötieto tallennettu")]
        public DateTime? CreatedAt { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Viimeksi muokattu pvm")]
        public DateTime? LastModifiedAt { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Arkistointi pvm")]
        public DateTime? DeletedAt { get; set; }
        [Display(Name = "Tila")]
        public bool? Active { get; set; }
        [Display(Name = "Tiedot")]
        public string Information { get; set; }

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

        [Display(Name = "Etunimi")]
        public string FirstNameH { get; set; }
        [Display(Name = "Sukunimi")]
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
        //public string PostOffices { get; set; }


        public int? Phone_id { get; set; }
        //public string Phone { get; set; }
        [Display(Name = "PuhNro")]
        public string PhoneNum_1 { get; set; }

        public int? User_id { get; set; }
        //public string User { get; set; }
        [Display(Name = "Käyttäjätunnus")]
        public string UserIdentity { get; set; }

        public string Password { get; set; }


        [Display(Name = "Alkaen klo")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:MM}", ApplyFormatInEditMode = true)]
        public DateTime? Start { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:MM}", ApplyFormatInEditMode = true)]
        [Display(Name = "Loppuu klo")]
        public DateTime? End { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Pvm")]
        public DateTime? Date { get; set; }

        //public virtual ICollection<StudentGroup> StudentGroup { get; set; }
        public virtual ICollection<Phone> Phone { get; set; }
        public virtual ICollection<PostOffices> PostOffices { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
        public virtual ICollection<TreatmentReport> TreatmentReport { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
