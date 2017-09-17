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

        private bool initialised = false;

        private void Initializer()
        {
            if (initialised == false)
            {
                Guid jobId = Guid.Parse("11111111-1111-1111-1111-111111111131");
                JobDTO jobDTO = db.JobDTOes.Find(jobId);
                jobDTO.JobEntityIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111151"));
                jobDTO.JobEntityIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111152"));
                jobDTO.JobHorseIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111161"));
                jobDTO.JobHorseIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111162"));
                jobDTO.JobHorseIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111163"));

                jobId = Guid.Parse("11111111-1111-1111-1111-111111111132");
                 jobDTO = db.JobDTOes.Find(jobId);
                jobDTO.JobEntityIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111153"));
                jobDTO.JobHorseIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111164"));
                jobDTO.JobHorseIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111165"));

                jobId = Guid.Parse("11111111-1111-1111-1111-111111111133");
                jobDTO = db.JobDTOes.Find(jobId);
                jobDTO.JobEntityIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111154"));
                jobDTO.JobHorseIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111166"));
                jobDTO.JobHorseIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111167"));

                jobId = Guid.Parse("11111111-1111-1111-1111-111111111134");
                 jobDTO = db.JobDTOes.Find(jobId);
                jobDTO.JobEntityIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111155"));
                jobDTO.JobEntityIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111156"));
                jobDTO.JobHorseIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111168"));
                jobDTO.JobHorseIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111169"));




                initialised = true;
            }

        }

        // GET: api/JobDTOes
        public IQueryable<JobDTO> GetJobDTOes()
        {
            Initializer();
            return db.JobDTOes;
        }

        [HttpGet]
        [Route("jobs/getList")]
        public IHttpActionResult GetHorsesByHorseIds(string opt = null, string ids = null)
        {

            Initializer();
            // Extract options.
            var listOnly = opt?.Contains("list") ?? false;

            // Extract requested IDs.

            try
            {
                var results = db.JobDTOes;

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

        // GET: api/JobDTOes/5
        [ResponseType(typeof(JobDTO))]
        public IHttpActionResult GetJobDTO(Guid id)
        {
            Initializer();
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