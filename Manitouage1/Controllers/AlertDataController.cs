using System;
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
    public class AlertDataController : ApiController
    {
        private ManitouageDbContext db = new ManitouageDbContext();

        // GET: api/AlertData/5
        public IHttpActionResult GetAlert(int id)
        {
            //When I run my view, It is telling me myalert is null. Unsure why.
            Alert myalert = db.alerts.Find(id);
            AlertDto alertDto = new AlertDto
            {
                alertId = myalert.alertId,
                title = myalert.title,
                dateTime = myalert.dateTime,
                description = myalert.description,
                EventId = myalert.eventId == null ? 0 : (int) myalert.eventId,
                jobPostingId = myalert.jobPostingId == null ? 0 : (int) myalert.jobPostingId
            };
            return Ok(alertDto);
        }
        public IHttpActionResult GetAlerts()
        {
            // Get the rows from the Alerts table and put them in a List object.
            List<Alert> myalert = db.alerts.ToList();

            // Create a List object to hold the dtos.
            List<AlertDto> AlertDtos = new List<AlertDto> { };

            // Convert each Alert into a AlertDto and put it in the list.
            foreach (var Alert in myalert)
            {
                AlertDto NewAlert = new AlertDto
                {
                    // Set the dto properties.
                    alertId = Alert.alertId,
                    title = Alert.title,
                    dateTime = Alert.dateTime,
                    description = Alert.description,
                };

                // Add the dto to the list.
                AlertDtos.Add(NewAlert);
            }

            // Return the Ok http action result containing the dto list.
            return Ok(AlertDtos);
        }

        // POST: api/AlertData
        [ResponseType(typeof(Alert))]
        [HttpPost]
        public IHttpActionResult AddAlert(Alert myalert)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.alerts.Add(myalert);
            db.SaveChanges();

            return Ok(myalert.alertId);
        }
        // PUT: api/AlertData/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateAlert(int id, [FromBody] Alert myalert)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != myalert.alertId)
            {
                return BadRequest();
            }

            db.Entry(myalert).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlertExists(id))
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
        // DELETE: api/AlertData/5
        [HttpPost]
        [ResponseType(typeof(Event))]
        public IHttpActionResult DeleteAlert(int id)
        {
            Alert myalert = db.alerts.Find(id);
            if (myalert == null)
            {
                return NotFound();
            }

            db.alerts.Remove(myalert);
            db.SaveChanges();

            return Ok();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool AlertExists(int id)
        {
            return db.alerts.Count(a => a.alertId == id) > 0;
        }
    }
}
