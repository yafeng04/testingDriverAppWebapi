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
    public class HorseDTOesController : ApiController
    {
        private testingDriverAppWebapiDTOContext db = new testingDriverAppWebapiDTOContext();

        // GET: api/HorseDTOes
        public IQueryable<HorseDTO> GetHorseDTOes()
        {
            return db.HorseDTOes;
        }

        // GET: api/HorseDTOes/5
        [ResponseType(typeof(HorseDTO))]
        public IHttpActionResult GetHorseDTO(Guid id)
        {
            HorseDTO horseDTO = db.HorseDTOes.Find(id);
            if (horseDTO == null)
            {
                return NotFound();
            }

            return Ok(horseDTO);
        }

        // PUT: api/HorseDTOes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHorseDTO(Guid id, HorseDTO horseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != horseDTO.HorseId)
            {
                return BadRequest();
            }

            db.Entry(horseDTO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorseDTOExists(id))
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

        // POST: api/HorseDTOes
        [ResponseType(typeof(HorseDTO))]
        public IHttpActionResult PostHorseDTO(HorseDTO horseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HorseDTOes.Add(horseDTO);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HorseDTOExists(horseDTO.HorseId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = horseDTO.HorseId }, horseDTO);
        }

        // DELETE: api/HorseDTOes/5
        [ResponseType(typeof(HorseDTO))]
        public IHttpActionResult DeleteHorseDTO(Guid id)
        {
            HorseDTO horseDTO = db.HorseDTOes.Find(id);
            if (horseDTO == null)
            {
                return NotFound();
            }

            db.HorseDTOes.Remove(horseDTO);
            db.SaveChanges();

            return Ok(horseDTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HorseDTOExists(Guid id)
        {
            return db.HorseDTOes.Count(e => e.HorseId == id) > 0;
        }
    }
}