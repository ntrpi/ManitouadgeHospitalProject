using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Manitouage1.Models;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using Microsoft.AspNet.Identity;

namespace Manitouage1.Controllers
{
    public class TestimonialController : Controller
    {

        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;

        /// <summary>
        /// This allows us to access a pre-defined C# HttpClient 'client' variable for sending POST and GET requests to the data access layer.
        /// </summary>
        static TestimonialController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                //cookies are manually set in RequestHeader
                UseCookies = false
            };

            client = new HttpClient(handler);
            //change this to match your own local port number
            client.BaseAddress = new Uri("https://localhost:44397/api/");
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Testimonial
        public ActionResult Index()
        {
            //var test = User.IsInRole("Admin");
            //Debug.WriteLine(test);

            return View();
        }

        // GET: Testimonial/List
        public ActionResult List()
        {
            string url = "testimonialdata/gettestimonials";

            HttpResponseMessage res = client.GetAsync(url).Result;

            if (res.IsSuccessStatusCode)
            {
                IEnumerable<TestimonialDto> TestimonialDtos = res.Content.ReadAsAsync<IEnumerable<TestimonialDto>>().Result;


                return View(TestimonialDtos);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(string testimonial, DateTime creationDate, bool approved, string UserId)
        {
            //@DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")
            string url = "testimonialdata/addtestimonial";
            Debug.WriteLine("testimonial from body: " + testimonial);
            Testimonial newTestimonial = new Testimonial();
            newTestimonial.creationDate = creationDate;
            newTestimonial.testimonial = testimonial;
            newTestimonial.approved = approved;
            newTestimonial.UserId = UserId;
            Debug.WriteLine("testimonial creation date" + newTestimonial.creationDate);
             
            HttpContent content = new StringContent(jss.Serialize(newTestimonial));

            Debug.WriteLine(content);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage res = client.PostAsync(url, content).Result;

            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        public ActionResult DeleteConfirm(int id)
        {
            string url = "testimonialdata/findtestimonial/" + id;
            HttpResponseMessage res = client.GetAsync(url).Result;

            if (res.IsSuccessStatusCode)
            {
                TestimonialDto TestimonialDto = res.Content.ReadAsAsync<TestimonialDto>().Result;
                return View(TestimonialDto);
            }
            return RedirectToAction("Error");
        }

        public ActionResult Details(int id)
        {
            string url = "testimonialdata/findtestimonial/" + id;
            HttpResponseMessage res = client.GetAsync(url).Result;

            if (res.IsSuccessStatusCode)
            {
                TestimonialDto TestimonialDto = res.Content.ReadAsAsync<TestimonialDto>().Result;
                return View(TestimonialDto);
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Delete(int id)
        {
            string url = "testimonialdata/deletetestimonial/" + id;
            HttpContent content = new StringContent("");
            HttpResponseMessage res = client.PostAsync(url, content).Result;

            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Error");
        }

        public ActionResult ApproveConfirm(int id)
        {
            string url = "testimonialdata/findtestimonial/" + id;
            HttpResponseMessage res = client.GetAsync(url).Result;

            if (res.IsSuccessStatusCode)
            {
                TestimonialDto TestimonialDto = res.Content.ReadAsAsync<TestimonialDto>().Result;
                return View(TestimonialDto);
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public ActionResult Approve(int id)
        {
            string url = "testimonialdata/approvetestimonial/" + id;
            HttpContent content = new StringContent("");
            HttpResponseMessage res = client.PostAsync(url, content).Result;

            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Error");
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}