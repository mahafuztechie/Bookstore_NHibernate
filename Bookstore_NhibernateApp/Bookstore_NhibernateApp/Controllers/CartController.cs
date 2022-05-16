using Bookstore_NhibernateApp.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bookstore_NhibernateApp.Controllers
{
    public class CartController : ApiController
    {
        ISession session = OpenSessionNHibernate.OpenSession();
        //Get all Notes

        [Route("getCarts")]

        public List<CartModel> GetCartList()
        {
            List<CartModel> Collab = session.Query<CartModel>().ToList();
            return Collab;
        }
  
    }
}
