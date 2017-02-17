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
    
    public partial class Treatment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Treatment()
        {
            this.Customer = new HashSet<Customer>();
            this.Personnel = new HashSet<Personnel>();
            this.Phone = new HashSet<Phone>();
            this.PostOffices = new HashSet<PostOffices>();
            this.Reservation = new HashSet<Reservation>();
            this.TreatmentPlace1 = new HashSet<TreatmentPlace>();
            this.Studentx1 = new HashSet<Studentx>();
            this.TreatmentOffice1 = new HashSet<TreatmentOffice>();
            this.User1 = new HashSet<User>();
        }
    
        public int Treatment_id { get; set; }
        public string TreatmentName { get; set; }
        public string TreatmentTime { get; set; }
        public string TreatmentPrice { get; set; }
        public Nullable<int> Personnel_id { get; set; }
        public Nullable<int> Phone_id { get; set; }
        public Nullable<int> Post_id { get; set; }
        public Nullable<int> Reservation_id { get; set; }
        public Nullable<int> Student_id { get; set; }
        public Nullable<int> Customer_id { get; set; }
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservation { get; set; }
        public virtual Reservation Reservation1 { get; set; }
        public virtual TreatmentPlace TreatmentPlace { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TreatmentPlace> TreatmentPlace1 { get; set; }
        public virtual Studentx Studentx { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Studentx> Studentx1 { get; set; }
        public virtual TreatmentOffice TreatmentOffice { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TreatmentOffice> TreatmentOffice1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> User1 { get; set; }
    }
}
