// Created by Sandra Kupfer 2021/03

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Manitouage1.Models;
using Manitouage1.Models.ViewModels;

namespace Manitouage1.Controllers
{
    public class InvoicesController: Controller
    {
        public const string helperName = "Invoice";

        private static readonly ControllersHelper helper;
        private static readonly ControllersHelper productsHelper;
        private static readonly ControllersHelper xInvoiceHelper;

        static InvoicesController()
        {
            helper = new ControllersHelper( helperName );
            productsHelper = new ControllersHelper( ProductsController.helperName );
            xInvoiceHelper = new ControllersHelper( ProductXInvoicesController.helperName );
        }

        // A utility function to save a few characters when using the ControllersHelper to construct a url.
        private string getUrl( string action, int id = 0 )
        {
            return helper.getUrl( action, id );
        }

        // A utility function that facilitates the call to the helper to retrieve an Invoice from the database.
        private InvoiceDto getInvoiceDto( int invoiceId )
        {
            HttpResponseMessage response = helper.doGetRequest( getUrl( "Get", invoiceId ) );
            if( !response.IsSuccessStatusCode ) {
                return null;
            }
            return helper.getFromResponse<InvoiceDto>( response );
        }

        // A utility function that facilitates the call to the helper to update an Invoice in the database.
        private bool updateInvoice( Invoice invoice )
        {
            // Send the post request.
            HttpResponseMessage response = helper.doPostRequest( getUrl( "Update", invoice.invoiceId ), invoice );
            return response.IsSuccessStatusCode;
        }

        // A utility function that facilitates the call to the helper to retrieve a User from the database.
        private ApplicationUser getUser( string userId )
        {
            // Get the user.
            HttpResponseMessage response = helper.doGetRequest( "InvoicesData/GetUser/" + userId );
            if( response.IsSuccessStatusCode ) {
               return helper.getFromResponse<ApplicationUser>( response );
            }
            return null;
        }

        // A utility function that facilitates the call to the helper to retrieve Users from the database.
        private IEnumerable<ApplicationUser> getUsers()
        {
            return helper.doGetAndGetFromResponse<IEnumerable<ApplicationUser>>( "InvoicesData/GetUsers" );
        }

        // A utility function that facilitates the call to the helper to retrieve a Product from the database.
        private ProductDto getProductDto( int productId )
        {
            return productsHelper.doGetAndGetFromResponse<ProductDto>( productsHelper.getUrl( "Get", productId ) );
        }

        // A utility function that facilitates the call to the helper to retrieve a list of Products from the database.
        private IEnumerable<ProductDto> getProductDtos()
        {
            return productsHelper.doGetAndGetFromResponse<IEnumerable<ProductDto>>( productsHelper.getUrl( "Get", 0 ) + "s" );
        }

        private IEnumerable<ViewInvoiceProduct> getViewInvoiceProducts( IEnumerable<ProductXInvoiceDto> xInvoices )
        {
            // Get the view products.
            List<ViewInvoiceProduct> invoiceProducts = new List<ViewInvoiceProduct>();
            foreach( ProductXInvoiceDto xInvoice in xInvoices ) {
                ProductDto productDto = getProductDto( xInvoice.productId );
                invoiceProducts.Add( new ViewInvoiceProduct(
                    productDto,
                    xInvoice.quantity
                    ) );
            }
            return invoiceProducts;
        }

        // A utility function that facilitates the call to the helper to retrieve a list of ProductXInvoices from the database.
        private IEnumerable<ProductXInvoiceDto> getProductXInvoices( int invoiceId )
        {
            return xInvoiceHelper.doGetAndGetFromResponse<IEnumerable<ProductXInvoiceDto>>( xInvoiceHelper.getUrl( "Get", 0 ) + "s/" + invoiceId );
        }

        // A utility function that constructs a ViewInvoice object.
        private ViewInvoice getViewInvoice( int invoiceId, bool isGetProducts = false )
        {
            InvoiceDto invoiceDto = getInvoiceDto( invoiceId );
            ViewInvoice viewInvoice = new ViewInvoice { 
                invoiceDto = invoiceDto,
                applicationUser = getUser( invoiceDto.userId )
            };

            if( isGetProducts ) {
                // Get the ProductXInvoices for this invoice.
                IEnumerable<ProductXInvoiceDto> xInvoices = getProductXInvoices( invoiceId );

                // Get the ViewInvoiceProduct objects.
                viewInvoice.invoiceProducts = getViewInvoiceProducts( xInvoices );
                viewInvoice.totals = new ProductTotals( viewInvoice.invoiceProducts );
            }

            return viewInvoice;
        }

