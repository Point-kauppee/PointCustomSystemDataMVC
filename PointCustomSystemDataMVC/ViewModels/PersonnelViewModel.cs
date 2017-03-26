using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointCustomSystemDataMVC.ViewModels
{
    public class PersonnelViewModel
    {
        public int Personnel_id { get; set; }
        public string Personnel { get; set; }
        public int? Customer_id { get; set; }

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

        [Display(Name = "Etunimi")]
        public string FirstNameP { get; set; }

        [Display(Name = "Sukunimi")]
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

        public string Note { get; set; }
        [Display(Name = "Henkilötieto tallennettu")]
        public DateTime? CreatedAt { get; set; }
        [Display(Name = "Henkilötietoja muokattu")]
        public DateTime? LastModifiedAt { get; set; }
        [Display(Name = "Arkistointi pvm")]
        public DateTime? DeletedAt { get; set; }

        [Display(Name = "Tila")]   
        public bool? Active { get; set; }
        [Display(Name = "Tiedot")]
        public string Information { get; set; }

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
        public string User { get; set; }
        [Display(Name = "Käyttäjätunnus")]
        public string UserIdentity { get; set; }
        [Display(Name = "Salasana")]
        public string Password { get; set; }
        

    }
}