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
    public class ProductXInvoicesController: Controller
    {
        public const string helperName = "ProductXInvoice";

        private static readonly ControllersHelper helper;

        static ProductXInvoicesController()
        {
            helper = new ControllersHelper( "ProductXInvoice" );
        }

        // A utility function to save a few characters when using the ControllersHelper to construct a url.
        private string getUrl( string action, int id = 0 )
        {
            return helper.getUrl( action, id );
        }

        /// <summary>
        /// Get and display a list of all the ProductXInvoices in the database.
        /// </summary>
        /// <returns>A View containing a List of ProductXInvoiceDto objects.</returns>
        /// <example>
        /// GET: ProductXInvoices
        /// </example>
        public ActionResult Index()
        {
            HttpResponseMessage response = helper.doGetRequest( getUrl( "Get" ) + "s" );
            if( !response.IsSuccessStatusCode ) {
                return View();
            }

            return View( helper.getFromResponse<IEnumerable<ProductXInvoiceDto>>( response ) );
        }

        // A utility function that facilitates the call to the helper to retrieve a ProductXInvoice from the database.
        private ProductXInvoiceDto getProductXInvoiceDto( int productXInvoiceId )
        {
            HttpResponseMessage response = helper.doGetRequest( getUrl( "Get", productXInvoiceId ) );
            if( !response.IsSuccessStatusCode ) {
                return null;
            }
            return helper.getFromResponse<ProductXInvoiceDto>( response );
        }

        /// <summary>
        /// Get and display the information for the ProductXInvoice with the given ID.
        /// </summary>
        /// <param name="id">The ID of the ProductXInvoice in the database to retrieve.</param>
        /// <returns>A View containing a ProductXInvoiceDto object if it was successfully retrieved, otherwise it contains null.</returns>
        /// <example>
        /// GET: ProductXInvoices/Details/5
        /// </example>
        public ActionResult Details( int id )
        {
            return View( getProductXInvoiceDto( id ) );
        }

        /// <summary>
        /// Display the form to create a new ProductXInvoice.
        /// </summary>
        /// <returns>A View with the relevant input fields to create a new ProductXInvoice.</returns>
        /// <example>
        /// GET: ProductXInvoices/Create
        /// </example>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create a new record in the database with the information contained in the ProductXInvoice object.
        /// </summary>
        /// <param name="productXInvoice">A ProductXInvoice object, received as form data.</param>
        /// <returns>If the new ProductXInvoice was created, a RedirectToAction pointing to Details and containing the new ProductXInvoice's ID is returned, otherwise, a View with an error message in the ViewBag.</returns>
        /// <example>
        /// POST: ProductXInvoices/Create
        /// Form Data: ProductXInvoice
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ProductXInvoice productXInvoice )
        {
            HttpResponseMessage response = helper.doPostRequest( getUrl( "Create" ), productXInvoice );
            if( !response.IsSuccessStatusCode ) {
                ViewBag.errorMessage = "Unable to add productXInvoice.";
                return View();
            }

            ProductXInvoiceDto productXInvoiceDto = helper.getFromResponse<ProductXInvoiceDto>( response );
            return RedirectToAction( "Details", new {
                id = productXInvoiceDto.id
            } );
        }

        /// <summary>
        /// Display the form to make changes to a ProductXInvoice.
        /// </summary>
        /// <param name="id">The ID of the ProductXInvoice to update.</param>
        /// <returns>A View with the relevant input fields to edit a ProductXInvoice.</returns>
        /// <example>
        /// GET: ProductXInvoices/Edit/5
        /// </example>
        public ActionResult Edit( int id )
        {
            return View( getProductXInvoiceDto( id ) );
        }

        /// <summary>
        /// Update the record in the database with the information contained in the ProductXInvoice object.
        /// </summary>
        /// <param name="id">The ID of the productXInvoice to update.</param>
        /// <param name="productXInvoice">A ProductXInvoice object, received as form data.</param>
        /// <returns>If the new ProductXInvoice was created, a RedirectToAction pointing to Details and containing the new ProductXInvoice's ID is returned, otherwise, a View with an error message in the ViewBag.</returns>
        /// <example>
        /// POST: ProductXInvoices/Create
        /// Form Data: ProductXInvoice
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( int id, ProductXInvoice productXInvoice )
        {
            HttpResponseMessage response = helper.doPostRequest( getUrl( "Update", id ), productXInvoice );
            if( !response.IsSuccessStatusCode ) {
                ViewBag.errorMessage = "Unable to add productXInvoice.";
                return View();
            }

            return RedirectToAction( "Details", new {
                id = id
            } );
        }

        /// <summary>
        /// Display a view that shows the details of the ProductXInvoice and asks the user to confirm its deletion.
        /// </summary>
        /// <param name="id">The ID of the ProductXInvoice to delete.</param>
        /// <returns>A View containing a ProductXInvoiceDto if retrieved successfully from the database, otherwise it is empty.</returns>
        /// <example>
        /// GET: ProductXInvoices/DeleteConfirm/5
        /// </example>
        [HttpGet]
        public ActionResult DeleteConfirm( int id )
        {
            return View( getProductXInvoiceDto( id ) );
        }

        /// <summary>
        /// Delete the record in the database with the given ID.
        /// </summary>
        /// <param name="id">The ID of the productXInvoice to update.</param>
        /// <param name="collection">A collection of properties passed in from the caller.</param>
        /// <returns>An ActionResult that Redirects to Index.</returns>
        /// <example>
        /// POST: ProductXInvoices/Create
        /// Form Data: ProductXInvoice
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
