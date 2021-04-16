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
            IEnumerable<Department> Departments = db.departments.ToList();
            List<DepartmentDto> DepartmentDtos = new List<DepartmentDto> { };

            Debug.WriteLine("Files Received");

            
            foreach (var Department in Departments)
            {
               
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
            Debug.WriteLine("Get DepartmentDtos");
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



        // <example>
        // GET: api/DepartementData/FindDepartmentForJobposting/5
        // </example>
        [HttpGet]
        [ResponseType(typeof(Department))]
        public IHttpActionResult FindDepartmentForJobposting(int id)
        {

            Department Department = db.departments
                .Where(m => m.jobpostings.Any(p => p.jobPostingId == id))
                .FirstOrDefault();
            //if not found, return 404 status code.
            if (Department == null)
            {
                return NotFound();
            }

            DepartmentDto DepartmentDto = new DepartmentDto
            {
                departmentId = Department.departmentId,
                departmentName = Department.departmentName
               
            };


            //pass along data as 200 status code OK response
            return Ok(DepartmentDto);
        }




        /// POST: api/DepartmentData/AddDepartment
        ///  FORM DATA: Department JSON Object
        /// </example>
        [ResponseType(typeof(Department))]
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

      



        // DELETE: api/DepartmentData/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
