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

        public IEnumerable<ProductDto> Getproducts()
        {
            List<Product> products = db.products.ToList<Product>();
            List<ProductDto> dtos = new List<ProductDto>();
            foreach( Product product in products ) {
                dtos.Add( product.getDto() );
            }
            return dtos;
        }

        [ResponseType( typeof( Product ) )]
        public IHttpActionResult GetProduct( int id )
        {
            Product product = db.products.Find( id );
            if( product == null ) {
                return NotFound();
            }

            return Ok( product );
        }

        [ResponseType( typeof( void ) )]
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

        [ResponseType( typeof( Product ) )]
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

        [ResponseType( typeof( Product ) )]
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

        protected override void Dispose( bool disposing )
        {
            if( disposing ) {
                db.Dispose();
            }
            base.Dispose( disposing );
        }

        private bool ProductExists( int id )
        {
            return db.products.Count( e => e.productId == id ) > 0;
        }
    }
}