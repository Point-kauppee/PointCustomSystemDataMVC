using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PointCustomSystemDataMVC.ViewModels
{
    public class HoursPerReservationModel
    {
        public int TreatmentHour_id { get; set; }

        public int Reservation_id { get; set; }

       
        public string TreatmentName { get; set; }

        public bool? TreatmentComplete { get; set; }
       
        public double TotalHours { get; set; }
    }
}