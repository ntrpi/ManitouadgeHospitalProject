// Created by Sandra Kupfer 2021/03

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Manitouage1.Models;

namespace Manitouage1.Controllers
{
    public class InvoicesDataController: ApiController
    {
        private ManitouageDbContext db = new ManitouageDbContext();

        /// <summary>
        /// Get a List of InvoiceDto objects constructed from the database.
        /// </summary>
        /// <returns>A List of InvoiceDto objects.</returns>
        /// <example>
        /// GET: api/InvoicesData/GetInvoices
        /// </example>
        [HttpGet]
        public IEnumerable<InvoiceDto> GetInvoices()
        {
            List<Invoice> invoices = db.invoices.ToList<Invoice>();
            List<InvoiceDto> dtos = new List<InvoiceDto>();
            foreach( Invoice invoice in invoices ) {
                dtos.Add( invoice.getDto() );
            }
            return dtos;
        }

        [HttpGet]
        [ResponseType( typeof( IEnumerable<ApplicationUser> ) )]
        public IHttpActionResult GetUsers()
        {
            return Ok( db.Users.ToList<ApplicationUser>() );
        }

        [HttpGet]
        [ResponseType( typeof( ApplicationUser ) )]
        public IHttpActionResult GetUser( string id )
        {
            return Ok( db.Users.Find( id ) );
        }

        /// <summary>
        /// Get a InvoiceDto object constructed from the record in the database with the given ID.
        /// </summary>
        /// <param name="id">The ID of the requested Invoice.</param>
        /// <returns>An IHttpActionResult containing a InvoiceDto object, or a NotFound IHttpActionResult.</returns>
        /// <example>
        /// GET: api/InvoicesData/GetInvoice/5
        /// </example>
        [HttpGet]
        [ResponseType( typeof( InvoiceDto ) )]
        public IHttpActionResult GetInvoice( int id )
        {
            Invoice invoice = db.invoices.Find( id );
            if( invoice == null ) {
                return NotFound();
            }

            return Ok( invoice.getDto() );
        }

        /// <summary>
        /// Update the record in the database with the information contained in the Invoice object.
        /// </summary>
        /// <param name="id">The ID of the Invoice to update.</param>
        /// <param name="invoice">A Invoice object, received as POST data.</param>
        /// <returns>An IHttpActionResult with no content if the update was successful, a BadRequest IHttpActionResult if there is a problem with the ModelState or Invoice ID, or a NotFound IHttpActionResult if the update was not successful.</returns>
        /// <example>
        /// POST: api/InvoicesData/UpdateInvoice/5
        /// FORM DATA: Invoice JSON Object
        /// </example>
        [ResponseType( typeof( void ) )]
        [HttpPost]
        public IHttpActionResult UpdateInvoice( int id, Invoice invoice )
        {
            if( !ModelState.IsValid ) {
                return BadRequest( ModelState );
            }

            if( id != invoice.invoiceId ) {
                return BadRequest();
            }

            db.Entry( invoice ).State = EntityState.Modified;

            try {
                db.SaveChanges();
            } catch( DbUpdateConcurrencyException ) {
                if( !InvoiceExists( id ) ) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return StatusCode( HttpStatusCode.NoContent );
        }

        /// <summary>
        /// Create a new record in the database with the information contained in the Invoice object.
        /// </summary>
        /// <param name="invoice">A Invoice object, received as POST data.</param>
        /// <returns>If the record was added a CreatedAtRoute IHttpActionResult containing the new record's ID is returned, otherwise, if the ModelState is invalid, a BadRequest IHttpActionResult is returned. 
        /// Otherwise, a NotFound ActionResult is returned.</returns>
        /// <example>
        /// POST: api/InvoicesData/CreateInvoice
        /// FORM DATA: Invoice JSON Object
        /// </example>
        [ResponseType( typeof( int ) )]
        [HttpPost]
        public IHttpActionResult CreateInvoice( Invoice invoice )
        {
            if( !ModelState.IsValid ) {
                return BadRequest( ModelState );
            }

            db.invoices.Add( invoice );
            db.SaveChanges();

            return CreatedAtRoute( "DefaultApi", new {
                id = invoice.invoiceId
            }, invoice );
        }

        /// <summary>
        /// Delete the record in the database with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Invoice to delete.</param>
        /// <returns>If the record exists in the database, an Ok IHttpActionResult containing a Invoice object is returned, otherwise a NotFound IHttpActionResult is returned. 
        /// <example>
        /// POST: api/InvoicesData/DeleteInvoice/5
        /// </example>
        [ResponseType( typeof( Invoice ) )]
        [HttpPost]
        public IHttpActionResult DeleteInvoice( int id )
        {
            Invoice invoice = db.invoices.Find( id );
            if( invoice == null ) {
                return NotFound();
            }

            db.invoices.Remove( invoice );
            db.SaveChanges();

            return Ok( invoice );
        }

        // Auto-generated.
        protected override void Dispose( bool disposing )
        {
            if( disposing ) {
                db.Dispose();
            }
            base.Dispose( disposing );
        }

        // Auto-generated.
        private bool InvoiceExists( int id )
        {
            return db.invoices.Count( e => e.invoiceId == id ) > 0;
        }
    }
}