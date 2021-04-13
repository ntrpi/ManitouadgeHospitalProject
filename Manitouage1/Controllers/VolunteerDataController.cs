using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        [ResponseType(typeof(VolunteerDto))]
        public IHttpActionResult FindVolunteer(int id)
        {
            Volunteer Volunteer = db.volunteers.Find(id);

            if (Volunteer == null)
            {
                return NotFound();
            }

            VolunteerDto VolunteerDto = new VolunteerDto
            {
                volunteerId = Volunteer.volunteerId,
                firstName = Volunteer.firstName,
                lastName = Volunteer.lastName,
                policeCheckPass = Volunteer.policeCheckPass,
                email = Volunteer.email,
                phone = Volunteer.phone,
                approved = Volunteer.approved
            };

            return Ok(VolunteerDto);
        }

        [HttpPost]
        public IHttpActionResult DeleteVolunteer(int id)
        {
            Volunteer volunteer = db.volunteers.Find(id);
            if (volunteer == null)
            {
                return NotFound();
            }

            db.volunteers.Remove(volunteer);
            db.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult ApproveVolunteer(int id)
        {
            Volunteer volunteer = db.volunteers.Find(id);
            if (volunteer == null)
            {
                return NotFound();
            }

            volunteer.approved = true;
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

    }
}