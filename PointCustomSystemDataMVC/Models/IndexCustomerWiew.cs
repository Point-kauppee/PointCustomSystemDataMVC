using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PointCustomSystemDataMVC.Models
{
    public class IndexCustomerWiew
    {
        public int Customer_id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identity { get; set; }
        public string Notes { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public string PhoneNum_1 { get; set; }
        public string PostalCode { get; set; }
        public string PostOffice { get; set; }
        public Nullable<int> Phone_id { get; set; }
        public string UserIdentity { get; set; }
        public Nullable<int> Post_id { get; set; }

        public Nullable<int> User_id { get; set; }    
        public string Password { get; set; }

    }
}