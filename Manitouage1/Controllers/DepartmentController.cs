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
    public class DepartmentController : Controller
    {
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;

        static DepartmentController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                UseCookies = false
            };
            client = new HttpClient(handler);
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


        // GET: Department/List
        public ActionResult List()
        {

            ListDepartments ViewModel = new ListDepartments();
            ViewModel.isadmin = User.IsInRole("Admin");

            string url = "departmentdata/getdepartments";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<DepartmentDto> SelectedDepaprtments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
                ViewModel.departments = SelectedDepaprtments;
                return View(ViewModel);
            }
            else
            {

                return RedirectToAction("Error");
            }
        }

        // GET: Department/Details/5
        public ActionResult Details(int id)
        {
            ShowDepartment ViewModel = new ShowDepartment();
            string url = "departmentdata/finddepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into department data transfer object
                DepartmentDto SelectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;
                ViewModel.department = SelectedDepartment;


               // url = "departmentdata/findteamforplayer/" + id;
               // response = client.GetAsync(url).Result;
               // TeamDto SelectedTeam = response.Content.ReadAsAsync<TeamDto>().Result;
               // ViewModel.team = SelectedTeam;

                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Department/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Department DepartmentInfo)
        {
            GetApplicationCookie();

            Debug.WriteLine(DepartmentInfo.departmentName);
            string url = "departmentdata/adddepartment";
            Debug.WriteLine(jss.Serialize(DepartmentInfo));
            HttpContent content = new StringContent(jss.Serialize(DepartmentInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {

                int departmentid = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Details", new { id = departmentid });
            }
            else
            {
                return RedirectToAction("Error");
            }


        }

        // GET: Department/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
           
           // UpdateDepartment ViewModel = new UpdateDepartment();

            string url = "departmentdata/finddepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            Debug.WriteLine("edit: " + response.StatusCode);
            if (response.IsSuccessStatusCode)
            {

                DepartmentDto SelectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;

                return View(SelectedDepartment);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Department/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, Department DepartmentInfo)
        {
            GetApplicationCookie();

            Debug.WriteLine("Controller Edit: " +DepartmentInfo.departmentId);
            string url = "departmentdata/updatedepartment/" + id;
            Debug.WriteLine(jss.Serialize(DepartmentInfo));
            HttpContent content = new StringContent(jss.Serialize(DepartmentInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {

                //Send over image data for player
               // url = "departmentdata/updatedepartmentpic/" + id;
               // Debug.WriteLine("Received player picture " + PlayerPic.FileName);

                //MultipartFormDataContent requestcontent = new MultipartFormDataContent();
               // HttpContent imagecontent = new StreamContent(PlayerPic.InputStream);
               // requestcontent.Add(imagecontent, "PlayerPic", PlayerPic.FileName);
               // response = client.PostAsync(url, requestcontent).Result;

                return RedirectToAction("Details", new { id = id });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Department/Delete/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "departmentdata/finddepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into department data transfer object
                DepartmentDto SelectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;
                return View(SelectedDepartment);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Department/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            GetApplicationCookie();
            string url = "departmentdata/deletedepartment/" + id;
            //post body is empty
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