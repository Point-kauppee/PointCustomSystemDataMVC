using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PointCustomSystemDataMVC.ViewModels
{
    public class CustomerParentViewModel
    {

        public CustomerViewModel CustomerViewModel {get; set;}

        public CustomerDetailViewModel CustomerDetailViewModel { get; set; }
    }
}