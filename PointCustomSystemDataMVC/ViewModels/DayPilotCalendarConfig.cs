using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using DayPilot.Web.Mvc.Data;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Enums.Calendar;
using DayPilot.Web.Mvc.Events.Calendar;
using DayPilot.Web.Mvc.Utils;
using System.ComponentModel.DataAnnotations;

namespace PointCustomSystemDataMVC.ViewModels
{
    public class DayPilotCalendarConfig
    {
        public int Customer_id { get; set; }
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

        public int Student_id { get; set; }

        public int Treatment_id { get; set; }
        [Display(Name = "PostiNro")]
        public string PostalCode { get; set; }
        [Display(Name = "Postiosoite")]
        public string PostOffice { get; set; }
        public string PostOffices { get; set; }


        public int? Phone_id { get; set; }
        public string Phone { get; set; }
        public string PhoneNum_1 { get; set; }

        public Nullable<int> User_id { get; set; }
        public string User { get; set; }
        public string UserIdentity { get; set; }

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

        public int Reservation_id { get; set; }

        public string TreatmentName { get; set; }
        [Display(Name = "Palvelu")]
        public string TreatmentPlaceName { get; set; }

        public string TreatmentOfficeName { get; set; }

        public int TreatmentPlace_id { get; set; }

        [Display(Name = "Alkaen klo")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]

        public Nullable<System.DateTime> Start { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Loppuu klo")]
        public Nullable<System.DateTime> End { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Pvm")]
        public Nullable<System.DateTime> Date { get; set; }

        public string DataDateField { get; set; }

        public string EventManager { get; set; }

        public string BackendUrl { get; set; }
        public string BorderColor { get; set; }
        public int BusinessBeginsHour { get; set; }
        public int BusinessEndsHour { get; set; }

        public string CellBackColor { get; set; }
        public string CellBorderColor { get; set; }
        public int CellHeight { get; set; }
        public int ColumnMarginRight { get; set; }
        public string CornerBackColor { get; set; }
        public bool CssOnly { get; set; }

        [Obsolete("Use Theme instead.")]
        public string CssClassPrefix { get { return Theme; } set { Theme = value; } }
        public string Theme { get; set; }

        public int Days { get; set; }

        public string EventBackColor { get; set; }
        public string EventBorderColor { get; set; }
        public string EventFontFamily { get; set; }
        public string EventFontSize { get; set; }
        public string EventFontColor { get; set; }
        public string EventHeaderFontSize { get; set; }
        public string EventHeaderFontFamily { get; set; }
        public string EventHeaderFontColor { get; set; }
        public int EventHeaderHeight { get; set; }
        public bool EventHeaderVisible { get; set; }

        public string HeaderDateFormat { get; set; }
        public string HeaderFontSize { get; set; }
        public string HeaderFontFamily { get; set; }
        public string HeaderFontColor { get; set; }
        public int HeaderHeight { get; set; }
        public HeightSpec HeightSpec { get; set; }
        public string HourHalfBorderColor { get; set; }
        public string HourBorderColor { get; set; }
        public string HourFontColor { get; set; }
        public string HourFontFamily { get; set; }
        public string HourFontSize { get; set; }
        public string HourNameBackColor { get; set; }
        public string HourNameBorderColor { get; set; }
        public int HourWidth { get; set; }

        public string LoadingLabelText { get; set; }
        public bool LoadingLabelVisible { get; set; }
        public string LoadingLabelFontSize { get; set; }
        public string LoadingLabelFontFamily { get; set; }
        public string LoadingLabelFontColor { get; set; }
        public string LoadingLabelBackColor { get; set; }

        public bool ShowToolTip { get; set; }
        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                switch (ViewType)
                {
                    case ViewType.WorkWeek:
                        return Week.FirstWorkingDayOfWeek(_startDate);
                    case ViewType.Week:
                        return Week.FirstDayOfWeek(_startDate, Week.Resolve(WeekStarts));
                }

                return _startDate;
            }
            set { _startDate = value; }
        }

        private int? _ScrollPositionHour;
        public int? ScrollPositionHour
        {
            get
            {
                if (_ScrollPositionHour == null)
                    return BusinessBeginsHour;

                return _ScrollPositionHour;
            }
            set { _ScrollPositionHour = value; }
        }


