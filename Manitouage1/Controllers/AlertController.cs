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
    public class AlertController : Controller
    {
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;
        // GET: Alert
        static AlertController()
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
        // GET: Alert
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public EventDto GetEventDto(int id)
        {
            // Create the string just as you would if you were typing it in the browser.
            string url = "EventData/GetEvent/" + id;

            // Send the http request and get an http action response.
            HttpResponseMessage response = client.GetAsync(url).Result;

            // The http call worked.
            if (response.IsSuccessStatusCode)
            {
                EventDto eventDto = response.Content.ReadAsAsync<EventDto>().Result;
                return eventDto;
            }
            return null;
        }

        // GET: Alert/Details/5
        public ActionResult Details(int id)
        {
            // Create the string just as you would if you were typing it in the browser.
            string url = "AlertData/GetAlert/" + id;

            // Send the http request and get an http action response.
            HttpResponseMessage response = client.GetAsync(url).Result;

            // The http call worked.
            if (response.IsSuccessStatusCode)
            {
                ViewAlert ViewAlert = new ViewAlert();
                AlertDto alertDto = response.Content.ReadAsAsync<AlertDto>().Result;
                ViewAlert.alert = alertDto;

                if(alertDto.EventId != 0)
                {
                    EventDto eventDto = GetEventDto(alertDto.EventId);
                    ViewAlert.eventDto = eventDto;

                }
                return View(ViewAlert);
            }
            return RedirectToAction("Error");
        }

        // GET: Alert/GetAlerts
        [HttpGet]
        public ActionResult List()
        {
            Debug.WriteLine("Here");
            // Create the string just as you would if you were typing it in the browser.
            string url = "AlertData/GetAlerts";

            // Send the http request and get an http action response.
            HttpResponseMessage response = client.GetAsync(url).Result;

            // The http call worked.
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<AlertDto> AlertDtos = response.Content.ReadAsAsync<IEnumerable<AlertDto>>().Result;
                return View(AlertDtos);

            }
            else
            {
                // The http call failed.
                return RedirectToAction("Error");
            }

            //always insert variable from second statement into brackets beside view
        }

        // GET: Alert/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Alert/Create
        [HttpPost]
        public ActionResult Create(Alert Alerts)
        {
            Debug.WriteLine("Here");
            // Create the string just as you would if you were typing it in the browser.
            string url = "AlertData/AddAlert";
            HttpContent content = new StringContent(jss.Serialize(Alerts));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {

                int alertId = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Details", new { id = alertId });
            }
            return RedirectToAction("Error");
        }

        // GET: Alert/Edit/5
        public ActionResult Edit(int id)
        {
            // Create the string just as you would if you were typing it in the browser.
            string url = "AlertData/GetAlert/" + id;

            // Send the http request and get an http action response.
            HttpResponseMessage response = client.GetAsync(url).Result;

            // The http call worked.
            if (response.IsSuccessStatusCode)
            {
                AlertDto SelectedAlert = response.Content.ReadAsAsync<AlertDto>().Result;
                return View(SelectedAlert);
            }

            return RedirectToAction("Error");
        }

        // POST: Alert/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(int id, Alert AlertInfo)
        {
            string url = "AlertData/UpdateAlert/" + id;

            Debug.WriteLine(jss.Serialize(AlertInfo));
            HttpContent content = new StringContent(jss.Serialize(AlertInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", new { id = id });
            }

            return RedirectToAction("Error");
        }

        // GET: Alert/Delete/5
        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "AlertData/GetAlert/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                AlertDto SelectedAlert = response.Content.ReadAsAsync<AlertDto>().Result;
                return View(SelectedAlert);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        // POST: Alert/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Delete(int id)
        {
            string url = "AlertData/DeleteAlert/" + id;

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
    }
}
