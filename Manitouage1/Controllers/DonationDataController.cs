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
using Manitouage1.Models.ViewModels;
using System.Diagnostics;

namespace Manitouage1.Controllers
{
    public class DonationDataController : ApiController
    {
        private ManitouageDbContext db = new ManitouageDbContext();


        /// <summary>
        /// Gets a list of donations in the database with the status code (200 ok)
        /// this is in direct reference to playersdatacontroller(varsity_w_auth)
        /// </summary>
        /// <returns>
        /// returns a list of all the donations made
        /// </returns>
        /// <example>
        // GET: api/DonationData/getdonations
        /// </example>
        [HttpGet]
        //changed to EventDto
        [ResponseType(typeof(IEnumerable<EventDto>))]
        public IHttpActionResult GetDonations()
        {
            //FOR MY UNDERSTANDING: go to donations dto and get the list 
            List<Donation> Donations = db.donations.ToList();
            //FOR MY UNDERSTANDING: show the list in the donation database 
            List<DonationDto> DonationDtos = new List<DonationDto> { };

            //information to be displayed 
            foreach (var Donation in Donations)
            {
                 Event Event = db.events
                .Where(e => e.Donations.Any(d => d.EventId == Donation.EventId))
                .FirstOrDefault();
                DonationDto NewDonation = new DonationDto
                {
                    donationId = Donation.donationId,
                    firstName = Donation.firstName,
                    lastName = Donation.lastName,
                    email = Donation.email,
                    phoneNumber = Donation.phoneNumber,
                    amount = Donation.amount,
                    //add event id 
                    EventId = Donation.EventId,


                };
                DonationDtos.Add(NewDonation);
            }

            //if status code 200 list donations
            return Ok(DonationDtos);
        }



        /// <summary>
        /// CHRISTINE IT's NOT WORKING!!!!!!!!!!!!!!!
        /// Gets a list of donations in the database with the status code (200 ok)
        /// this is in direct reference to playersdatacontroller(varsity_w_auth)
        /// </summary>
        /// <returns>
        /// returns a list of all the donations and the events linked to them
        /// </returns>
        /// <example>
        // GET: api/DonationData/GetAllDonations
        /// </example>
        //changed to EventDto
        [ResponseType(typeof(IEnumerable<ListDonation>))]
        public IHttpActionResult GetAllDonations()
        {
            //List of donations
            List<Donation> Donations = db.donations.ToList();
            //using view model :: show donation 
            List<ListDonation> DonationDtos = new List<ListDonation> { };

            //information to be displayed 
            foreach (var Donation in Donations)
            {
                ListDonation donation = new ListDonation();

                //getting events from the events table and linking it to the donations table 
                Event Event = db.events
               .Where(e => e.Donations.Any(d => d.EventId == Donation.EventId))
               .FirstOrDefault();

                //now calling the events dto to fetch the information to list
                EventDto NewEvent = new EventDto
                {
                    EventId = Event.EventId,
                    Title = Event.Title

                };

                //now calling the donations dto to fetch the data
                DonationDto NewDonation = new DonationDto
                {
                    donationId = Donation.donationId,
                    firstName = Donation.firstName,
                    lastName = Donation.lastName,
                    email = Donation.email,
                    phoneNumber = Donation.phoneNumber,
                    amount = Donation.amount,
                    //add event id 
                    EventId = Donation.EventId,


                };

                donation.Event = NewEvent;
                DonationDtos.Add(donation);

            }

            return Ok(DonationDtos);

        }





        /// <summary> 
        /// Finds a specfic donation by id with an OK status code. if donation is not found displays status code 404
        /// </summary>
        /// <param name="id">The donation id</param>
        /// <returns>information about the donation: donation made by, amount and if it was made to an event</returns>
        // <example>
        // GET: api/DonationData/FindDonation/5
        // </example>
        [HttpGet]
        [ResponseType(typeof(DonationDto))]
        public IHttpActionResult FindDonation(int id)
        {
            Donation Donation = db.donations.Find(id);

            //not found 404 status code.
            if (Donation == null)
            {
                return NotFound();
            }

            DonationDto DonationDto = new DonationDto
            {
                donationId = Donation.donationId,
                firstName = Donation.firstName,
                lastName = Donation.lastName,
                email = Donation.email,
                phoneNumber = Donation.phoneNumber,
                amount = Donation.amount,
                //add event id
                EventId = Donation.EventId,


            };


            //if donation is in the database 200 status code OK response
            return Ok(DonationDto);
        }

        ///<summary>
        ///finding the event id for a donations
        ///</summary>
        ///<prama name="id">Donation id</prama>
        ///<result>
        ///This will display the event associated with the donation 
        ///</result>
        ///REFERENCE TO VARSITY PROJECT
        //GET: api/DonationData/FindEventForDonation
        [HttpGet]
        [ResponseType(typeof(IEnumerable<EventDto>))]
        public IHttpActionResult FindEventForDonation(int id)//this is donation id 
        {
            Event Event = db.events
                //in the events table donation id equals the donations donation id 
                .Where(e => e.Donations.Any(d => d.donationId == id))
                .FirstOrDefault();

            if (Event == null)
            {
                return NotFound();
            }

            EventDto Events = new EventDto
            {
                EventId = Event.EventId,
                Title = Event.Title

            };

            return Ok(Events);

        }


        /// <summary>
        /// Adds a donation to the database.
        /// </summary>
        /// <param name="donation">adds a donation object. POST request through form</param>
        /// <returns>status code 200 if successful. 400 if unsuccessful</returns>
        /// <example>
        /// POST: api/DonationData/AddDonation
        /// FORM DATA: Donation JSON Object
        /// </example>
        [ResponseType(typeof(Donation))]
        [HttpPost]
        public IHttpActionResult AddDonation([FromBody] Donation donation)
        {
            //validate according to data annotation discribed in my doination model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400 status code if database is not updated
            }

            db.donations.Add(donation);
            db.SaveChanges();

            return Ok(donation.donationId); 
        }


        /// <summary>
        /// Updates a donation in the database
        /// </summary>
        /// <param name="id">donation id</param>
        /// <param name="firstName">Received as POST data.</param>
        /// <param name="lastName">Received as POST data.</param>
        /// <param name="email">Received as POST data.</param>
        /// <returns>redirect to list page with the updated donation information</returns>
        /// <example>
        /// POST: api/DonationData/UpdateDonation/5
        /// FORM DATA: Donatrion JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        //[Authorize(Roles = "Admin")] //only admin can update donation info
        public IHttpActionResult UpdateDonation(int id, [FromBody] Donation donation)
        {
            //checking for model state and donation id ... if not valid throw error
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != donation.donationId)
            {
                return BadRequest();
            }
            //updating the database entry
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


        // DELETE: api/DonationsData/DeleteDonation/5
        [HttpPost]
        public IHttpActionResult DeleteDonation(int id)
        {
            Donation donation = db.donations.Find(id);
            if (donation == null)
            {
                return NotFound();
            }

            db.donations.Remove(donation);
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

        private bool DonationExists(int id)
        {
            return db.donations.Count(d => d.donationId == id) > 0;
        }
    }
}


