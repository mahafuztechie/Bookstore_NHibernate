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

        [Route("registerUser")]
        [HttpPost]
        public HttpResponseMessage RegisterUser(UserModel User)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
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

        [Route("loginUser")]
        [HttpPost]
        public HttpResponseMessage login(LoginModel login)
        {
            try
            {
                string token = "";
                if (ModelState.IsValid)
                {
                    List<UserModel> Users = session.Query<UserModel>().ToList();
                    foreach (var user in Users)
                    {
                        if (user.Email== login.Email && user.Password == login.Password)
                        {
                            token = GenToken.GenerateJWTToken(user.Email, user.UserId);
                            break;

                        }
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, token);
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


        [Route("forgotPass")]
        [HttpPost]
        public HttpResponseMessage ForgotPass(ForgotPassModel forgotPass)
        {
            try
            {
                string token = "";
                if (ModelState.IsValid)
                {
                    List<UserModel> Users = session.Query<UserModel>().ToList();
                    foreach (var user in Users)
                    {
                        if (user.Email == forgotPass.Email)
                        {
                            token = GenToken.GenerateJWTToken(user.Email, user.UserId);
                            new MSMQ().Sender(token);
                            break;

                        }
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, "token sent to your email successfylly");
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

        [Route("resetPass")]
        [HttpPut]
       
        public HttpResponseMessage ResetPass(ResetPassModel resetPass)
        {
            try
            {
              //  var user = User.Claims.FirstOrDefault(e => e).Value;
                

                if (ModelState.IsValid)
                {
                  List<UserModel> Users = session.Query<UserModel>().ToList();
                 
                    foreach (var user in Users)
                    {
                        if (user.Email == "janto4115@gmail.com")
                        {

                            if (resetPass.NewPassword == resetPass.ConfrimPassword)
                            {
                                user.Password = resetPass.ConfrimPassword;
                                using (ITransaction transaction = session.BeginTransaction())
                                {
                                    session.SaveOrUpdate(user);
                                    transaction.Commit();
                                }
                               break;
                            }

                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, "password reset successful");
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
