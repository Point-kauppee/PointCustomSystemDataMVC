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
            this.Customer = new HashSet<Customer>();
            this.Personnel = new HashSet<Personnel>();
            this.Phone = new HashSet<Phone>();
            this.PostOffices = new HashSet<PostOffices>();
            this.Treatment1 = new HashSet<Treatment>();
            this.TreatmentOffice1 = new HashSet<TreatmentOffice>();
            this.TreatmentPlace1 = new HashSet<TreatmentPlace>();
            this.User1 = new HashSet<User>();
        }
    
        public int Reservation_id { get; set; }
        public string TreatmentName { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Type { get; set; }
        public string Note { get; set; }
        public Nullable<int> Personnel_id { get; set; }
        public Nullable<int> Phone_id { get; set; }
        public Nullable<int> Post_id { get; set; }
        public Nullable<int> Customer_id { get; set; }
        public Nullable<int> Student_id { get; set; }
        public Nullable<int> Treatment_id { get; set; }
        public Nullable<int> TreatmentOffice_id { get; set; }
        public Nullable<int> TreatmentPlace_id { get; set; }
        public Nullable<int> User_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customer { get; set; }
        public virtual Customer Customer1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Personnel> Personnel { get; set; }
        public virtual Personnel Personnel1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Phone> Phone { get; set; }
        public virtual Phone Phone1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostOffices> PostOffices { get; set; }
        public virtual PostOffices PostOffices1 { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual TreatmentOffice TreatmentOffice { get; set; }
        public virtual TreatmentPlace TreatmentPlace { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Treatment> Treatment1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TreatmentOffice> TreatmentOffice1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TreatmentPlace> TreatmentPlace1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> User1 { get; set; }
        public virtual Studentx Studentx { get; set; }
    }
}
