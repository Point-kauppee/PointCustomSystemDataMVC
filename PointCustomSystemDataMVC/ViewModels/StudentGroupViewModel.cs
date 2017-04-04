using PointCustomSystemDataMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointCustomSystemDataMVC.ViewModels
{
    public class StudentGroupViewModel
    {
        public StudentGroupViewModel()
        {
            this.Studentx = new HashSet<Studentx>();
        }

        public int StudentGroup_id { get; set; }
        public string StudentGroupName { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> LastModifiedAt { get; set; }
        public Nullable<System.DateTime> DeletedAt { get; set; }

        public int? Student_id { get; set; }

        [Display(Name = "Hoitaja Etunimi")]
        public string FirstNameH { get; set; }
        [Display(Name = "Hoitaja Sukunimi")]
        public string LastNameH { get; set; }
        [Display(Name = "Hoitaja")]
        public string FullNameH
        {
            get { return FirstNameH + " " + LastNameH; }
        }

        public virtual ICollection<Studentx> Studentx { get; set; }
    }
}