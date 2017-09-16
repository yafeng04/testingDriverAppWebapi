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

        // GET: api/EntityDTOes
        public IQueryable<EntityDTO> GetEntityDTOes()
        {
            return db.EntityDTOes;
        }

        // GET: api/EntityDTOes/5
        [ResponseType(typeof(EntityDTO))]
        public IHttpActionResult GetEntityDTO(Guid id)
        {
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