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

    // Most of the controller code has been referenced from "VarsityMvp, Passion Project and inclass examples"

    public class DonationController : Controller
    {
        //reference my passion project also varsity with auth
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;

        static DonationController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };
            client = new HttpClient(handler);
            //Christine comment:: change this to match your own local port number
            client.BaseAddress = new Uri("https://localhost:44397/api/");
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));


        }
        // GET: Donation
        //for views where it links back to list
        public ActionResult Index()
        {
            return View("List");
        }

        //GET: Donation/List
        [HttpGet]
        public ActionResult List()
        {
            //using view model to view donations
            ListDonation ViewModel = new ListDonation();

            //debugging to see if this controller works
            Debug.WriteLine("Here");

            //URL string for browser
            string url = "DonationData/getdonations";

            // Sending HHTP request and getting a http response 
            HttpResponseMessage response = client.GetAsync(url).Result;

            //if the response works show alist of donations 
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<DonationDto> allDonations = response.Content.ReadAsAsync<IEnumerable<DonationDto>>().Result;

                //return View(Donations);

                ViewModel.alldonations = allDonations;
                return View(ViewModel);

            }
            else
            {
                //else the call failed return "error" string 
                return RedirectToAction("Error");
            }


        }

        // GET: Donation/Details/5
        public ActionResult Details(int id)
        {
            //using view model to detail donation by id REFERENCE teamController in varsity w auth
            ShowDonation ViewModel = new ShowDonation();

            //creating a url string
            string url = "DonationData/FindDonation/" + id;

            //sending http request and getting a response back
            HttpResponseMessage response = client.GetAsync(url).Result;

            //if statment:: if the call worked return this else return error
            if (response.IsSuccessStatusCode)
            {
                DonationDto SelectedDonation = response.Content.ReadAsAsync<DonationDto>().Result;
                ViewModel.donation = SelectedDonation;

                //add the get statement for events related to the donations (id is the donation id that we will use to get details)
                //REF VIDEO for week 4 before passion project (git code was harder to understand)
                url = "DonationData/FindEventForDonation" + id;//donation id FYI
                response = client.GetAsync(url).Result;
                EventDto SelectedEvent = response.Content.ReadAsAsync<EventDto>().Result;
                ViewModel.events = SelectedEvent;

                return View(ViewModel);
            }
            else
            {

                return RedirectToAction("Error");

            }
        }

        // GET: Donation/Create
        public ActionResult Create()
        {
            //using view model for add donation.
            //here we want to display a form and a drop down for all events if the user wished to donate to an event
            CreateDonation ViewModel = new CreateDonation();
            //Pulling from AMANDA's eventcontroller for get events
            string url = "EventData/GetEvents";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<EventDto> PotentialEvents = response.Content.ReadAsAsync<IEnumerable<EventDto>>().Result;

            ViewModel.allevents = PotentialEvents;

            return View(ViewModel);
        }

        // POST: Donation/Create
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(Donation donationInfo)
        {
            //Debug.WriteLine("THIS IS WORKING!!!");
            // url string we will use to send port request  
            string url = "DonationData/AddDonation";
            //Debug.WriteLine(jss.Serialize(donationInfo));
            HttpContent content = new StringContent(jss.Serialize(donationInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                int donationId = response.Content.ReadAsAsync<int>().Result;
                //in this case we would redirect to list page for user to review the donation posted 
                return RedirectToAction("Details", new { id = donationId });
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        // GET: Donation/Edit/5
        //UPDATED CODE to get the event id to edit although I dont think event id needs to be edited as anything with money should not be edit not sure how to lock that down
        public ActionResult Edit(int id)
        {
            //add n the new view model and create a new instance of it 
            UpdateDonation ViewModel = new UpdateDonation();
            //starting with the url string REFERENCES:varsity MVP for this code and inclass live example
            //this is where I had issues in my passion project!!!! (Mind blown)
            string url = "DonationData/FindDonation/" + id;
            //reusing code from "Find Donation By Id".
            HttpResponseMessage response = client.GetAsync(url).Result;

            // The http call worked.
            if (response.IsSuccessStatusCode)
            {
                DonationDto Selecteddonation = response.Content.ReadAsAsync<DonationDto>().Result;
                ViewModel.donation = Selecteddonation;

                //adding in event id 
                //REUSING CODE FROM create donation 
                //url : from AMANDA's DATA CONTROLLER
                url = "EventData/GetEvents";
                response = client.GetAsync(url).Result;
                IEnumerable<EventDto> PotentialEvents = response.Content.ReadAsAsync<IEnumerable<EventDto>>().Result;
                ViewModel.allevents = PotentialEvents;

                return View(ViewModel);

            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        // POST: Donation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(int id, Donation donationInfo)
        {
            string url = "DonationData/UpdateDonation/" + id;

            Debug.WriteLine(jss.Serialize(donationInfo));
            HttpContent content = new StringContent(jss.Serialize(donationInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                //redirect to details page for the user to view the details that have been updated 
                return RedirectToAction("Details", new { id = id });
            }
            else
            {
                return RedirectToAction("Error");
            }


        }

        // GET: Donation/Delete/5
        //PROCESS:: first get the dontaion by id -> confirm the delete ->then delete the choosen donation and redirect to new list page without the donation
        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "DonationData/FindDonation/" + id;
            //sending HTTP request 
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                DonationDto Selecteddonation = response.Content.ReadAsAsync<DonationDto>().Result;
                return View(Selecteddonation);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Donation/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Delete(int id)
        {
            string url = "DonationData/DeleteDonation/" + id;

            //no new Http content to be displayed here
            HttpContent content = new StringContent("");
            //Sending reponse request 
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            //if successful return the user to List view with the update list..else error
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
