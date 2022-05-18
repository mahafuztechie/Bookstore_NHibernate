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


        [HttpPut]
        public HttpResponseMessage UpdateFeedback(int FeedbackId, FeedBackModel feedbackModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var FeedBack= session.Get<FeedBackModel>(FeedbackId);
                    FeedBack.Comment = feedbackModel.Comment;
                    FeedBack.Rating = feedbackModel.Rating;
                    FeedBack.UserId = feedbackModel.UserId;
                    FeedBack.BookId = feedbackModel.BookId;
                   

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.SaveOrUpdate(FeedBack);
                        transaction.Commit();
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, FeedBack);
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

        [HttpDelete]
        public HttpResponseMessage DeleteFeedback(int id, int userId)
        {
            try
            {
                var Feed = session.Get<FeedBackModel>(id);

                if (Feed.UserId == userId)
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(Feed);
                        transaction.Commit();
                        return Request.CreateResponse(HttpStatusCode.OK, "Feedback deleted Successfully");
                    }

                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error !");
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
