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
using Rule = WebAppMsLogisctics.Entites.Rule;

namespace WebAppMsLogisctics.Controllers
{
    public class RulesController : ApiController
    {
        private EntitiesMsLog db = new EntitiesMsLog();

        // GET: api/Rules
        [Route("api/GetAllRules"), ResponseType(typeof(List<ResponseRule>))]
        public IHttpActionResult GetAllRule()
        {
            return Ok(db.Rule.ToList().ConvertAll(p=> new ResponseRule(p)).ToList());
        }

        // GET: api/Rules/5
        [ResponseType(typeof(ResponseRule))]
        public IHttpActionResult GetRule(int id)
        {
            Rule rule = db.Rule.Find(id);
            if (rule == null)
            {
                return NotFound();
            }

            return Ok(new ResponseRule(rule));
        }

        // PUT: api/Rules/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRule(int id, Rule rule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rule.Id)
            {
                return BadRequest();
            }

            db.Entry(rule).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuleExists(id))
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

        // POST: api/Rules
        [ResponseType(typeof(Rule))]
        public IHttpActionResult PostRule(Rule rule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rule.Add(rule);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rule.Id }, rule);
        }

        // DELETE: api/Rules/5
        [ResponseType(typeof(Rule))]
        public IHttpActionResult DeleteRule(int id)
        {
            Rule rule = db.Rule.Find(id);
            if (rule == null)
            {
                return NotFound();
            }

            db.Rule.Remove(rule);
            db.SaveChanges();

            return Ok(rule);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RuleExists(int id)
        {
            return db.Rule.Count(e => e.Id == id) > 0;
        }
    }
}