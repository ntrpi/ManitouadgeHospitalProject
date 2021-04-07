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
    public class EventDataController : ApiController

    {
        private ManitouageDbContext db = new ManitouageDbContext();
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
    }
}