        // Utility function to create an UpdateInvoice object.
        private UpdateInvoice getUpdateInvoice( int invoiceId, InvoiceDto invoice = null )
        {
            // Get the list of products.
            IEnumerable<ProductDto> productDtos = getProductDtos();

            // Create the UpdateInvoice object with the users, products, and product totals.
            UpdateInvoice updateInvoice = new UpdateInvoice {
                productDtos = productDtos,
                applicationUsers = getUsers(),
                totals = new ProductTotals( productDtos )
            };

            // If the id is not 0, then this invoice alread exists, so set the dto and other properties.
            if( invoiceId != 0 ) {
                InvoiceDto invoiceDto = invoice == null ? getInvoiceDto( invoiceId ) : invoice;
                updateInvoice.invoiceDto = invoiceDto;
                if( invoiceDto.userId != null ) {
                    updateInvoice.user = getUser( invoiceDto.userId );
                }
                updateInvoice.invoiceProducts = getViewInvoiceProducts( getProductXInvoices( invoiceId ) );
            }
            return updateInvoice;
        }

        /// <summary>
        /// Get and display a list of all the Invoices in the database.
        /// </summary>
        /// <returns>A View containing a List of InvoiceDto objects.</returns>
        /// <example>
        /// GET: Invoices
        /// </example>
        public ActionResult Index()
        {
            HttpResponseMessage response = helper.doGetRequest( getUrl( "Get" ) + "s" );
            if( !response.IsSuccessStatusCode ) {
                return View();
            }

            return View( helper.getFromResponse<IEnumerable<InvoiceDto>>( response ) );
        }

        /// <summary>
        /// Get and display the information for the Invoice with the given ID.
        /// </summary>
        /// <param name="id">The ID of the Invoice in the database to retrieve.</param>
        /// <returns>A View containing a InvoiceDto object if it was successfully retrieved, otherwise it contains null.</returns>
        /// <example>
        /// GET: Invoices/Details/5
        /// </example>
        public ActionResult Details( int id )
        {
            return View( getViewInvoice( id, true ) );
        }

        /// <summary>
        /// Display the form to create a new Invoice.
        /// </summary>
        /// <returns>A View with the relevant input fields to create a new Invoice.</returns>
        /// <example>
        /// GET: Invoices/Create
        /// </example>
        public ActionResult Create()
        {
            return View( getUpdateInvoice( 0 ) );
        }

        /// <summary>
        /// Create a new record in the database with the information contained in the Invoice object.
        /// </summary>
        /// <param name="invoice">A Invoice object, received as form data.</param>
        /// <returns>If the new Invoice was created, a RedirectToAction pointing to Details and containing the new Invoice's ID is returned, otherwise, a View with an error message in the ViewBag.</returns>
        /// <example>
        /// POST: Invoices/Create
        /// Form Data: Invoice
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Invoice invoice )
        {
            // Set the default values for a new invoice.
            invoice.created = DateTime.Now;
            invoice.status = Invoice.Status.Created;

            // Send the post request.
            HttpResponseMessage response = helper.doPostRequest( getUrl( "Create" ), invoice );
            if( !response.IsSuccessStatusCode ) {
                ViewBag.errorMessage = "Unable to add invoice.";
                return View();
            }

            // TODO: just send the id instead of the whole dto.
            InvoiceDto invoiceDto = helper.getFromResponse<InvoiceDto>( response );
            return RedirectToAction( "Details", new {
                id = invoiceDto.invoiceId
            } );
        }

        /// <summary>
        /// Display the form to make changes to a Invoice.
        /// </summary>
        /// <param name="id">The ID of the Invoice to update.</param>
        /// <returns>A View with the relevant input fields to edit a Invoice.</returns>
        /// <example>
        /// GET: Invoices/Edit/5
        /// </example>
        public ActionResult Edit( int id )
        {
            InvoiceDto invoiceDto = getInvoiceDto( id );
            if( invoiceDto.status == Invoice.Status.Paid ) {
                return RedirectToAction( "Details", new {
                    id = invoiceDto.invoiceId
                } );
            }
            return View( getUpdateInvoice( id, invoiceDto ) );
        }

        /// <summary>
        /// Display the form to update the client for an invoice.
        /// </summary>
        /// <param name="id">The ID of the Invoice to update.</param>
        /// <returns>A View with the relevant input fields to edit a Invoice.</returns>
        /// <example>
        /// GET: Invoices/EditClient/5
        /// </example>
        public ActionResult EditClient( int id )
        {
            // TODO: find a way to refactor this.
            InvoiceDto invoiceDto = getInvoiceDto( id );
            if( invoiceDto.status == Invoice.Status.Paid ) {
                return RedirectToAction( "Details", new {
                    id = invoiceDto.invoiceId
                } );
            }
            return View( getUpdateInvoice( id, invoiceDto ) );
        }

