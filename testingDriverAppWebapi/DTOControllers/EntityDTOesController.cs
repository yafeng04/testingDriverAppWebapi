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
    public class EntityDTOesController : ApiController
    {
        
        private testingDriverAppWebapiDTOContext db = new testingDriverAppWebapiDTOContext();
        private bool initialised = false;

        private void Initializer()
        {
            if (initialised == false)
            {
                Guid entityId = Guid.Parse("11111111-1111-1111-1111-111111111111");
                EntityDTO entityDTO = db.EntityDTOes.Find(entityId);
                entityDTO.JobEntityIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111151"));

                entityId = Guid.Parse("11111111-1111-1111-1111-111111111112");
                entityDTO = db.EntityDTOes.Find(entityId);
                entityDTO.JobEntityIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111152"));
                entityDTO.JobEntityIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111156"));

                entityId = Guid.Parse("11111111-1111-1111-1111-111111111113");
                entityDTO = db.EntityDTOes.Find(entityId);
                entityDTO.JobEntityIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111153"));

                entityId = Guid.Parse("11111111-1111-1111-1111-111111111114");
                entityDTO = db.EntityDTOes.Find(entityId);
                entityDTO.JobEntityIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111154"));
                entityDTO.JobEntityIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111155"));
                initialised = true;
            }

        }


        // GET: api/EntityDTOes
        public IQueryable<EntityDTO> GetEntityDTOes()
        {
            Initializer();
            return db.EntityDTOes;
        }

        [HttpGet]
        [Route("entities/getList")]
        public IHttpActionResult GetHorsesByHorseIds(string opt = null, string ids = null)
        {

            Initializer();
            // Extract options.
            var listOnly = opt?.Contains("list") ?? false;

            // Extract requested IDs.

            try
            {
                var results = db.EntityDTOes;

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

        // GET: api/EntityDTOes/5
        [ResponseType(typeof(EntityDTO))]
        public IHttpActionResult GetEntityDTO(Guid id)
        {

            Initializer();
            EntityDTO entityDTO = db.EntityDTOes.Find(id);
            if (entityDTO == null)
            {
                return NotFound();
            }

            return Ok(entityDTO);
        }

        // PUT: api/EntityDTOes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEntityDTO(Guid id, EntityDTO entityDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entityDTO.EntityId)
            {
                return BadRequest();
            }

            db.Entry(entityDTO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityDTOExists(id))
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

        // POST: api/EntityDTOes
        [ResponseType(typeof(EntityDTO))]
        public IHttpActionResult PostEntityDTO(EntityDTO entityDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EntityDTOes.Add(entityDTO);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EntityDTOExists(entityDTO.EntityId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = entityDTO.EntityId }, entityDTO);
        }

        // DELETE: api/EntityDTOes/5
        [ResponseType(typeof(EntityDTO))]
        public IHttpActionResult DeleteEntityDTO(Guid id)
        {
            EntityDTO entityDTO = db.EntityDTOes.Find(id);
            if (entityDTO == null)
            {
                return NotFound();
            }

            db.EntityDTOes.Remove(entityDTO);
            db.SaveChanges();

            return Ok(entityDTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EntityDTOExists(Guid id)
        {
            return db.EntityDTOes.Count(e => e.EntityId == id) > 0;
        }
    }
}