﻿using Bookstore_NhibernateApp.Models;
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
        [Route("AddToCart")]
        [HttpPost]
        public HttpResponseMessage AddToCart(CartModel cart)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(cart);
                        transaction.Commit();
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, cart);
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
        public CartModel getCart(int id)
        {
            var cart = session.Get<CartModel>(id);
            return cart;
        }

      
        [HttpPut]
        public HttpResponseMessage UpdateCart(int cartId, CartModel cartModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cart = session.Get<CartModel>(cartId);
                    cart.OrderQuantity = cartModel.OrderQuantity;
                    cart.UserId = cartModel.UserId;
                    cart.BookId = cartModel.BookId;
                  

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.SaveOrUpdate(cart);
                        transaction.Commit();
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, cart);
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
