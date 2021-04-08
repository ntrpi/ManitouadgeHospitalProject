﻿using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Manitouage1.Models;
using System.Diagnostics;


namespace Manitouage1.Controllers
{
    public class EventDataController : ApiController

    {
        private ManitouageDbContext db = new ManitouageDbContext();

        // GET: api/EventsData/5
        public IHttpActionResult GetEvent(int id)
        {
            Event myevent = db.events.Find(id);
            EventDto eventDto = new EventDto
            {
                EventId = myevent.EventId,
                Title = myevent.Title,
                Description = myevent.Description,
                DateTime = myevent.DateTime,
                Location = myevent.Location,
                Duration = myevent.Duration,
                ContactPerson = myevent.ContactPerson
            };
            return Ok(eventDto);
        }
        // GET: api/GetEvents
        public IHttpActionResult GetEvents()
        {
            // Get the rows from the Questions table and put them in a List object.
            List<Event> Events = db.events.ToList();

            // Create a List object to hold the dtos.
            List<EventDto> EventDtos = new List<EventDto> { };

            // Convert each Question into a QuestionDto and put it in the list.
            foreach (var Event in Events)
            {
                EventDto NewEvent = new EventDto
                {
                    // Set the dto properties.
                    EventId = Event.EventId,
                    Title = Event.Title,
                    Description = Event.Description,
                    DateTime = Event.DateTime,
                    Location = Event.Location,
                    Duration = Event.Duration,
                    ContactPerson = Event.ContactPerson
                };

                // Add the dto to the list.
                EventDtos.Add(NewEvent);
            }

            // Return the Ok http action result containing the dto list.
            return Ok(EventDtos);
        }
        // PUT: api/EventsData/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateEvent(int id, [FromBody] Event myevent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != myevent.EventId)
            {
                return BadRequest();
            }

            db.Entry(myevent).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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
        // POST: api/EventsData
        [ResponseType(typeof(Event))]
        [HttpPost]
        public IHttpActionResult AddEvent(Event myevent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.events.Add(myevent);
            db.SaveChanges();

            return Ok(myevent.EventId);
        }
        // DELETE: api/EventsData/5
        [ResponseType(typeof(Event))]
        public IHttpActionResult DeleteEvent(int id)
        {
            Event myevent = db.events.Find(id);
            if (myevent == null)
            {
                return NotFound();
            }

            db.events.Remove(myevent);
            db.SaveChanges();

            return Ok(myevent);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventExists(int id)
        {
            return db.events.Count(e => e.EventId == id) > 0;
        }
    }

}





