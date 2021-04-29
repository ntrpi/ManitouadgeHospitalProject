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
using Manitouage1.Models;

namespace Manitouage1.Controllers
{
    public class ProductXInvoicesDataController: ApiController
    {
        private ManitouageDbContext db = new ManitouageDbContext();

        /// <summary>
        /// Get a List of ProductXInvoiceDto objects constructed from the database for a given Invoice ID.
        /// </summary>
        /// <param name="id">The ID of the Invoice for which to return the ProductXInvoiceDto objects.</param>
        /// <returns>A List of ProductXInvoiceDto objects.</returns>
        /// <example>
        /// GET: api/ProductXInvoicesData/GetProductXInvoicesForInvoice/5
        /// </example>
        [HttpGet]
        public IEnumerable<ProductXInvoiceDto> GetProductXInvoicesForInvoice( int id )
        {
            List<ProductXInvoice> productXInvoices = db.productXInvoices.Where( p => p.invoiceId == id ).ToList();
            return getDtos( productXInvoices );
        }

        // A utility function to get a List of ProductXInvoiceDto objects from ProductXInvoices.
        private IEnumerable<ProductXInvoiceDto> getDtos( IEnumerable<ProductXInvoice> productXInvoices )
        {
            List<ProductXInvoiceDto> dtos = new List<ProductXInvoiceDto>();
            foreach( ProductXInvoice productXInvoice in productXInvoices ) {
                dtos.Add( productXInvoice.getDto() );
            }
            return dtos;
        }

        /// <summary>
        /// Get a List of ProductXInvoiceDto objects constructed from the database for a given Invoice ID.
        /// </summary>
        /// <returns>A List of ProductXInvoiceDto objects.</returns>
        /// <example>
        /// GET: api/ProductXInvoicesData/GetProductXInvoices/5
        /// </example>
        [HttpGet]
        public IEnumerable<ProductXInvoiceDto> GetProductXInvoices( int id )
        {
            List<ProductXInvoice> productXInvoices = db.productXInvoices.Where( p => p.invoiceId == id ).ToList<ProductXInvoice>();
            return getDtos( productXInvoices );
        }

        /// <summary>
        /// Get a List of ProductXInvoiceDto objects constructed from the database.
        /// </summary>
        /// <returns>A List of ProductXInvoiceDto objects.</returns>
        /// <example>
        /// GET: api/ProductXInvoicesData/GetAllProductXInvoices
        /// </example>
        [HttpGet]
        public IEnumerable<ProductXInvoiceDto> GetAllProductXInvoices()
        {
            List<ProductXInvoice> productXInvoices = db.productXInvoices.ToList<ProductXInvoice>();
            return getDtos( productXInvoices );
        }

        /// <summary>
        /// Get a ProductXInvoiceDto object constructed from the record in the database with the given ID.
        /// </summary>
        /// <param name="id">The ID of the requested ProductXInvoice.</param>
        /// <returns>An IHttpActionResult containing a ProductXInvoiceDto object, or a NotFound IHttpActionResult.</returns>
        /// <example>
        /// GET: api/ProductXInvoicesData/GetProductXInvoice/5
        /// </example>
        [HttpGet]
        [ResponseType( typeof( ProductXInvoiceDto ) )]
        public IHttpActionResult GetProductXInvoice( int id )
        {
            ProductXInvoice productXInvoice = db.productXInvoices.Find( id );
            if( productXInvoice == null ) {
                return NotFound();
            }

            return Ok( productXInvoice.getDto() );
        }

        /// <summary>
        /// Update the record in the database with the information contained in the ProductXInvoice object.
        /// </summary>
        /// <param name="id">The ID of the ProductXInvoice to update.</param>
        /// <param name="productXInvoice">A ProductXInvoice object, received as POST data.</param>
        /// <returns>An IHttpActionResult with no content if the update was successful, a BadRequest IHttpActionResult if there is a problem with the ModelState or ProductXInvoice ID, or a NotFound IHttpActionResult if the update was not successful.</returns>
        /// <example>
        /// POST: api/ProductXInvoicesData/UpdateProductXInvoice/5
        /// FORM DATA: ProductXInvoice JSON Object
        /// </example>
        [ResponseType( typeof( void ) )]
        [HttpPost]
        public IHttpActionResult UpdateProductXInvoice( int id, ProductXInvoice productXInvoice )
        {
            if( !ModelState.IsValid ) {
                return BadRequest( ModelState );
            }

            if( id != productXInvoice.id ) {
                return BadRequest();
            }

            db.Entry( productXInvoice ).State = EntityState.Modified;

            try {
                db.SaveChanges();
            } catch( DbUpdateConcurrencyException ) {
                if( !ProductXInvoiceExists( id ) ) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return StatusCode( HttpStatusCode.NoContent );
        }

        /// <summary>
        /// Create a new record in the database with the information contained in the ProductXInvoice object.
        /// </summary>
        /// <param name="productXInvoice">A ProductXInvoice object, received as POST data.</param>
        /// <returns>If the record was added a CreatedAtRoute IHttpActionResult containing the new record's ID is returned, otherwise, if the ModelState is invalid, a BadRequest IHttpActionResult is returned. 
        /// Otherwise, a NotFound ActionResult is returned.</returns>
        /// <example>
        /// POST: api/ProductXInvoicesData/CreateProductXInvoice
        /// FORM DATA: ProductXInvoice JSON Object
        /// </example>
        [ResponseType( typeof( int ) )]
        [HttpPost]
        public IHttpActionResult CreateProductXInvoice( ProductXInvoice productXInvoice )
        {
            if( !ModelState.IsValid ) {
                return BadRequest( ModelState );
            }

            db.productXInvoices.Add( productXInvoice );
            db.SaveChanges();

            return CreatedAtRoute( "DefaultApi", new {
                id = productXInvoice.id
            }, productXInvoice );
        }

        /// <summary>
        /// Delete the record in the database with the given ID.
        /// </summary>
        /// <param name="id">The ID of the ProductXInvoice to delete.</param>
        /// <returns>If the record exists in the database, an Ok IHttpActionResult containing a ProductXInvoice object is returned, otherwise a NotFound IHttpActionResult is returned. 
        /// <example>
        /// POST: api/ProductXInvoicesData/DeleteProductXInvoice/5
        /// </example>
        [ResponseType( typeof( ProductXInvoice ) )]
        [HttpPost]
        public IHttpActionResult DeleteProductXInvoice( int id )
        {
            ProductXInvoice productXInvoice = db.productXInvoices.Find( id );
            if( productXInvoice == null ) {
                return NotFound();
            }

            db.productXInvoices.Remove( productXInvoice );
            db.SaveChanges();

            return Ok( productXInvoice );
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
        private bool ProductXInvoiceExists( int id )
        {
            return db.productXInvoices.Count( e => e.id == id ) > 0;
        }
    }
}