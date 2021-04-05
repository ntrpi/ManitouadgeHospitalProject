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
    public class ProductsDataController: ApiController
    {
        private ManitouageDbContext db = new ManitouageDbContext();

        /// <summary>
        /// Get a List of ProductDto objects constructed from the database.
        /// </summary>
        /// <returns>A List of ProductDto objects.</returns>
        /// <example>
        /// GET: api/ProductsData/GetProducts
        /// </example>
        [HttpGet]
        public IEnumerable<ProductDto> GetProducts()
        {
            List<Product> products = db.products.ToList<Product>();
            List<ProductDto> dtos = new List<ProductDto>();
            foreach( Product product in products ) {
                dtos.Add( product.getDto() );
            }
            return dtos;
        }

        /// <summary>
        /// Get a ProductDto object constructed from the record in the database with the given ID.
        /// </summary>
        /// <param name="id">The ID of the requested Product.</param>
        /// <returns>An IHttpActionResult containing a ProductDto object, or a NotFound IHttpActionResult.</returns>
        /// <example>
        /// GET: api/ProductsData/GetProduct/5
        /// </example>
        [HttpGet]
        [ResponseType( typeof( ProductDto ) )]
        public IHttpActionResult GetProduct( int id )
        {
            Product product = db.products.Find( id );
            if( product == null ) {
                return NotFound();
            }

            return Ok( product.getDto() );
        }

        /// <summary>
        /// Update the record in the database with the information contained in the Product object.
        /// </summary>
        /// <param name="id">The ID of the Product to update.</param>
        /// <param name="product">A Product object, received as POST data.</param>
        /// <returns>An IHttpActionResult with no content if the update was successful, a BadRequest IHttpActionResult if there is a problem with the ModelState or Product ID, or a NotFound IHttpActionResult if the update was not successful.</returns>
        /// <example>
        /// POST: api/ProductsData/UpdateProduct/5
        /// FORM DATA: Product JSON Object
        /// </example>
        [ResponseType( typeof( void ) )]
        [HttpPost]
        public IHttpActionResult UpdateProduct( int id, Product product )
        {
            if( !ModelState.IsValid ) {
                return BadRequest( ModelState );
            }

            if( id != product.productId ) {
                return BadRequest();
            }

            db.Entry( product ).State = EntityState.Modified;

            try {
                db.SaveChanges();
            } catch( DbUpdateConcurrencyException ) {
                if( !ProductExists( id ) ) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return StatusCode( HttpStatusCode.NoContent );
        }

        /// <summary>
        /// Create a new record in the database with the information contained in the Product object.
        /// </summary>
        /// <param name="product">A Product object, received as POST data.</param>
        /// <returns>If the record was added a CreatedAtRoute IHttpActionResult containing the new record's ID is returned, otherwise, if the ModelState is invalid, a BadRequest IHttpActionResult is returned. 
        /// Otherwise, a NotFound ActionResult is returned.</returns>
        /// <example>
        /// POST: api/ProductsData/CreateProduct
        /// FORM DATA: Product JSON Object
        /// </example>
        [ResponseType( typeof( int ) )]
        [HttpPost]
        public IHttpActionResult CreateProduct( Product product )
        {
            if( !ModelState.IsValid ) {
                return BadRequest( ModelState );
            }

            db.products.Add( product );
            db.SaveChanges();

            return CreatedAtRoute( "DefaultApi", new {
                id = product.productId
            }, product );
        }

        /// <summary>
        /// Delete the record in the database with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Product to delete.</param>
        /// <returns>If the record exists in the database, an Ok IHttpActionResult containing a Product object is returned, otherwise a NotFound IHttpActionResult is returned. 
        /// <example>
        /// POST: api/ProductsData/DeleteProduct/5
        /// </example>
        [ResponseType( typeof( Product ) )]
        [HttpPost]
        public IHttpActionResult DeleteProduct( int id )
        {
            Product product = db.products.Find( id );
            if( product == null ) {
                return NotFound();
            }

            db.products.Remove( product );
            db.SaveChanges();

            return Ok( product );
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
        private bool ProductExists( int id )
        {
            return db.products.Count( e => e.productId == id ) > 0;
        }
    }
}