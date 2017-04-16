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

        [Display(Name = "Hoitajaryhmä")]
        public string StudentGroupName { get; set; }
        [Display(Name = "Tila")]
        public bool? Active { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Tieto tallennettu pvm")]
        public Nullable<System.DateTime> CreatedAt { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Muokattu pvm")]
        public Nullable<System.DateTime> LastModifiedAt { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Arkistointi pvm")]
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