        public ViewType ViewType { get; set; }
        public TimeFormat TimeFormat { get; set; }
        public WeekStarts WeekStarts { get; set; }
        public string Width { get; set; }
        public bool DurationBarVisible { get; set; }

        public EventClickHandlingType EventClickHandling { get; set; }
        public EventMoveHandlingType EventMoveHandling { get; set; }
        public EventResizeHandlingType EventResizeHandling { get; set; }
        public TimeRangeSelectedHandlingType TimeRangeSelectedHandling { get; set; }

        public string EventClickJavaScript { get; set; }
        public string EventMoveJavaScript { get; set; }
        public string EventResizeJavaScript { get; set; }
        public string TimeRangeSelectedJavaScript { get; set; }


        public DayPilotCalendarConfig()
        {

            BorderColor = "#CED2CE";
            BusinessBeginsHour = 9;
            BusinessEndsHour = 19;

            CellBackColor = "white";

            CellBorderColor = "#DEDFDE";
            CellHeight = 100;
            ColumnMarginRight = 5;
            CornerBackColor = "#F3F3F9";

            CssOnly = true;
            Days = 1;
            DurationBarVisible = true;

            EventBackColor = "#638EDE";
            EventBorderColor = "#2951A5";
            EventFontSize = "8pt";
            EventFontColor = "#ffffff";
            EventFontFamily = "Tahoma, Arial, Helvetica, sans-serif";
            EventHeaderFontSize = "8pt";
            EventHeaderFontFamily = "Tahoma, Arial, Helvetica, sans-serif";
            EventHeaderFontColor = "#ffffff";
            EventHeaderHeight = 14;
            EventHeaderVisible = true;

            HeaderDateFormat = "d";
            HeaderFontSize = "10pt";
            HeaderFontColor = "#42658C";
            HeaderFontFamily = "Tahoma, Arial, Helvetica, sans-serif";
            HeaderHeight = 21;
            HeightSpec = HeightSpec.BusinessHours;
            HourHalfBorderColor = "#EBEDEB";
            HourBorderColor = "#DEDFDE";
            HourFontColor = "#42658C";
            HourFontFamily = "Tahoma, Arial, Helvetica, sans-serif";
            HourFontSize = "16pt";
            HourNameBackColor = "#F3F3F9";
            HourNameBorderColor = "#DEDFDE";
            HourWidth = 45;

            LoadingLabelText = "Loading...";
            LoadingLabelVisible = true;
            LoadingLabelFontSize = "10pt";
            LoadingLabelFontColor = "#ffffff";
            LoadingLabelFontFamily = "Tahoma, Arial, Helvetica, sans-serif";
            LoadingLabelBackColor = "#ff0000";

            StartDate = DateTime.Today;
            TimeFormat = TimeFormat.Auto;
            WeekStarts = WeekStarts.Auto;
            Width = "100%";

            EventClickHandling = EventClickHandlingType.Disabled;
            EventMoveHandling = EventMoveHandlingType.Disabled;
            EventResizeHandling = EventResizeHandlingType.Disabled;
            TimeRangeSelectedHandling = TimeRangeSelectedHandlingType.Disabled;
        }

        public List<Hashtable> GetColumns()
        {
            var _columns = new List<Hashtable>();

            int dayCount = (int)(EndDate - StartDate).TotalDays + 1;

            for (int i = 0; i < dayCount; i++)
            {
                DateTime date = StartDate.AddDays(i);

                Column col = new Column(date.ToString(HeaderDateFormat), null);
                col.Date = date;
                Hashtable c = GetColumn(col);
                _columns.Add(c);

            }
            return _columns;
        }

        private Hashtable GetColumn(Column column)
        {
            Hashtable c = new Hashtable();

            c["Name"] = column.Name;
            c["Start"] = column.Date.ToString("s");
            c["ToolTip"] = column.ToolTip;
            c["InnerHTML"] = column.Name;

            return c;

        }
        private DateTime EndDate
        {
            get
            {
                return StartDate.AddDays(Days - 1);
            }
        }
    }
}




