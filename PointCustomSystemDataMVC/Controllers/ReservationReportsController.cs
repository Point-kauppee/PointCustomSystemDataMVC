using PointCustomSystemDataMVC.Models;
using PointCustomSystemDataMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PointCustomSystemDataMVC.Controllers
{
    public class ReservationReportsController : Controller
    {
        // GET: Reports
        public ActionResult HoursPerReservation()
        {
            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                DateTime today = DateTime.Today;
                DateTime tomorrow = today.AddDays(1);

                // haetaan kaikki kuluvan päivän tuntikirjaukset
                List<Reservation> allReservationsToday = (from rs in entities.Reservation
                                                          where (rs.Start > today) &&
                                                          (rs.Start < tomorrow) &&
                                                          (rs.TreatmentCompleted == true)
                                                          select rs).ToList();

                // ryhmitellään kirjaukset tehtävittäin, ja lasketaan kestot
                List<HoursPerReservationModel> model = new List<HoursPerReservationModel>();

                foreach (Reservation reservation in allReservationsToday)
                {
                    int reservationId = reservation.Reservation_id;
                    HoursPerReservationModel existing = model.Where(
                        m => m.Reservation_id == reservationId).FirstOrDefault();

                    if (existing != null)
                    {
                        existing.TotalHours += (reservation.End.Value - reservation.Start.Value).TotalHours;
                    }
                    else

                    {
                        existing = new HoursPerReservationModel()
                        {
                            Reservation_id = reservationId,
                            TreatmentName = reservation.Treatment.TreatmentName,
                            TotalHours = (reservation.End.Value - reservation.Start.Value).TotalHours
                        };
                        model.Add(existing);
                    }
                }
                return View(model);
            }
            finally
            {
                entities.Dispose();
            }
        }

        public ActionResult HoursPerReservationAsExcel()
        {
            // TODO: hae tiedot tietokannasta!
            StringBuilder csv = new StringBuilder();

            // luodaan CSV-muotoinen tiedosto
            csv.AppendLine("Matti;123,5");
            csv.AppendLine("Jesse;86,25");
            csv.AppendLine("Kaisa;99,00");

            // palautetaan CSV-tiedot selaimelle
            byte[] buffer = Encoding.UTF8.GetBytes(csv.ToString());
            return File(buffer, "text/csv", "Palvelutunnit.csv");
        }

        ////public ActionResult HoursPerReservationAsExcel2()
        ////{
        ////    StringBuilder csv = new StringBuilder();

        ////    // luodaan CSV-muotoinen tiedosto
        ////    JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

        ////    try
        ////    {
        ////        DateTime today = DateTime.Today;
        ////        DateTime tomorrow = today.AddDays(1);

        ////        // haetaan kaikki kuluvan päivän tuntikirjaukset
        ////        List<Reservation> allReservationsToday = (from ts in entities.Reservation
        ////                                              where (ts.Start > today) &&
        ////                                              (ts.Start < tomorrow) &&
        ////                                              (ts.TreatmentCompleted == true)
        ////                                              select ts).ToList();

        ////        foreach (Reservation reservation in allReservationsToday)
        ////        {
        ////            csv.AppendLine(reservation.Customer_id + ";" +
        ////                reservation.Start + ";" + reservation.End + ";");
        ////        }
        ////    }
        ////    finally
        ////    {
        ////        entities.Dispose();
        ////    }

        ////    // palautetaan CSV-tiedot selaimelle

        ////    byte[] buffer = Encoding.UTF8.GetBytes(csv.ToString());
        ////    return File(buffer, "text/csv", "Työtunnit.csv");
        ////}

        ////public ActionResult GetTimesheetCounts(string onlyComplete)
        ////{
        ////    ReportChartDataViewModel model = new ReportChartDataViewModel();

        ////    JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
        ////    try
        ////    {
        ////        model.Labels = (from wa in entities.Reservation
        ////                        orderby wa.Reservation_id
        ////                        select wa.Title).ToArray();

        ////        if (onlyComplete == "1")
        ////        {
        ////            model.Counts = (from ts in entities.Reservation
        ////                            where (ts.TreatmentCompleted == true)
        ////                            orderby ts.Reservation_id
        ////                            group ts by ts.Reservation_id into grp
        ////                            select grp.Count()).ToArray();
        ////        }
        ////        else
        ////        {
        ////            model.Counts = (from ts in entities.Reservation
        ////                            orderby ts.Reservation_id
        ////                            group ts by ts.Reservation_id into grp
        ////                            select grp.Count()).ToArray();
        ////        }

        ////    }
        ////    finally
        ////    {
        ////        entities.Dispose();
        ////    }

        ////    return Json(model, JsonRequestBehavior.AllowGet);
        ////}
    }
}