using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using testingDriverAppWebapi.DTO;
using testingDriverAppWebapi.Models;

namespace testingDriverAppWebapi.Controllers
{
    public class JobEntitiesController : ApiController
    {
        private testingDriverAppWebapiContext db = new testingDriverAppWebapiContext();

        // GET: api/JobEntities
        public IQueryable<JobEntityDTO> GetJobEntities()
        {

            var jobEntityDTOs = new List<JobEntityDTO>();

            foreach (var jobEntity in db.JobEntities)
            {

                var jobEntityDTO = new JobEntityDTO()
                {
                    JobEntityId = jobEntity.JobEntityId,
                    EntityId = jobEntity.EntityId,
                    JobId = jobEntity.JobId,
                    MethodToNotify = jobEntity.MethodToNotify,
                    NotifyTime = jobEntity.NotifyTime

                };
                jobEntityDTOs.Add(jobEntityDTO);
            }
            return jobEntityDTOs.AsQueryable();

        }

        // GET: api/JobEntities/5
        [ResponseType(typeof(JobEntityDTO))]
        public IHttpActionResult GetJobEntity(Guid id)
        {
            JobEntity jobEntity = db.JobEntities.Find(id);
            if (jobEntity == null)
            {
                return NotFound();
            }
            var jobEntityDTO = new JobEntityDTO()
            {
                JobEntityId = jobEntity.JobEntityId,
                EntityId = jobEntity.EntityId,
                JobId = jobEntity.JobId,
                MethodToNotify = jobEntity.MethodToNotify,
                NotifyTime = jobEntity.NotifyTime

            };

            return Ok(jobEntityDTO);
        }

        // PUT: api/JobEntities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJobEntity(Guid id, JobEntityDTO jobEntityDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobEntityDTO.JobEntityId)
            {
                return BadRequest();
            }

            JobEntity jobEntity = db.JobEntities.Find(id);
            if (jobEntity == null)
            {
                return NotFound();
            }

            jobEntity.EntityId = jobEntityDTO.EntityId;
            jobEntity.JobId = jobEntityDTO.JobId;
            jobEntity.MethodToNotify = jobEntityDTO.MethodToNotify;
            jobEntity.NotifyTime = jobEntityDTO.NotifyTime;

            var jobInDb = db.Jobs.Find(jobEntity.JobId);
            if (jobInDb == null)
            {
                return NotFound();
            }

            jobEntity.Job = jobInDb;

            var entityInDb = db.Entities.Find(jobEntity.EntityId);
            if (entityInDb == null)
            {
                return NotFound();
            }

            jobEntity.Entity = entityInDb;

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
        [ResponseType(typeof(JobEntityDTO))]
        public IHttpActionResult PostJobEntity(JobEntityDTO jobEntityDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobEntity = new JobEntity
            {
                EntityId = jobEntityDTO.EntityId,
                JobId = jobEntityDTO.JobId,
                MethodToNotify = jobEntityDTO.MethodToNotify,
                NotifyTime = jobEntityDTO.NotifyTime
            };




            var jobInDb = db.Jobs.Find(jobEntity.JobId);
            if (jobInDb == null)
            {
                return NotFound();
            }

            jobEntity.Job = jobInDb;

            var entityInDb = db.Entities.Find(jobEntity.EntityId);
            if (entityInDb == null)
            {
                return NotFound();
            }

            jobEntity.Entity = entityInDb;


            //db.JobEntities.Add();

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (JobEntityExists(jobEntityDTO.JobEntityId))
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