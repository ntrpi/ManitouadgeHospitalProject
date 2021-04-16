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
    public class JobPostingDataController : ApiController
    {
        private ManitouageDbContext db = new ManitouageDbContext();

        // GET: api/JobPostingData/GetJobPostings
        [ResponseType(typeof(IEnumerable<JobPostingDto>))]
        public IHttpActionResult GetJobPostings()
        {
            List<JobPosting> JobPostings = db.jobPostings.ToList();
            List<JobPostingDto> JobPostingDtos = new List<JobPostingDto> { };

            Debug.WriteLine("Files Received");


            foreach (var JobPosting in JobPostings)
            {
                 Department Department = db.departments
                .Where(s => s.jobpostings.Any(m => m.departmentId == JobPosting.departmentId))
                .FirstOrDefault();
               // if not found, return 404 status code.
                 if (Department == null)
                 {
                   return NotFound();
                 }

                JobPostingDto NewJobPosting = new JobPostingDto
                {
                    jobPostingId = JobPosting.jobPostingId,
                    jobNumber = JobPosting.jobNumber,
                    jobTitle = JobPosting.jobTitle,
                    jobType = JobPosting.jobType,
                    jobDescription = JobPosting.jobDescription,
                    salary = JobPosting.salary,
                    closingDate = JobPosting.closingDate,
                    departmentId = Department.departmentId,
                    departmentName = Department.departmentName
                };

                JobPostingDtos.Add(NewJobPosting);
            }

            return Ok(JobPostingDtos);
        }


        // GET: api/JobPostinData/FindJobPosting/5
        [HttpGet]
        [ResponseType(typeof(JobPostingDto))]
        public IHttpActionResult FindJobPosting(int id)
        {
            JobPosting JobPosting = db.jobPostings.Find(id);
            if (JobPosting == null)
            {
                return NotFound();
            }

            Department Department = db.departments
                .Where(t => t.jobpostings.Any(p => p.jobPostingId == id))
                .FirstOrDefault();

            // if not found, return 404 status code.
            if (Department == null)
            {
                return NotFound();
            }

            JobPostingDto JobPostingDto = new JobPostingDto
            {
                jobPostingId = JobPosting.jobPostingId,
                jobNumber = JobPosting.jobNumber,
                jobTitle = JobPosting.jobTitle,
                jobType = JobPosting.jobType,
                jobDescription = JobPosting.jobDescription,
                salary = JobPosting.salary,
                closingDate = JobPosting.closingDate,
                departmentId = Department.departmentId,
                departmentName = Department.departmentName
            };


            //pass along data as 200 status code OK response
            return Ok(JobPostingDto);
        }


        // PUT: api/MemberData/FinddeDartmentForJobposting/5
        [HttpGet]
        [ResponseType(typeof(IEnumerable<DepartmentDto>))]
        public IHttpActionResult FindDepartmentForJobposting(int id)
        {
            Debug.WriteLine("id:" + id);
            Department Department = db.departments
                .Where(t => t.jobpostings.Any(p => p.jobPostingId == id))
                .FirstOrDefault();

            Debug.WriteLine(Department.departmentId);
            Debug.WriteLine(Department.departmentName);
            if (Department == null)
            {
                return NotFound();
            }

            DepartmentDto DepartmentDtos = new DepartmentDto
            {
                departmentId = Department.departmentId,
                departmentName = Department.departmentName

            };


            //pass along data as 200 status code OK response
            return Ok(DepartmentDtos);


           
        }


        /// POST: api/JobPostingData/AddJobPosting
        ///  FORM DATA: JobPosting JSON Object
        /// </example>
        [ResponseType(typeof(JobPosting))]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult AddJobPosting([FromBody] JobPosting jobposting)
        {
            //Will Validate according to data annotations specified on model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.jobPostings.Add(jobposting);
            db.SaveChanges();

            return Ok(jobposting.jobPostingId);
        }

        /// POST: api/JobPostingData/UpdateJobPosting/5

        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult UpdateJobPosting(int id, [FromBody] JobPosting jobPosting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobPosting.jobPostingId)
            {
                return BadRequest();
            }

            db.Entry(jobPosting).State = EntityState.Modified;

            try
            {
                db.SaveChanges();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobPostingExists(id))
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




        // DELETE: api/JobPostingData/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteJobPosting(int id)
        {
            JobPosting jobposting = db.jobPostings.Find(id);
            if (jobposting == null)
            {
                return NotFound();
            }

            db.jobPostings.Remove(jobposting);
            db.SaveChanges();

            return Ok(jobposting);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobPostingExists(int id)
        {
            return db.jobPostings.Count(e => e.jobPostingId == id) > 0;
        }
    }
}
