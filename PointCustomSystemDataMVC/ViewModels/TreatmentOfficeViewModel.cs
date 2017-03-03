using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointCustomSystemDataMVC.ViewModels
{
    public class TreatmentOfficeViewModel
    {
        public int? Reservation_id { get; set; }

 
        [Display(Name = "Huomiot")]
        public string Note { get; set; }

        public string Address { get; set; }

        public string TretamentOffice { get; set; }
        public int? TreatmentOffice_id { get; set; }
        public string TreatmentOfficeName { get; set; }

        public string TretamentPlace { get; set; }
        public int? Treatmentplace_id { get; set; }
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

    }
}