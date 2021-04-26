using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Manitouage1.Models;
using System.Diagnostics;

namespace Manitouage1.Controllers
{
    public class TestimonialDataController : ApiController
    {
        // db context
        private ManitouageDbContext db = new ManitouageDbContext();


        /// <summary>
        /// Get a list of testimonials in the database alongside a status code (200 OK).
        /// </summary>
        /// <returns>Returns a list of testimonials.</returns>
        /// <example>
        /// GET: api/testimonialdata/gettestimonials
        /// </example>
        [ResponseType(typeof(IEnumerable<TestimonialDto>))]
        [Route("api/testimonialdata/gettestimonials")]
        public IHttpActionResult GetTestimonials()
        {
            List<Testimonial> Testimonials = db.testimonials.ToList();
            List<TestimonialDto> TestimonialDtos = new List<TestimonialDto> { };

            foreach (var Testimonial in Testimonials)
            {
                TestimonialDto newTestimonial = new TestimonialDto
                {
                    testimonialId = Testimonial.testimonialId,
                    creationDate = Testimonial.creationDate,
                    testimonial = Testimonial.testimonial,
                    approved = Testimonial.approved,
                    UserId = Testimonial.UserId
                };
                TestimonialDtos.Add(newTestimonial);
            }

            return Ok(TestimonialDtos);
        }

        /// <summary>
        /// Adds a Testimonial to the database.
        /// </summary>
        /// <param name="testimonial">A testimonial object sent as POST form data.</param>
        /// <returns>returns status code 200 OK if successful, otherwise 400 if unsuccessful.</returns>
        /// <example>
        /// POST: api/testimonialdata/addtestimonial
        /// FORM DATA: Testimonial JSON Object
        /// </example>
        [ResponseType(typeof(Testimonial))]
        [HttpPost]
        public IHttpActionResult AddTestimonial([FromBody] Testimonial testimonial)
        {
            Debug.WriteLine("Adding Testimonial");
            Debug.WriteLine(ModelState);
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Invalid Model");
                Debug.WriteLine(ModelState);
                return BadRequest(ModelState);
            }
            Debug.WriteLine(testimonial);
            db.testimonials.Add(testimonial);
            db.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Find specific testimonial based on testimonial id.
        /// </summary>
        /// <param name="id">The testimonial id.</param>
        /// <returns>returns the testimonialdto along with a 200 status code if the testimonial is in the database. otherwise return 404.</returns>
        /// <example>
        /// GET: api/testimonialdata/findtestimonial/2
        /// </example>
        [HttpGet]
        [ResponseType(typeof(TestimonialDto))]
        public IHttpActionResult FindTestimonial(int id)
        {
            Testimonial Testimonial = db.testimonials.Find(id);

            if (Testimonial == null)
            {
                return NotFound();
            }

            TestimonialDto TestimonialDto = new TestimonialDto
            {
                testimonialId = Testimonial.testimonialId,
                creationDate = Testimonial.creationDate,
                testimonial = Testimonial.testimonial,
                approved = Testimonial.approved,
                UserId = Testimonial.UserId
            };

            return Ok(TestimonialDto);
        }

        /// <summary>
        /// deletes a testimonial from the database.
        /// </summary>
        /// <param name="id">the id of the testimonial.</param>
        /// <returns>returns 200 if successful, otherwise return 404.</returns>
        /// <example>
        /// POST: api/testimonialdata/deletetestimonial/2
        /// </example>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteTestimonial(int id)
        {
            Testimonial testimonial = db.testimonials.Find(id);
            if (testimonial == null)
            {
                return NotFound();
            }

            db.testimonials.Remove(testimonial);
            db.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// set testimonial approval to true.
        /// </summary>
        /// <param name="id">the id of the testimonial.</param>
        /// <returns>returns status 200 on success, or 404 if not successful.</returns>
        /// <example>
        /// POST: api/testimonialdata/approvetestimonial/2
        /// </example>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IHttpActionResult ApproveTestimonial(int id)
        {
            Testimonial testimonial = db.testimonials.Find(id);
            if (testimonial == null)
            {
                return NotFound();
            }

            testimonial.approved = true;
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
