using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookstore_NhibernateApp.Models
{
    public class ResetPassModel
    {
        public virtual string NewPassword { get; set; }
        public virtual string ConfrimPassword { get; set; }
    }
}