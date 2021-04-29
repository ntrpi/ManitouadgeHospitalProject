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


        /// <summary>
        /// Get a list of volunteers in the database alongside a status code (200 OK).
        /// </summary>
        /// <returns>Returns a list of volunteers.</returns>
        /// <example>
        /// GET: api/volunteerdata/getvolunteers
        /// </example>
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

        /// <summary>
        /// Adds a volunteer to the database.
        /// </summary>
        /// <param name="volunteer">A volunteer object sent as POST form data.</param>
        /// <returns>returns status code 200 OK if successful, otherwise 400 if unsuccessful.</returns>
        /// <example>
        /// POST: api/volunteerdata/addvolunteer
        /// FORM DATA: volunteer JSON Object
        /// </example>
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

        /// <summary>
        /// Find specific volunteer based on volunteer id.
        /// </summary>
        /// <param name="id">The volunteer id.</param>
        /// <returns>returns the volunteerdto along with a 200 status code if the volunteer is in the database. otherwise return 404.</returns>
        /// <example>
        /// GET: api/volunteerdata/findvolunteer/2
        /// </example>
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

        /// <summary>
        /// deletes a volunteer from the database.
        /// </summary>
        /// <param name="id">the id of the volunteer.</param>
        /// <returns>returns 200 if successful, otherwise return 404.</returns>
        /// <example>
        /// POST: api/volunteerdata/deletevolunteer/2
        /// </example>
        [HttpPost]
        [Authorize(Roles = "Admin")]
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

        /// <summary>
        /// set volunteer approval to true.
        /// </summary>
        /// <param name="id">the id of the volunteer.</param>
        /// <returns>returns status 200 on success, or 404 if not successful.</returns>
        /// <example>
        /// POST: api/volunteerdata/approvevolunteer/2
        /// </example>
        [HttpPost]
        [Authorize(Roles = "Admin")]
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