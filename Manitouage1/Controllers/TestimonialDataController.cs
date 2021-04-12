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



        [ResponseType(typeof(IEnumerable<TestimonialDto>))]
        [System.Web.Http.Route("api/testimonialdata/gettestimonials")]
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

        [HttpPost]
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
