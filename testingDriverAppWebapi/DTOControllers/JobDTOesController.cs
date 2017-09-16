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
using testingDriverAppWebapi.DTO;
using testingDriverAppWebapi.Models;

namespace testingDriverAppWebapi.Controllers
{
    public class JobDTOesController : ApiController
    {
        private testingDriverAppWebapiDTOContext db = new testingDriverAppWebapiDTOContext();

        // GET: api/JobDTOes
        public IQueryable<JobDTO> GetJobDTOes()
        {
            return db.JobDTOes;
        }

        // GET: api/JobDTOes/5
        [ResponseType(typeof(JobDTO))]
        public IHttpActionResult GetJobDTO(Guid id)
        {
            JobDTO jobDTO = db.JobDTOes.Find(id);
            if (jobDTO == null)
            {
                return NotFound();
            }

            return Ok(jobDTO);
        }

        // PUT: api/JobDTOes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJobDTO(Guid id, JobDTO jobDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobDTO.JobId)
            {
                return BadRequest();
            }

            db.Entry(jobDTO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobDTOExists(id))
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

        // POST: api/JobDTOes
        [ResponseType(typeof(JobDTO))]
        public IHttpActionResult PostJobDTO(JobDTO jobDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobDTOes.Add(jobDTO);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (JobDTOExists(jobDTO.JobId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = jobDTO.JobId }, jobDTO);
        }

        // DELETE: api/JobDTOes/5
        [ResponseType(typeof(JobDTO))]
        public IHttpActionResult DeleteJobDTO(Guid id)
        {
            JobDTO jobDTO = db.JobDTOes.Find(id);
            if (jobDTO == null)
            {
                return NotFound();
            }

            db.JobDTOes.Remove(jobDTO);
            db.SaveChanges();

            return Ok(jobDTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobDTOExists(Guid id)
        {
            return db.JobDTOes.Count(e => e.JobId == id) > 0;
        }
    }
}