using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using Manitouage1.Models;
using System.Diagnostics;

namespace Manitouage1.Controllers
{
    public class EventsController : Controller
    {
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;

        // GET: Events
        static EventsController()
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

        // GET: Events/Details/5

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult List()
        {
            Debug.WriteLine("Here");
            // Create the string just as you would if you were typing it in the browser.
            string url = "EventsData/GetEvents";

            // Send the http request and get an http action response.
            HttpResponseMessage response = client.GetAsync(url).Result;

            // The http call worked.
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<EventDto> EventDtos = response.Content.ReadAsAsync<IEnumerable<EventDto>>().Result;
                return View(EventDtos);

            }
            else
            {
                // The http call failed.
                return RedirectToAction("Error");
            }

            //always insert variable from second statement into brackets beside view
        }
        public ActionResult Details(int id)
        {
            // Create the string just as you would if you were typing it in the browser.
            string url = "EventsData/GetQuestion/" + id;

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

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Events/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Events/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
