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
using testingDriverAppWebapi.Models;

namespace testingDriverAppWebapi.Controllers
{
    public class HorsesController : ApiController
    {
        private testingDriverAppWebapiContext db = new testingDriverAppWebapiContext();

        // GET: api/Horses
        public IQueryable<Horse> GetHorses()
        {
            return db.Horses;
        }

        // GET: api/Horses/5
        [ResponseType(typeof(Horse))]
        public IHttpActionResult GetHorse(Guid id)
        {
            Horse horse = db.Horses.Find(id);
            if (horse == null)
            {
                return NotFound();
            }

            return Ok(horse);
        }

        // PUT: api/Horses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHorse(Guid id, Horse horse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != horse.HorseId)
            {
                return BadRequest();
            }

            db.Entry(horse).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorseExists(id))
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

        // POST: api/Horses
        [ResponseType(typeof(Horse))]
        public IHttpActionResult PostHorse(Horse horse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Horses.Add(horse);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HorseExists(horse.HorseId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = horse.HorseId }, horse);
        }

        // DELETE: api/Horses/5
        [ResponseType(typeof(Horse))]
        public IHttpActionResult DeleteHorse(Guid id)
        {
            Horse horse = db.Horses.Find(id);
            if (horse == null)
            {
                return NotFound();
            }

            db.Horses.Remove(horse);
            db.SaveChanges();

            return Ok(horse);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HorseExists(Guid id)
        {
            return db.Horses.Count(e => e.HorseId == id) > 0;
        }
    }
}