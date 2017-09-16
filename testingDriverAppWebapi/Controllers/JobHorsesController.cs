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
    public class JobHorsesController : ApiController
    {
        private testingDriverAppWebapiContext db = new testingDriverAppWebapiContext();

        // GET: api/JobHorses
        public IQueryable<JobHorse> GetJobHorses()
        {
            return db.JobHorses;
        }

        // GET: api/JobHorses/5
        [ResponseType(typeof(JobHorse))]
        public IHttpActionResult GetJobHorse(Guid id)
        {
            JobHorse jobHorse = db.JobHorses.Find(id);
            if (jobHorse == null)
            {
                return NotFound();
            }

            return Ok(jobHorse);
        }

        // PUT: api/JobHorses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJobHorse(Guid id, JobHorse jobHorse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobHorse.JobHorseId)
            {
                return BadRequest();
            }

            db.Entry(jobHorse).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobHorseExists(id))
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

        // POST: api/JobHorses
        [ResponseType(typeof(JobHorse))]
        public IHttpActionResult PostJobHorse(JobHorse jobHorse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobHorses.Add(jobHorse);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (JobHorseExists(jobHorse.JobHorseId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = jobHorse.JobHorseId }, jobHorse);
        }

        // DELETE: api/JobHorses/5
        [ResponseType(typeof(JobHorse))]
        public IHttpActionResult DeleteJobHorse(Guid id)
        {
            JobHorse jobHorse = db.JobHorses.Find(id);
            if (jobHorse == null)
            {
                return NotFound();
            }

            db.JobHorses.Remove(jobHorse);
            db.SaveChanges();

            return Ok(jobHorse);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobHorseExists(Guid id)
        {
            return db.JobHorses.Count(e => e.JobHorseId == id) > 0;
        }
    }
}