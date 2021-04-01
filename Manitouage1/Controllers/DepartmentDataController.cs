using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Manitouage1.Models;
using System.Diagnostics;

namespace Manitouage1.Controllers
{
    public class DepartmentDataController : ApiController
    {
        private ManitouageDbContext db = new ManitouageDbContext();

        // GET: api/DepartmentData/GetDepartments
        [ResponseType(typeof(IEnumerable<DepartmentDto>))]
        public IHttpActionResult GetDepartments()
        {
            List<Department> Departments = db.departments.ToList();
            List<DepartmentDto> DepartmentDtos = new List<DepartmentDto> { };

            Debug.WriteLine("Files Received");

            
            foreach (var Department in Departments)
            {
               // Salon Salon = db.Salons
               //.Where(s => s.Members.Any(m => m.SalonID == Member.SalonID))
               //.FirstOrDefault();
                //if not found, return 404 status code.
               // if (Salon == null)
               // {
                 //   return NotFound();
               // }

                DepartmentDto NewDepartment = new DepartmentDto
                {
                    departmentId = Department.departmentId,
                    departmentName = Department.departmentName,
                    email = Department.email,
                    phone = Department.phone,
                    fax = Department.fax,
                    extension = Department.extension,
                    category = Department.category
                };

                DepartmentDtos.Add(NewDepartment);
            }

            return Ok(DepartmentDtos);
        }


        // GET: api/DepartmentData/FindDepartment/5
        [HttpGet]
        [ResponseType(typeof(DepartmentDto))]
        public IHttpActionResult FindDepartment(int id)
        {
            Department Department = db.departments.Find(id);
            if (Department == null)
            {
                return NotFound();
            }

            //Salon Salon = db.Salons
            //.Where(s => s.Members.Any(m => m.SalonID == Member.SalonID))
            //.FirstOrDefault();
            //if not found, return 404 status code.
            //if (Salon == null)
            // {
            //   return NotFound();
            //}

            DepartmentDto DepartmentDto = new DepartmentDto
            {
                departmentId = Department.departmentId,
                departmentName = Department.departmentName,
                email = Department.email,
                phone = Department.phone,
                fax = Department.fax,
                extension = Department.extension,
                category = Department.category
            };


            //pass along data as 200 status code OK response
            return Ok(DepartmentDto);
        }

        // PUT: api/MemberData/FindSalonForMember/5
       // [HttpGet]
       // [ResponseType(typeof(IEnumerable<SalonDto>))]
       // public IHttpActionResult FindSalonForMember(int id)
        //{

          //  Salon Salon = db.Salons
           //     .Where(s => s.Members.Any(m => m.SalonID == id))
             //   .FirstOrDefault();
            //if not found, return 404 status code.
           // if (Salon == null)
            //{
              //  return NotFound();
            //}

            //SalonDto SalonDto = new SalonDto
            //{
              //  SalonID = Salon.SalonID,
                //SalonName = Salon.SalonName

            //};


            //pass along data as 200 status code OK response
            //return Ok(SalonDto);
        //}

        /// POST: api/DepartmentData/AddDepartment
        ///  FORM DATA: Department JSON Object
        /// </example>
        [ResponseType(typeof(Department))]
        [HttpPost]
        public IHttpActionResult AddDepartment([FromBody] Department department)
        {
            //Will Validate according to data annotations specified on model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.departments.Add(department);
            db.SaveChanges();

            return Ok(department.departmentId);
        }

        /// POST: api/DepartmentData/UpdateDepartment/5

        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateDepartment(int id, [FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != department.departmentId)
            {
                return BadRequest();
            }

            db.Entry(department).State = EntityState.Modified;

            try
            {
                db.SaveChanges();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

      //  [HttpPost]
      // public IHttpActionResult UpdateMemberPic(int id)
       // {

       //     bool haspic = false;
         //   string picextension;
          //  if (Request.Content.IsMimeMultipartContent())
         //   {
         //       Debug.WriteLine("Received multipart form data.");

          //      int numfiles = HttpContext.Current.Request.Files.Count;
          //      Debug.WriteLine("Files Received: " + numfiles);

                //Check if a file is posted
             //   if (numfiles == 1 && HttpContext.Current.Request.Files[0] != null)
            //    {
             //       var MemberPic = HttpContext.Current.Request.Files[0];
                    //Check if the file is empty
            //        if (MemberPic.ContentLength > 0)
             //       {
               //         var valtypes = new[] { "jpeg", "jpg", "png", "gif" };
             //           var extension = Path.GetExtension(MemberPic.FileName).Substring(1);
                        //Check the extension of the file
                 //       if (valtypes.Contains(extension))
                  //      {
                    //        try
                      //      {
                                //file name is the id of the image
                        //        string fn = id + "." + extension;

                                //get a direct file path to ~/Content/Members/{id}.{extension}
                          //      string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Members/"), fn);

                                //save the file
                            //    MemberPic.SaveAs(path);

                                //if these are all successful then we can set these fields
                              //  haspic = true;
                                //picextension = extension;

                                //Update the member haspic and picextension fields in the database
                                //Member SelectedMember = db.Members.Find(id);
                                //SelectedMember.MemberHasPic = haspic;
                                //SelectedMember.Picture = extension;
                                //db.Entry(SelectedMember).State = EntityState.Modified;

 //                                db.SaveChanges();

   //                         }
     //                       catch (Exception ex)
       //                     {
         //                       Debug.WriteLine("Member Image was not saved successfully.");
           //                     Debug.WriteLine("Exception:" + ex);
             //               }
               //         }
                 //   }

               // }
           // }

           // return Ok();
//        }




        // DELETE: api/DepartmentData/5
        [HttpPost]
        public IHttpActionResult DeleteDepartment(int id)
        {
            Department department = db.departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            db.departments.Remove(department);
            db.SaveChanges();

            return Ok(department);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartmentExists(int id)
        {
            return db.departments.Count(e => e.departmentId == id) > 0;
        }
    }
}
