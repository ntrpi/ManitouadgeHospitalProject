using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Manitouage1.Models;
using Manitouage1.Models.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Web.Script.Serialization;

namespace Manitouage1.Controllers
{
    public class EventController : Controller
    {
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;

        static EventController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };
            client = new HttpClient(handler);
            //change this to match your own local port number
            client.BaseAddress = new Uri("https://localhost:44397/api/");
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));


            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);

        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: Event/List
        [HttpGet]
        public ActionResult List()
        {
            ListEventxDonation ViewModel = new ListEventxDonation();
            // Create the string just as you would if you were typing it in the browser.
            string url = "EventData/GetEvents";

            // Send the http request and get an http action response.
            HttpResponseMessage response = client.GetAsync(url).Result;

            // The http call worked.
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<EventDto> EventDtos = response.Content.ReadAsAsync<IEnumerable<EventDto>>().Result;
                ViewModel.events = EventDtos;
                return View(ViewModel);

            }
            else
            {
                // The http call failed.
                return RedirectToAction("Error");
            }

            //always insert variable from second statement into brackets beside view
        }

        // GET: Event/Details/5
        public ActionResult Details(int id)
        {
            // Create the string just as you would if you were typing it in the browser.
            string url = "EventData/GetEvent/" + id;

            // Send the http request and get an http action response.
            HttpResponseMessage response = client.GetAsync(url).Result;

            // The http call worked.
            if (response.IsSuccessStatusCode)
            {
                EventDto eventDto = response.Content.ReadAsAsync<EventDto>().Result;
                return View(eventDto);
            }
            return RedirectToAction("Error");
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        public ActionResult Create(Event Events)
        {
            Debug.WriteLine("Here");
            // Create the string just as you would if you were typing it in the browser.
            string url = "EventData/AddEvent";
            HttpContent content = new StringContent(jss.Serialize(Events));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {

                int EventId = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Details", new { id = EventId });
            }
            return RedirectToAction("Error");
        }


        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            // Create the string just as you would if you were typing it in the browser.
            string url = "EventData/GetEvent/" + id;

            // Send the http request and get an http action response.
            HttpResponseMessage response = client.GetAsync(url).Result;

            // The http call worked.
            if (response.IsSuccessStatusCode)
            {
                EventDto SelectedEvent = response.Content.ReadAsAsync<EventDto>().Result;
                return View(SelectedEvent);
            }

            return RedirectToAction("Error");
        }
        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(int id, Event EventInfo)
        {
            string url = "EventData/UpdateEvent/" + id;

            Debug.WriteLine(jss.Serialize(EventInfo));
            HttpContent content = new StringContent(jss.Serialize(EventInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", new { id = id });
            }

            return RedirectToAction("Error");
        }


        // GET: Event/Delete/5
        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "EventData/GetEvent/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                EventDto SelectedEvent = response.Content.ReadAsAsync<EventDto>().Result;
                return View(SelectedEvent);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Event/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Delete(int id)
        {
            string url = "EventData/DeleteEvent/" + id;

            HttpContent content = new StringContent("");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }
        public ActionResult Error()
        {
            return View();
        }
    }
}