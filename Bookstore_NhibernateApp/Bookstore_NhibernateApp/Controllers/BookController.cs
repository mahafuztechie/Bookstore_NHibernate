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

        [Route("AddBook")]
        [HttpPost]
        public HttpResponseMessage AddNewBook(BookModel book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(book);
                        transaction.Commit();
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, book);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error !");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

     

    }
}
