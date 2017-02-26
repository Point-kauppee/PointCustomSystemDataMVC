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

        public string Note { get; set; }

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

        [Display(Name = "Asiakas")]
        public string FullNameA
        {
            get { return FirstName + " " + LastName; }
        }
    }
}