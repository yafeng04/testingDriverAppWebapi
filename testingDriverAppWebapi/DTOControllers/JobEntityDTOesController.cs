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
    public class JobEntityDTOesController : ApiController
    {
        private testingDriverAppWebapiDTOContext db = new testingDriverAppWebapiDTOContext();

        // GET: api/JobEntityDTOes
        public IQueryable<JobEntityDTO> GetJobEntityDTOes()
        {
            return db.JobEntityDTOes;
        }

        // GET: api/JobEntityDTOes/5
        [ResponseType(typeof(JobEntityDTO))]
        public IHttpActionResult GetJobEntityDTO(Guid id)
        {
            JobEntityDTO jobEntityDTO = db.JobEntityDTOes.Find(id);
            if (jobEntityDTO == null)
            {
                return NotFound();
            }

            return Ok(jobEntityDTO);
        }

        // PUT: api/JobEntityDTOes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJobEntityDTO(Guid id, JobEntityDTO jobEntityDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobEntityDTO.JobEntityId)
            {
                return BadRequest();
            }

            db.Entry(jobEntityDTO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobEntityDTOExists(id))
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

        // POST: api/JobEntityDTOes
        [ResponseType(typeof(JobEntityDTO))]
        public IHttpActionResult PostJobEntityDTO(JobEntityDTO jobEntityDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobEntityDTOes.Add(jobEntityDTO);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (JobEntityDTOExists(jobEntityDTO.JobEntityId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = jobEntityDTO.JobEntityId }, jobEntityDTO);
        }

        // DELETE: api/JobEntityDTOes/5
        [ResponseType(typeof(JobEntityDTO))]
        public IHttpActionResult DeleteJobEntityDTO(Guid id)
        {
            JobEntityDTO jobEntityDTO = db.JobEntityDTOes.Find(id);
            if (jobEntityDTO == null)
            {
                return NotFound();
            }

            db.JobEntityDTOes.Remove(jobEntityDTO);
            db.SaveChanges();

            return Ok(jobEntityDTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobEntityDTOExists(Guid id)
        {
            return db.JobEntityDTOes.Count(e => e.JobEntityId == id) > 0;
        }
    }
}