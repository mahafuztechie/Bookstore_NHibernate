﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookstore_NhibernateApp.Models
{
    public class AddressTypeModel
    {
        public virtual int ? TypeId { get; set; }
        public virtual string TypeName { get; set; }
    }
}