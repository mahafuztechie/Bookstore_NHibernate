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
    public class BookController : ApiController
    {
        ISession session = OpenSessionNHibernate.OpenSession();
        [Route("getBooks")]
        public List<BookModel> GetBookList()
        {
            List<BookModel> Books = session.Query<BookModel>().ToList();
            return Books;
        }
       
      
    }
}
