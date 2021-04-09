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
    public class VolunteerDataController : ApiController
    {
        // db context
        private ManitouageDbContext db = new ManitouageDbContext();



        [ResponseType(typeof(IEnumerable<VolunteerDto>))]
        [System.Web.Http.Route("api/volunteerdata/getvolunteers")]
        public IHttpActionResult GetVolunteers()
        {
            List<Volunteer> Volunteers = db.volunteers.ToList();
            List<VolunteerDto> VolunteerDtos = new List<VolunteerDto> { };

            foreach (var Volunteer in Volunteers)
            {
                VolunteerDto newVolunteer = new VolunteerDto
                {
                    volunteerId = Volunteer.volunteerId,
                    firstName = Volunteer.firstName,
                    lastName = Volunteer.lastName,
                    policeCheckPass = Volunteer.policeCheckPass,
                    email = Volunteer.email,
                    phone = Volunteer.phone,
                    approved = Volunteer.approved
                };
                VolunteerDtos.Add(newVolunteer);
            }

            return Ok(VolunteerDtos);
        }

        [ResponseType(typeof(Volunteer))]
        [HttpPost]
        public IHttpActionResult AddVolunteer([FromBody] Volunteer volunteer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.volunteers.Add(volunteer);
            db.SaveChanges();
            return Ok();
        }

    }
}