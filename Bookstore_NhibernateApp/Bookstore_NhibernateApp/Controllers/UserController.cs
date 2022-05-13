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
   
    
    public class UserController : ApiController
    {
        ISession session = OpenSessionNHibernate.OpenSession();
        
        [HttpPost]
        public HttpResponseMessage RegisterUser(UserModel User)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                       // session.CreateSQLQuery("EXEC spRegisterUser @FullName = N'" + User.Fullname+ "',@Email = N'" + User.Email + "',@Password = N'" + User.Password + "',@MobileNumber = N'" + User.MobileNumber + "'").ExecuteUpdate();
                        session.Save(User);
                        transaction.Commit();
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, User);
                  
                  
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
