using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAppMsLogisctics.Entites;
using WebAppMsLogisctics.Models;

namespace WebAppMsLogisctics.Controllers
{
    public class OrdersController : ApiController
    {
        private EntitiesMsLog db = new EntitiesMsLog();

        // GET: api/Orders
        public IQueryable<Order> GetOrder()
        {
            return db.Order;
        }

        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(int id)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }
        // GET: get Orders by CLients id (For Client Look)
        [ResponseType(typeof(List<ResponseOrder>)), Route("api/OrdersByUser")]
        public IHttpActionResult GetOrdersByUser(int clientId)
        {
            //TODO: Возможно будет смысл разделять сразу и не показывать отмененные заказы (см таблицу со статусами)
            List<Order> currOrderList = db.Order.ToList().Where(p => p.ClientId == clientId).ToList();
            //Тут можно сделать проверку на нулевость листа и отправлять 404, но в общем если он будет пустую возвращать, то без разницы
            //Если хочешь, можешь прикрутить. Там обычный if
            var currList = currOrderList.ConvertAll(p => new ResponseOrder(p, true)).ToList();
            return Ok(currList);
        }

        // GET: get Orders by Courier id (For Courier Look)
        [ResponseType(typeof(List<ResponseOrder>)), Route("api/OrdersWithoutCourier")]
        public IHttpActionResult GetOrdersWithoutCourier()
        {
            List<Order> currOrderList = db.Order.ToList().Where(p => p.CourierId == null).ToList();
            //Тут можно сделать проверку на нулевость листа и отправлять 404, но в общем если он будет пустую возвращать, то без разницы
            //Если хочешь, можешь прикрутить. Там обычный if
            var currList = currOrderList.ConvertAll(p => new ResponseOrder(p, false)).ToList();
            return Ok(currList);
        }
        [ResponseType(typeof(List<ResponseOrder>)), Route("api/OrderWithCourier")]
        public IHttpActionResult GetOrderWithCourier()
        {
            List<Order> currOrderList = db.Order.ToList().Where(p => p.CourierId != null && p.OrderStatus.Id==4).ToList();
            //Тут можно сделать проверку на нулевость листа и отправлять 404, но в общем если он будет пустую возвращать, то без разницы
            //Если хочешь, можешь прикрутить. Там обычный if
            var currList = currOrderList.ConvertAll(p => new ResponseOrder(p, false)).ToList();
            return Ok(currList);
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder(int id, Order order)
        {
            //if (order.Comment.Length > 1000)
            //    ModelState.AddModelError("Comment Lenght", "Comment Lenght cant be more than 1000 symbols");
            //if (order.Code.Length > 100)
            //    ModelState.AddModelError("Code Lenght", "Code lenght cant be more than 100 symbols");
            //if (order.Code.Length == 0)
            //    ModelState.AddModelError("Code Lenght", "Code cant be null");
            //if (db.OrderStatus.ToList().Where(p => p.Id == order.StatusId).FirstOrDefault() == null)
            //    ModelState.AddModelError("OrderStatus", "We dont have this order status");
            //if (db.Client.ToList().Where(p => p.Id == order.ClientId).FirstOrDefault() == null)
            //    ModelState.AddModelError("Client id", "We dont have this Client");
            //if (order.ClientId == 0)
            //    ModelState.AddModelError("Client id", "We need client id");
            //if (db.Location.ToList().Where(p => p.Id == order.StartLocation).FirstOrDefault() == null)
            //    ModelState.AddModelError("Start location id", "We dont have this location");
            //if (order.StartLocation == 0)
            //    ModelState.AddModelError("Start location id", "We need Start loaction id");
            //if (db.Location.ToList().Where(p => p.Id == order.EndLocation).FirstOrDefault() == null)
            //    ModelState.AddModelError("End location id", "We dont have this location");
            //if (order.EndLocation == 0)
            //    ModelState.AddModelError("End location id", "We need end location id");
            //if (db.Manager.ToList().Where(p => p.Id == order.ManagerId).FirstOrDefault() == null)
            //    ModelState.AddModelError("Manager id", "We dont have this manager");
            //if (order.ManagerId == 0)
            //    ModelState.AddModelError("Manager id", "We need manager id");
            //if (db.OrderType.ToList().Where(p => p.Id == order.OrderTypeId).FirstOrDefault() == null)
            //    ModelState.AddModelError("Order type id", "We dont have this order type");
            //if (order.CourierId != 0 && db.Courier.ToList().Where(p => p.Id == order.CourierId).FirstOrDefault() == null)
            //    ModelState.AddModelError("Courier id", "We dont have this Courier");
            //if (order.CourierRate > 5 || order.CourierRate < 0)
            //    ModelState.AddModelError("Courier Rate", "Invalid range for rate (Courier)");
            //if (order.ClientRate > 5 || order.ClientRate < 0)
            //    ModelState.AddModelError("Client Rate", "Invalid range for rate (Client)");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Orders
        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(Order order)
        {
            //if (order.Comment.Length > 1000)
            //    ModelState.AddModelError("Comment Lenght", "Comment Lenght cant be more than 1000 symbols");
            //if (order.Code.Length > 100)
            //    ModelState.AddModelError("Code Lenght", "Code lenght cant be more than 100 symbols");
            //if (order.Code.Length == 0)
            //    ModelState.AddModelError("Code Lenght", "Code cant be null");
            //if (db.OrderStatus.ToList().Where(p=>p.Id == order.StatusId).FirstOrDefault() == null)
            //    ModelState.AddModelError("OrderStatus", "We dont have this order status");
            //if (db.Client.ToList().Where(p => p.Id == order.ClientId).FirstOrDefault() == null)
            //    ModelState.AddModelError("Client id", "We dont have this Client");
            //if (order.ClientId == 0)
            //    ModelState.AddModelError("Client id", "We need client id");
            //if (db.Location.ToList().Where(p => p.Id == order.StartLocation).FirstOrDefault() == null)
            //    ModelState.AddModelError("Start location id", "We dont have this location");
            //if (order.StartLocation == 0)
            //    ModelState.AddModelError("Start location id", "We need Start loaction id");
            //if (db.Location.ToList().Where(p => p.Id == order.EndLocation).FirstOrDefault() == null)
            //    ModelState.AddModelError("End location id", "We dont have this location");
            //if (order.EndLocation == 0)
            //    ModelState.AddModelError("End location id", "We need end location id");
            //if (db.Manager.ToList().Where(p => p.Id == order.ManagerId).FirstOrDefault() == null)
            //    ModelState.AddModelError("Manager id", "We dont have this manager");
            //if (order.ManagerId == 0)
            //    ModelState.AddModelError("Manager id", "We need manager id");
            //if (db.OrderType.ToList().Where(p => p.Id == order.OrderTypeId).FirstOrDefault() == null)
            //    ModelState.AddModelError("Order type id", "We dont have this order type");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Order.Add(order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Order.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Order.Count(e => e.Id == id) > 0;
        }
    }
}