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
    public class JobHorseDTOesController : ApiController
    {
        private testingDriverAppWebapiDTOContext db = new testingDriverAppWebapiDTOContext();

        // GET: api/JobHorseDTOes
        public IQueryable<JobHorseDTO> GetJobHorseDTOes()
        {
            return db.JobHorseDTOes;
        }

        [HttpGet]
        [Route("jobHorses/getList")]
        public IHttpActionResult GetHorsesByHorseIds(string opt = null, string ids = null)
        {


            // Extract options.
            var listOnly = opt?.Contains("list") ?? false;

            // Extract requested IDs.

            try
            {
                var results = db.JobHorseDTOes;

                var response = new Dictionary<string, object>()
                {
                    { "Results", results }
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/JobHorseDTOes/5
        [ResponseType(typeof(JobHorseDTO))]
        public IHttpActionResult GetJobHorseDTO(Guid id)
        {
            JobHorseDTO jobHorseDTO = db.JobHorseDTOes.Find(id);
            if (jobHorseDTO == null)
            {
                return NotFound();
            }

            return Ok(jobHorseDTO);
        }

        // PUT: api/JobHorseDTOes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJobHorseDTO(Guid id, JobHorseDTO jobHorseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobHorseDTO.JobHorseId)
            {
                return BadRequest();
            }

            db.Entry(jobHorseDTO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobHorseDTOExists(id))
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

        // POST: api/JobHorseDTOes
        [ResponseType(typeof(JobHorseDTO))]
        public IHttpActionResult PostJobHorseDTO(JobHorseDTO jobHorseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobHorseDTOes.Add(jobHorseDTO);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (JobHorseDTOExists(jobHorseDTO.JobHorseId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = jobHorseDTO.JobHorseId }, jobHorseDTO);
        }

        // DELETE: api/JobHorseDTOes/5
        [ResponseType(typeof(JobHorseDTO))]
        public IHttpActionResult DeleteJobHorseDTO(Guid id)
        {
            JobHorseDTO jobHorseDTO = db.JobHorseDTOes.Find(id);
            if (jobHorseDTO == null)
            {
                return NotFound();
            }

            db.JobHorseDTOes.Remove(jobHorseDTO);
            db.SaveChanges();

            return Ok(jobHorseDTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobHorseDTOExists(Guid id)
        {
            return db.JobHorseDTOes.Count(e => e.JobHorseId == id) > 0;
        }
    }
}