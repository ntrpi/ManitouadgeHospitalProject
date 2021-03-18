using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Manitouage1.Models;

namespace Manitouage1.Controllers
{
    public class ProductsController: Controller
    {
        private static readonly ControllersHelper helper;

        static ProductsController()
        {
            helper = new ControllersHelper( "Product" );
        }

        private string getUrl( string action, int id = 0 )
        {
            return helper.getUrl( action, id );
        }

        // GET: Products
        public ActionResult Index()
        {
            return View( helper.doGetRequest( getUrl( "Get" ) + "s" ) );
        }

        private ProductDto getProductDto( int productId )
        {
            HttpResponseMessage response = helper.doGetRequest( getUrl( "Get", productId ) );
            if( !response.IsSuccessStatusCode ) {
                return null;
            }
            return helper.getFromResponse<ProductDto>( response );
        }

        // GET: Products/Details/5
        public ActionResult Details( int id )
        {
            return View( getProductDto( id ) );
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Product product )
        {
            HttpResponseMessage response = helper.doPostRequest( getUrl( "Create" ), product );
            if( !response.IsSuccessStatusCode ) {
                ViewBag.errorMessage = "Unable to add product.";
                return View();
            }

            ProductDto productDto = helper.getFromResponse<ProductDto>( response );
            return RedirectToAction( "Details", new {
                id = productDto.productId
            } );
        }

        // GET: Products/Edit/5
        public ActionResult Edit( int id )
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( int id, Product product )
        {
            // Just in case.
            product.productId = id;

            HttpResponseMessage response = helper.doPostRequest( getUrl( "Update", id ), product );
            if( !response.IsSuccessStatusCode ) {
                ViewBag.errorMessage = "Unable to add product.";
                return View();
            }

            ProductDto productDto = helper.getFromResponse<ProductDto>( response );
            return RedirectToAction( "Details", new {
                id = productDto.productId
            } );
        }

        public ActionResult DeleteConfirm( int id )
        {
            return View( getProductDto( id ) );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete( int id, FormCollection collection )
        {
            // TODO: do something with the response.
            helper.doPostRequest( getUrl( "Delete", id ), "" );
            return RedirectToAction( "Index" );
        }
    }
}
