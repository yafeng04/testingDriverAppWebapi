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
    public class EntitiesController : ApiController
    {
        private testingDriverAppWebapiContext db = new testingDriverAppWebapiContext();

        // GET: api/Entities
        public IQueryable<EntityDTO> GetEntities()
        {
            var entityDTOs = new List<EntityDTO>();


            foreach (var entity in db.Entities)
            {
                var jobEntityIds = new List<Guid>();
                foreach (var jobEntity in entity.JobEntities)
                {
                    jobEntityIds.Add(jobEntity.JobEntityId);
                }
                var entityDTO = new EntityDTO()
                {
                    EntityId = entity.EntityId,
                    Email = entity.Email,
                    Name = entity.Name,
                    Mobile = entity.Mobile,
                    JobEntityIds = jobEntityIds
                };
                entityDTOs.Add(entityDTO);
            }
            return entityDTOs.AsQueryable();

        }

        // GET: api/Entities/5
        [ResponseType(typeof(EntityDTO))]
        public IHttpActionResult GetEntity(Guid id)
        {
            Entity entity = db.Entities.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            var jobEntityIds = new List<Guid>();
            foreach (var jobEntity in entity.JobEntities)
            {
                jobEntityIds.Add(jobEntity.JobEntityId);
            }
            var entityDTO = new EntityDTO()
            {
                EntityId = entity.EntityId,
                Email = entity.Email,
                Name = entity.Name,
                Mobile = entity.Mobile,
                JobEntityIds = jobEntityIds
            };


            return Ok(entityDTO);
        }

        // PUT: api/Entities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEntity(Guid id, Entity entity)
            
            //TODO implement the put method that serves DTO
            
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Entity localEntity = db.Entities.Find(id);

            if (id != entity.EntityId)
            {
                return BadRequest();
            }

            db.Entry(entity).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
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

        // POST: api/Entities
        [ResponseType(typeof(Entity))]
        public IHttpActionResult PostEntity(Entity entity)

            //TODO implement the post method that serves DTO

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entities.Add(entity);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EntityExists(entity.EntityId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = entity.EntityId }, entity);
        }

        // DELETE: api/Entities/5
        [ResponseType(typeof(Entity))]
        public IHttpActionResult DeleteEntity(Guid id)
        {
            Entity entity = db.Entities.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            db.Entities.Remove(entity);
            db.SaveChanges();

            return Ok(entity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EntityExists(Guid id)
        {
            return db.Entities.Count(e => e.EntityId == id) > 0;
        }
    }
}