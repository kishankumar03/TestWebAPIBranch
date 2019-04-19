using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestWebAPIBranch.Models
{
    [Table("UserInfo")]
    public class UserInfo
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        //public int age { get; set; }
        //public string emailId { get; set; }
        //public string addressLine1 { get; set; }
        //public string addressLine2 { get; set; }
        //public string city { get; set; }
        //public string state { get; set; }
        //public string country { get; set; }
        //public int zipCode { get; set; }

        [Key, ForeignKey("UserLogin")]
        public int userId { get; set; }
        public virtual UserLogin UserLogin { get; set; }
    }
}