        /// <summary>
        /// Update the record in the database with the information contained in the Invoice object.
        /// </summary>
        /// <param name="id">The ID of the invoice to update.</param>
        /// <param name="invoice">A Invoice object, received as form data.</param>
        /// <returns>If the update was successful, a RedirectToAction pointing to Details is returned, otherwise, a View with an error message in the ViewBag.</returns>
        /// <example>
        /// POST: Invoices/EditClient
        /// Form Data: Invoice
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClient( int id, Invoice invoice )
        {
            if( !updateInvoice( invoice ) ) {
                ViewBag.errorMessage = "Unable to update invoice.";
                return View();
            }

            return RedirectToAction( "Details", new {
                id = id
            } );
        }

        /// <summary>
        /// Display the form to update the status for an invoice.
        /// </summary>
        /// <param name="id">The ID of the Invoice to update.</param>
        /// <returns>A View with the relevant input fields to edit a Invoice.</returns>
        /// <example>
        /// GET: Invoices/EditStatus/5
        /// </example>
        public ActionResult EditStatus( int id )
        {
            // TODO: find a way to refactor this.
            InvoiceDto invoiceDto = getInvoiceDto( id );
            if( invoiceDto.status == Invoice.Status.Paid ) {
                return RedirectToAction( "Details", new {
                    id = invoiceDto.invoiceId
                } );
            }
            return View( getUpdateInvoice( id, invoiceDto ) );
        }

        /// <summary>
        /// Update the record in the database with the information contained in the Invoice object.
        /// </summary>
        /// <param name="id">The ID of the invoice to update.</param>
        /// <param name="invoice">A Invoice object, received as form data.</param>
        /// <returns>If the Invoice was updated, a RedirectToAction pointing to Details is returned, otherwise, a View with an error message in the ViewBag.</returns>
        /// <example>
        /// POST: Invoices/Edit
        /// Form Data: Invoice
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStatus( int id, Invoice invoice )
        {
            switch( invoice.status ) {
                case Invoice.Status.Created: {
                    invoice.status = Invoice.Status.Issued;
                    invoice.issued = DateTime.Now;
                    break;
                }
                case Invoice.Status.Issued: {
                    invoice.paid = DateTime.Now;
                    invoice.status = Invoice.Status.Paid;
                    break;
                }
            }

            if( !updateInvoice( invoice ) ) {
                ViewBag.errorMessage = "Unable to update invoice.";
                return View();
            }

            return RedirectToAction( "Details", new {
                id = id
            } );
        }

        /// <summary>
        /// Display the form to update the client for an invoice.
        /// </summary>
        /// <param name="id">The ID of the Invoice to update.</param>
        /// <returns>A View with the relevant input fields to edit a Invoice.</returns>
        /// <example>
        /// GET: Invoices/EditClient/5
        /// </example>
        public ActionResult EditProducts( int id )
        {
            // TODO: find a way to refactor this.
            InvoiceDto invoiceDto = getInvoiceDto( id );
            if( invoiceDto.status == Invoice.Status.Paid ) {
                return RedirectToAction( "Details", new {
                    id = invoiceDto.invoiceId
                } );
            }
            return View( getUpdateInvoice( id, invoiceDto ) );
        }



        /// <summary>
        /// Display a view that shows the details of the Invoice and asks the user to confirm its deletion.
        /// </summary>
        /// <param name="id">The ID of the Invoice to delete.</param>
        /// <returns>A View containing a InvoiceDto if retrieved successfully from the database, otherwise it is empty.</returns>
        /// <example>
        /// GET: Invoices/DeleteConfirm/5
        /// </example>
        [HttpGet]
        public ActionResult DeleteConfirm( int id )
        {
            return View( getViewInvoice( id ) );
        }

        /// <summary>
        /// Delete the record in the database with the given ID.
        /// </summary>
        /// <param name="id">The ID of the invoice to update.</param>
        /// <param name="collection">A collection of properties passed in from the caller.</param>
        /// <returns>An ActionResult that Redirects to Index.</returns>
        /// <example>
        /// POST: Invoices/Create
        /// Form Data: Invoice
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete( int id, FormCollection collection )
        {
            // Get the product IDs.
            IEnumerable<ProductXInvoiceDto> xInvoices = getProductXInvoices( id );             foreach( ProductXInvoiceDto product in xInvoices ) {
                string url = ControllersHelper.getUrl( "ProductXInvoice", "Delete", product.id );
                helper.doPostRequest( url, "" );
            }

            // TODO: do something with the response.
            helper.doPostRequest( getUrl( "Delete", id ), "" );
            return RedirectToAction( "Index" );
        }
    }
}
