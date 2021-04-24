// Created by Sandra Kupfer 2021/03

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
        public const string helperName = "Product";

        private static readonly ControllersHelper helper;

        static ProductsController()
        {
            helper = new ControllersHelper( helperName );
        }

        // A utility function to save a few characters when using the ControllersHelper to construct a url.
        private string getUrl( string action, int id = 0 )
        {
            return helper.getUrl( action, id );
        }

        /// <summary>
        /// Get and display a list of all the Products in the database.
        /// </summary>
        /// <returns>A View containing a List of ProductDto objects.</returns>
        /// <example>
        /// GET: Products
        /// </example>
        public ActionResult Index()
        {
            HttpResponseMessage response = helper.doGetRequest( getUrl( "Get" ) + "s" );
            if( !response.IsSuccessStatusCode ) {
                return View();
            }

            return View( helper.getFromResponse<IEnumerable<ProductDto>>( response ) );
        }

        // A utility function that facilitates the call to the helper to retrieve a Product from the database.
        private ProductDto getProductDto( int productId )
        {
            HttpResponseMessage response = helper.doGetRequest( getUrl( "Get", productId ) );
            if( !response.IsSuccessStatusCode ) {
                return null;
            }
            return helper.getFromResponse<ProductDto>( response );
        }

        /// <summary>
        /// Get and display the information for the Product with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Product in the database to retrieve.</param>
        /// <returns>A View containing a ProductDto object if it was successfully retrieved, otherwise it contains null.</returns>
        /// <example>
        /// GET: Products/Details/5
        /// </example>
        public ActionResult Details( int id )
        {
            return View( getProductDto( id ) );
        }

    /// <summary>
        /// Display the form to create a new Product.
        /// </summary>
        /// <returns>A View with the relevant input fields to create a new Product.</returns>
        /// <example>
        /// GET: Products/Create
        /// </example>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create a new record in the database with the information contained in the Product object.
        /// </summary>
        /// <param name="product">A Product object, received as form data.</param>
        /// <returns>If the new Product was created, a RedirectToAction pointing to Details and containing the new Product's ID is returned, otherwise, a View with an error message in the ViewBag.</returns>
        /// <example>
        /// POST: Products/Create
        /// Form Data: Product
        /// </example>
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

        /// <summary>
        /// Display the form to make changes to a Product.
        /// </summary>
        /// <param name="id">The ID of the Product to update.</param>
        /// <returns>A View with the relevant input fields to edit a Product.</returns>
        /// <example>
        /// GET: Products/Edit/5
        /// </example>
        public ActionResult Edit( int id )
        {
            return View( getProductDto( id ) );
        }

        /// <summary>
        /// Update the record in the database with the information contained in the Product object.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="product">A Product object, received as form data.</param>
        /// <returns>If the new Product was created, a RedirectToAction pointing to Details and containing the new Product's ID is returned, otherwise, a View with an error message in the ViewBag.</returns>
        /// <example>
        /// POST: Products/Create
        /// Form Data: Product
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( int id, Product product )
        {
            HttpResponseMessage response = helper.doPostRequest( getUrl( "Update", id ), product );
            if( !response.IsSuccessStatusCode ) {
                ViewBag.errorMessage = "Unable to add product.";
                return View();
            }

            return RedirectToAction( "Details", new {
                id = id
            } );
        }

        /// <summary>
        /// Display a view that shows the details of the Product and asks the user to confirm its deletion.
        /// </summary>
        /// <param name="id">The ID of the Product to delete.</param>
        /// <returns>A View containing a ProductDto if retrieved successfully from the database, otherwise it is empty.</returns>
        /// <example>
        /// GET: Products/DeleteConfirm/5
        /// </example>
        [HttpGet]
        public ActionResult DeleteConfirm( int id )
        {
            return View( getProductDto( id ) );
        }

        /// <summary>
        /// Delete the record in the database with the given ID.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="collection">A collection of properties passed in from the caller.</param>
        /// <returns>An ActionResult that Redirects to Index.</returns>
        /// <example>
        /// POST: Products/Create
        /// Form Data: Product
        /// </example>
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
