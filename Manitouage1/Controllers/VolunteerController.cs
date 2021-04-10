﻿using System;
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
        // do stuff with this later, maybe admin dashboard?
        public ActionResult Index()
        {
            return View();
        }

        // GET: List all volunteer applications
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
        public ActionResult Create()
        {
            return View();
        }

        // POST : bind volunteer model to form post
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
                return RedirectToAction("List");
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
            return View();
        }

        public ActionResult DeleteConfirm(int id)
        {
            return RedirectToAction("Error");
        }

        public ActionResult Delete(int id)
        {
            return RedirectToAction("List");
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}