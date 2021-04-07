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
using Manitouage1.Models;

namespace Manitouage1.Controllers
{
    public class DonationDataController : ApiController
    {
        private ManitouageDbContext db = new ManitouageDbContext();

        // GET: api/DonationData
        public IQueryable<Donation> Getdonations()
        {
            return db.donations;
        }

        // GET: api/DonationData/5
        [ResponseType(typeof(Donation))]
        public IHttpActionResult GetDonation(int id)
        {
            Donation donation = db.donations.Find(id);
            if (donation == null)
            {
                return NotFound();
            }

            return Ok(donation);
        }

        // PUT: api/DonationData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDonation(int id, Donation donation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != donation.donationId)
            {
                return BadRequest();
            }

            db.Entry(donation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonationExists(id))
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

        // POST: api/DonationData
        [ResponseType(typeof(Donation))]
        public IHttpActionResult PostDonation(Donation donation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.donations.Add(donation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = donation.donationId }, donation);
        }

        // DELETE: api/DonationData/5
        [ResponseType(typeof(Donation))]
        public IHttpActionResult DeleteDonation(int id)
        {
            Donation donation = db.donations.Find(id);
            if (donation == null)
            {
                return NotFound();
            }

            db.donations.Remove(donation);
            db.SaveChanges();

            return Ok(donation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DonationExists(int id)
        {
            return db.donations.Count(e => e.donationId == id) > 0;
        }
    }
}