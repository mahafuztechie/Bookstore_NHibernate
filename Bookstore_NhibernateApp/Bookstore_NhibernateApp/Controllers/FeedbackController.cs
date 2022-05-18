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
    public class FeedbackController : ApiController
    {
        ISession session = OpenSessionNHibernate.OpenSession();

        [Route("feedback")]
        [HttpPost]
        public HttpResponseMessage AddFeedback(FeedBackModel feed)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(feed);
                        transaction.Commit();
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, feed);
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

        [HttpGet]
        public List<FeedBackModel> getFeedback(int bookId)
        {
            List<FeedBackModel> ListFeedBacks = new List<FeedBackModel>();
            List<FeedBackModel>feedbacks  = session.Query<FeedBackModel>().ToList();
            foreach (var Feedback in feedbacks)
            {
                if (Feedback.BookId == bookId)
                {
                    ListFeedBacks.Add(Feedback);
                }
            }

            return ListFeedBacks;
        }


    }
}
