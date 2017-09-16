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
    public class JobEntitiesController : ApiController
    {
        private testingDriverAppWebapiContext db = new testingDriverAppWebapiContext();

        // GET: api/JobEntities
        public IQueryable<JobEntity> GetJobEntities()
        {
            return db.JobEntities;
        }

        // GET: api/JobEntities/5
        [ResponseType(typeof(JobEntity))]
        public IHttpActionResult GetJobEntity(Guid id)
        {
            JobEntity jobEntity = db.JobEntities.Find(id);
            if (jobEntity == null)
            {
                return NotFound();
            }

            return Ok(jobEntity);
        }

        // PUT: api/JobEntities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJobEntity(Guid id, JobEntity jobEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobEntity.JobEntityId)
            {
                return BadRequest();
            }

            db.Entry(jobEntity).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobEntityExists(id))
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

        // POST: api/JobEntities
        [ResponseType(typeof(JobEntity))]
        public IHttpActionResult PostJobEntity(JobEntity jobEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobEntities.Add(jobEntity);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (JobEntityExists(jobEntity.JobEntityId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = jobEntity.JobEntityId }, jobEntity);
        }

        // DELETE: api/JobEntities/5
        [ResponseType(typeof(JobEntity))]
        public IHttpActionResult DeleteJobEntity(Guid id)
        {
            JobEntity jobEntity = db.JobEntities.Find(id);
            if (jobEntity == null)
            {
                return NotFound();
            }

            db.JobEntities.Remove(jobEntity);
            db.SaveChanges();

            return Ok(jobEntity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobEntityExists(Guid id)
        {
            return db.JobEntities.Count(e => e.JobEntityId == id) > 0;
        }
    }
}