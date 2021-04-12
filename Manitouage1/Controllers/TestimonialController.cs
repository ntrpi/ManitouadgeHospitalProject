using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Manitouage1.Models;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Manitouage1.Controllers
{
    public class TestimonialController : Controller
    {

        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;

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
            return View();
        }

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
        public ActionResult Create(Testimonial Testimonial)
        {
            string url = "testimonialdata/addtestimonial";
            HttpContent content = new StringContent(jss.Serialize(Testimonial));
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