//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PointCustomSystemDataMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reservation()
        {
            this.TreatmentReport = new HashSet<TreatmentReport>();
        }
    
        public int Reservation_id { get; set; }
        public Nullable<System.DateTime> Start { get; set; }
        public Nullable<System.DateTime> End { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<bool> TreatmentPaid { get; set; }
        public Nullable<System.DateTime> TreatmentPaidDate { get; set; }
        public Nullable<int> Type { get; set; }
        public string Note { get; set; }
        public string CalendarTitle { get; set; }
        public string TreatmentReportTexts { get; set; }
        public Nullable<int> Personnel_id { get; set; }
        public Nullable<int> Student_id { get; set; }
        public Nullable<int> Customer_id { get; set; }
        public Nullable<int> TreatmentPlace_id { get; set; }
        public Nullable<int> Treatment_id { get; set; }
        public Nullable<int> TreatmentOffice_id { get; set; }
        public Nullable<int> User_id { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Personnel Personnel { get; set; }
        public virtual Studentx Studentx { get; set; }
        public virtual TreatmentPlace TreatmentPlace { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual User User { get; set; }
        public virtual TreatmentOffice TreatmentOffice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TreatmentReport> TreatmentReport { get; set; }
    }
}
