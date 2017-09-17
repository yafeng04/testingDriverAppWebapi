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
        private bool initialised = false;

        private void Initializer()
        {
            if (initialised == false)
            {
                Guid horseId = Guid.Parse("11111111-1111-1111-1111-111111111121");
                HorseDTO horseDTO = db.HorseDTOes.Find(horseId);
                horseDTO.JobHorseIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111161"));
                horseDTO.JobHorseIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111166"));
                horseDTO.JobHorseIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111169"));

                 horseId = Guid.Parse("11111111-1111-1111-1111-111111111122");
                 horseDTO = db.HorseDTOes.Find(horseId);
                horseDTO.JobHorseIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111162"));
                horseDTO.JobHorseIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111164"));

                horseId = Guid.Parse("11111111-1111-1111-1111-111111111123");
                horseDTO = db.HorseDTOes.Find(horseId);
                horseDTO.JobHorseIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111163"));

                horseId = Guid.Parse("11111111-1111-1111-1111-111111111124");
                horseDTO = db.HorseDTOes.Find(horseId);
                horseDTO.JobHorseIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111165"));

                horseId = Guid.Parse("11111111-1111-1111-1111-111111111125");
                horseDTO = db.HorseDTOes.Find(horseId);
                horseDTO.JobHorseIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111167"));
                horseDTO.JobHorseIds.Add(Guid.Parse("11111111-1111-1111-1111-111111111168"));



                initialised = true;
            }

        }


        // GET: api/HorseDTOes
        public IQueryable<HorseDTO> GetHorseDTOes()
        {

            Initializer();
            return db.HorseDTOes;
        }

        [HttpGet]
        [Route("horses/getList")]
        public IHttpActionResult GetHorsesByHorseIds(string opt = null, string ids = null)
        {

            Initializer();
            // Extract options.
            var listOnly = opt?.Contains("list") ?? false;

            // Extract requested IDs.

            try
            {
                var results = db.HorseDTOes;

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