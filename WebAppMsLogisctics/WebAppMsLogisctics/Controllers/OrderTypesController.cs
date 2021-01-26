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

namespace WebAppMsLogisctics.Controllers
{
    public class OrderTypesController : ApiController
    {
        private EntitiesMsLog db = new EntitiesMsLog();

        // GET: api/OrderTypes
        [ResponseType(typeof(Models.ResponseOrderType))]
        public IHttpActionResult GetOrderType()
        {
            return Ok(db.OrderType.ToList().ConvertAll(p => new Models.ResponseOrderType(p)).ToList());
        }

        // GET: api/OrderTypes/5
        [ResponseType(typeof(Models.ResponseOrderType))]
        public IHttpActionResult GetOrderType(int id)
        {
            OrderType orderType = db.OrderType.Find(id);
            if (orderType == null)
            {
                return NotFound();
            }

            return Ok(new Models.ResponseOrderType(orderType));
        }

        // PUT: api/OrderTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrderType(int id, OrderType orderType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderType.Id)
            {
                return BadRequest();
            }

            db.Entry(orderType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderTypeExists(id))
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

        // POST: api/OrderTypes
        [ResponseType(typeof(OrderType))]
        public IHttpActionResult PostOrderType(OrderType orderType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderType.Add(orderType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrderTypeExists(orderType.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = orderType.Id }, orderType);
        }

        // DELETE: api/OrderTypes/5
        [ResponseType(typeof(OrderType))]
        public IHttpActionResult DeleteOrderType(int id)
        {
            OrderType orderType = db.OrderType.Find(id);
            if (orderType == null)
            {
                return NotFound();
            }

            db.OrderType.Remove(orderType);
            db.SaveChanges();

            return Ok(orderType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderTypeExists(int id)
        {
            return db.OrderType.Count(e => e.Id == id) > 0;
        }
    }
}