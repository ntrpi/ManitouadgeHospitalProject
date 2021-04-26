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

namespace Manitouage1.Controllers
{
    public class VolunteerController : Controller

    {

        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;

        static VolunteerController()
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


        // GET: Volunteer
        // Lists all events that require volunteers
        public ActionResult Index()
        {
            // get list of events
            string url = "eventdata/getevents";
            HttpResponseMessage res = client.GetAsync(url).Result;
            if (res.IsSuccessStatusCode)
            {
                IEnumerable<EventDto> EventDtos = res.Content.ReadAsAsync<IEnumerable<EventDto>>().Result;
                return View(EventDtos);
            }
            return View();
        }

        // GET: List all volunteer applications
        // only admin can see the list of volunteers
        [Authorize(Roles = "Admin")]
        public ActionResult List()
        {
            //List<VolunteerDto> Volunteers = new List<VolunteerDto>();

            string url = "volunteerdata/getvolunteers";

            HttpResponseMessage res = client.GetAsync(url).Result;

            if (res.IsSuccessStatusCode)
            {
                IEnumerable<VolunteerDto> VolunteerDtos = res.Content.ReadAsAsync<IEnumerable<VolunteerDto>>().Result;


                return View(VolunteerDtos);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: View for volunteer application. 
        // TODO figure out how to handle policecheck (maybe file upload then admin update?)
        public ActionResult Create(int? id)
        {
            string url = "eventdata/getevent/" + id;
            HttpResponseMessage res = client.GetAsync(url).Result;

            // event exists
            if (res.IsSuccessStatusCode)
            {
                ViewBag.eventId = id;
                return View();
            }

            ViewBag.eventId = null;
            return View();
        }

        // POST : bind volunteer model to form post
        // TODO: create some screening questions for the application
        // TODO: add validation to create form
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(Volunteer Volunteer)
        {

            string url = "volunteerdata/addvolunteer";
            HttpContent content = new StringContent(jss.Serialize(Volunteer));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage res = client.PostAsync(url, content).Result;

            if (res.IsSuccessStatusCode)
            {
                // List is essentially admin dashboard
                if (User.IsInRole("Admin")) {
                    return RedirectToAction("List");
                }

                return RedirectToAction("ConfirmApplication");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        public ActionResult Edit(int id)
        {
            string url = "volunteerdata/findvolunteer/" + id;
            HttpResponseMessage res = client.GetAsync(url).Result;

            if (res.IsSuccessStatusCode)
            {
                VolunteerDto VolunteerDto = res.Content.ReadAsAsync<VolunteerDto>().Result;
                return View(VolunteerDto);
            }
            return RedirectToAction("Error");
        }

        public ActionResult DeleteConfirm(int id)
        {
            string url = "volunteerdata/findvolunteer/" + id;
            HttpResponseMessage res = client.GetAsync(url).Result;

            if (res.IsSuccessStatusCode)
            {
                VolunteerDto VolunteerDto = res.Content.ReadAsAsync<VolunteerDto>().Result;
                return View(VolunteerDto);
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Delete(int id)
        {
            string url = "volunteerdata/deletevolunteer/" + id;
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
            string url = "volunteerdata/findvolunteer/" + id;
            HttpResponseMessage res = client.GetAsync(url).Result;

            if (res.IsSuccessStatusCode)
            {
                VolunteerDto VolunteerDto = res.Content.ReadAsAsync<VolunteerDto>().Result;
                return View(VolunteerDto);
            }
            return RedirectToAction("Error");
        }


        [HttpPost]
        public ActionResult Approve(int id)
        {
            string url = "volunteerdata/approvevolunteer/" + id;
            HttpContent content = new StringContent("");
            HttpResponseMessage res = client.PostAsync(url, content).Result;

            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Error");
        }

        public ActionResult ConfirmApplication()
        {
            return View();
        }

        // GET: Volunteer/Error
        public ActionResult Error()
        {
            return View();
        }
    }
}