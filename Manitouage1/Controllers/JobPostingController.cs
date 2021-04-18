using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Manitouage1.Models;
using Manitouage1.Models.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Web.Script.Serialization;

namespace Manitouage1.Controllers
{
    public class JobPostingController : Controller
    {
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;

        static JobPostingController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                UseCookies = false
            };
            client = new HttpClient(handler);
            //change this to match your own local port number
            client.BaseAddress = new Uri("https://localhost:44397/api/");
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));


            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);

        }

        private void GetApplicationCookie()
        {
            string token = "";
           
            client.DefaultRequestHeaders.Remove("Cookie");
            if (!User.Identity.IsAuthenticated) return;

            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get(".AspNet.ApplicationCookie");
            if (cookie != null) token = cookie.Value;

            Debug.WriteLine("Token Submitted is : " + token);
            if (token != "") client.DefaultRequestHeaders.Add("Cookie", ".AspNet.ApplicationCookie=" + token);

            return;
        }


        // GET: JobPostitng/List
        public ActionResult List()
        {
            ListJobPostings ViewModel = new ListJobPostings();
            ViewModel.isadmin = User.IsInRole("Admin");

            string url = "jobpostingdata/getjobpostings";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<JobPostingDto> SelectedJobPosting = response.Content.ReadAsAsync<IEnumerable<JobPostingDto>>().Result;
                ViewModel.jobpostings = SelectedJobPosting;
                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: JobPosting/Details/5
        public ActionResult Details(int id)
        {
            ShowJobPosting ViewModel = new ShowJobPosting();
            ViewModel.isadmin = User.IsInRole("Admin");

            string url = "jobpostingdata/findjobposting/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            Debug.WriteLine("Details:" + response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into department data transfer object
                JobPostingDto SelectedJobPosting = response.Content.ReadAsAsync<JobPostingDto>().Result;
                ViewModel.jobposting = SelectedJobPosting;


                 url = "jobpostingdata/finddepartmentforjobposting/" + id;
                 response = client.GetAsync(url).Result;
                 DepartmentDto SelectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;
                 ViewModel.department = SelectedDepartment;

                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: JobPosting/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {

            UpdateJobPosting ViewModel = new UpdateJobPosting();
            //get information about Department
            string url = "departmentdata/getdepartments";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<DepartmentDto> PotentialDepartments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
            ViewModel.alldepartments = PotentialDepartments;

            return View(ViewModel);
        }

        

        // POST: JobPosting/Create
        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(JobPosting JobPostingInfo)
        {
            GetApplicationCookie();

            Debug.WriteLine("Create Post");
            Debug.WriteLine(JobPostingInfo.jobPostingId);
            string url = "jobpostingdata/addjobposting";
            Debug.WriteLine(jss.Serialize(JobPostingInfo));
            HttpContent content = new StringContent(jss.Serialize(JobPostingInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {

                int jobPostingid = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Details", new { id = jobPostingid });
            }
            else
            {
                return RedirectToAction("Error");
            }


        }

        // GET: JobPosting/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            UpdateJobPosting ViewModel = new UpdateJobPosting();

            string url = "jobpostingdata/findjobposting/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            Debug.WriteLine("edit: " + response.StatusCode);
            if (response.IsSuccessStatusCode)
            {

                JobPostingDto SelectedJobPosting = response.Content.ReadAsAsync<JobPostingDto>().Result;
                ViewModel.jobposting = SelectedJobPosting;
                

                 url = "departmentdata/getdepartments";
                 response = client.GetAsync(url).Result;
                 IEnumerable<DepartmentDto> PotentialDepartments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
                Debug.WriteLine("Jobposting before getDepartment:");

                ViewModel.alldepartments = PotentialDepartments;
               
                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: JobPosting/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, JobPosting JobPostingInfo)
        {
            GetApplicationCookie();

            Debug.WriteLine("Controller Edit: " + JobPostingInfo.jobPostingId);
            string url = "jobpostingdata/updatejobposting/" + id;
            Debug.WriteLine(jss.Serialize(JobPostingInfo));
            HttpContent content = new StringContent(jss.Serialize(JobPostingInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Details", new { id = id });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: JobPosting/Delete/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "jobpostingdata/findjobposting/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into jobposting data transfer object
                JobPostingDto SelectedJobPosting = response.Content.ReadAsAsync<JobPostingDto>().Result;
                return View(SelectedJobPosting);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: JobPosting/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            GetApplicationCookie();

            Debug.WriteLine("Delete");
            string url = "jobpostingdata/deletejobposting/" + id;
            //post body is empty
            HttpContent content = new StringContent("");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
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