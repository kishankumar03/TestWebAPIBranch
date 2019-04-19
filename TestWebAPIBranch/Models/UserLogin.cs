using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestWebAPIBranch.Models
{
    [Table("UserLogin")]
    public class UserLogin
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}