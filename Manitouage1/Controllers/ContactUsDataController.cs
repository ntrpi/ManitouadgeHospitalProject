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
    public class ContactUsDataController : ApiController
    {
        private ManitouageDbContext db = new ManitouageDbContext();

        // GET: api/ContactUsData/5
        public IHttpActionResult GetContactUs(int id)
        {
            ContactUs ContactUs = db.contactus.Find(id);
            ContactUsDto ContactUsDto = new ContactUsDto
            {
                ContactUsId = ContactUs.ContactUsId,
                FirstName = ContactUs.FirstName,
                LastName = ContactUs.LastName,
                Email = ContactUs.Email,
                Message = ContactUs.Message,
                Reply = ContactUs.Reply,
                Status = ContactUs.Status
            };
            return Ok(ContactUsDto);
        }
        // GET: api/GetContactUs
        public IHttpActionResult GetContactUss()
        {

            List<ContactUs> ContactUs = db.contactus.ToList();

            // list object holds dtos
            List<ContactUsDto> ContactUsDtos = new List<ContactUsDto> { };

            // Convert ContactUs into a ContactUsDto
            foreach (var XContactUs in ContactUs)
            {
                ContactUsDto NewContactUs = new ContactUsDto
                {
                    // Set the dto properties.
                    ContactUsId = XContactUs.ContactUsId,
                    FirstName = XContactUs.FirstName,
                    LastName = XContactUs.LastName,
                    Email = XContactUs.Email,
                    Message = XContactUs.Message,
                    Reply = XContactUs.Reply,
                    Status = XContactUs.Status
                };

                // Add the dto to the list.
                ContactUsDtos.Add(NewContactUs);
            }


            return Ok(ContactUsDtos);
        }
        // PUT: api/ContactUsData/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateContactUs(int id, [FromBody] ContactUs ContactUs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ContactUs.ContactUsId)
            {
                return BadRequest();
            }

            db.Entry(ContactUs).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactUsExists(id))
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
        // POST: api/ContactUsData
        [ResponseType(typeof(ContactUs))]
        [HttpPost]
        public IHttpActionResult AddContactUs(ContactUs ContactUs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.contactus.Add(ContactUs);
            db.SaveChanges();

            return Ok(ContactUs.ContactUsId);
        }
        // DELETE: api/ContactUsData/5
        [HttpPost]
        [ResponseType(typeof(ContactUs))]
        public IHttpActionResult DeleteContactUs(int id)
        {
            ContactUs ContactUs = db.contactus.Find(id);
            if (ContactUs == null)
            {
                return NotFound();
            }

            db.contactus.Remove(ContactUs);
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

        private bool ContactUsExists(int id)
        {
            return db.contactus.Count(e => e.ContactUsId == id) > 0;
        }
    }

}
