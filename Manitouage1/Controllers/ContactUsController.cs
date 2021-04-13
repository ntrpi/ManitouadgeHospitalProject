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
    public class ContactUsController : Controller
    {
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;

        static ContactUsController()
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

        // GET: ContactUs/List
        [HttpGet]
        public ActionResult List()
        {
            Debug.WriteLine("Here");
            // browser url
            string url = "ContactUsData/GetContactUs";

            // send and recieve http request and action
            HttpResponseMessage response = client.GetAsync(url).Result;

            // http call successful
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<ContactUsDto> ContactUsDtos = response.Content.ReadAsAsync<IEnumerable<ContactUsDto>>().Result;
                return View(ContactUsDtos);

            }
            else
            {
                // http call failed
                return RedirectToAction("Error");
            }


        }

        // GET: ContactUs/Details/5
        public ActionResult Details(int id)
        {
            // browser url
            string url = "ContactUsData/GetContactUs/" + id;

            // send and recieve http request and action
            HttpResponseMessage response = client.GetAsync(url).Result;

            // http call successful
            if (response.IsSuccessStatusCode)
            {
                ContactUsDto ContactUsDto = response.Content.ReadAsAsync<ContactUsDto>().Result;
                return View(ContactUsDto);
            }
            return RedirectToAction("Error");
        }

        // GET: ContactUs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactUs/Create
        [HttpPost]
        public ActionResult Create(ContactUs ContactUs)
        {

            // browser url
            string url = "ContactUsData/CreateContactUs";
            HttpContent content = new StringContent(jss.Serialize(ContactUs));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {

                int ContactUsId = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Details", new { id = ContactUsId });
            }
            return RedirectToAction("Error");
        }


        // GET: ContactUs/Edit/5
        public ActionResult Edit(int id)
        {
            // browser url
            string url = "ContactUsData/GetContactUs/" + id;

            // send and recieve http request and action
            HttpResponseMessage response = client.GetAsync(url).Result;

            // http call successful
            if (response.IsSuccessStatusCode)
            {
                ContactUsDto SelectedContactUs = response.Content.ReadAsAsync<ContactUsDto>().Result;
                return View(SelectedContactUs);
            }

            return RedirectToAction("Error");
        }
        // POST: ContactUs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(int id, ContactUs ContactUsInfo)
        {
            string url = "ContactUsData/UpdateContactUs/" + id;

            Debug.WriteLine(jss.Serialize(ContactUsInfo));
            HttpContent content = new StringContent(jss.Serialize(ContactUsInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", new { id = id });
            }

            return RedirectToAction("Error");
        }


        // GET: ContactUs/Delete/5
        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "ContactUsData/GetContactUs/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                ContactUsDto SelectedContactUs = response.Content.ReadAsAsync<ContactUsDto>().Result;
                return View(SelectedContactUs);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: ContactUs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Delete(int id)
        {
            string url = "ContactUsData/DeleteContactUs/" + id;

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