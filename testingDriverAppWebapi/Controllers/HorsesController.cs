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
    public class HorsesController : ApiController
    {
        private testingDriverAppWebapiContext db = new testingDriverAppWebapiContext();

        // GET: api/Horses
        public IQueryable<HorseDTO> GetHorses()
        {
            var horseDTOs = new List<HorseDTO>();


            foreach (var horse in db.Horses)
            {
                var jobHorseIds = new List<Guid>();
                foreach (var jobHorse in horse.JobHorses)
                {
                    jobHorseIds.Add(jobHorse.JobHorseId);
                }
                var horseDTO = new HorseDTO()
                {
                    HorseId = horse.HorseId,
                    
                    Name = horse.Name,

                    Brand = horse.Brand,

                    Microchip = horse.Microchip,

                    Colour = horse.Colour,

                    Sex = horse.Sex,

                    JobHorseIds = jobHorseIds
                };
                horseDTOs.Add(horseDTO);
            }
            return horseDTOs.AsQueryable();
        }

        // GET: api/Horses/5
        [ResponseType(typeof(HorseDTO))]
        public IHttpActionResult GetHorse(Guid id)
        {
            Horse horse = db.Horses.Find(id);
            if (horse == null)
            {
                return NotFound();
            }

            var jobHorseIds = new List<Guid>();
            foreach (var jobHorse in horse.JobHorses)
            {
                jobHorseIds.Add(jobHorse.JobHorseId);
            }
            var horseDTO = new HorseDTO()
            {
                HorseId = horse.HorseId,

                Name = horse.Name,

                Brand = horse.Brand,

                Microchip = horse.Microchip,

                Colour = horse.Colour,

                Sex = horse.Sex,
                JobHorseIds = jobHorseIds
            };


            return Ok(horseDTO);
        }

        // PUT: api/Horses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHorse(Guid id, Horse horse)
            //TODO implement the put method that serves DTO
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != horse.HorseId)
            {
                return BadRequest();
            }

            db.Entry(horse).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorseExists(id))
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

        // POST: api/Horses
        [ResponseType(typeof(Horse))]
        public IHttpActionResult PostHorse(Horse horse)
            //TODO implement the post method that serves DTO
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Horses.Add(horse);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HorseExists(horse.HorseId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = horse.HorseId }, horse);
        }

        // DELETE: api/Horses/5
        [ResponseType(typeof(Horse))]
        public IHttpActionResult DeleteHorse(Guid id)
        {
            Horse horse = db.Horses.Find(id);
            if (horse == null)
            {
                return NotFound();
            }

            db.Horses.Remove(horse);
            db.SaveChanges();

            return Ok(horse);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HorseExists(Guid id)
        {
            return db.Horses.Count(e => e.HorseId == id) > 0;
        }
    }
}