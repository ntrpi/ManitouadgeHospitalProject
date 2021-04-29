// This class represents the product data retrieved from the database,
// as well as the id of its row in the table.
class ProductData
{
    // This constructor is expecting an object constructed from the
    // response of a call to the ProductsDataController GetProduct( id )
    // function.
    constructor( data )
    {
        this.quantity = 0;
        if( !data ) {
            this.productId = 0;
            this.productName = "";
            this.price = 0;
            this.taxRate = 0;
        } else {
            this.productId = data.productId;
            this.productName = data.productName;
            this.price = data.price;
            this.taxRate = data.taxRate;
        }
    }

    // Syntactic sugar.
    addOneToQuantity()
    {
        this.quantity++;
    }

    // Syntactic sugar.
    getRowId()
    {
        return "trId" + this.productId;
    }

    // Syntactic sugar.
    getName()
    {
        return this.productName;
    }

    // Syntactic sugar.
    getQuantity()
    {
        return this.quantity;
    }

    // Syntactic sugar.
    getCost()
    {
        return this.quantity * this.price;
    }

    // Syntactic sugar.
    getTaxes()
    {
        return this.getCost() * this.taxRate;
    }
}

// Create a formatter that will display currency properly.
var currencyFormatter = new Intl.NumberFormat( 'en-US', {
    style: 'currency',
    currency: 'USD',
} );

// Use the above formatter to create a currency string for the given amount.
function getCurrencyString( amount )
{
    let currencyString = currencyFormatter.format( amount );
    return currencyString;
}

// Add or update a row in the table that displays the products
// with the information for the product with the given ID.
function addProductToTable( productId )
{
    // Get the product data that was stored after the GET call.
    let productData = selectedProducts[ productId ];

    // Increase the quantity of the product for the invoice.
    productData.quantity++;

    // Check if the product has already been added or not.
    if( productData.quantity == 1 ) {

        // If it's new, add a new row to the table.
        addProductRow( productData );
    } else {

        // Update the existing row.
        updateProductRow( productData );
    }

    // Update the rows of totals and taxes at the bottom of the table.
    updateTotalsRows( true, productData );
}

// Reduce the quantity and possibly
// remove the product with the given ID from the table.
function removeProductFromTable( productId )
{
    // Get the product data that was stored after the GET call.
    let productData = selectedProducts[ productId ];

    // Decrease the quantity.
    productData.quantity--;

    // Check if there are any left.
    if( productData.quantity == 0 ) {

        // If the quantity is 0, remove the product from the table.
        removeProductRow( productData );

        // Delete the product data.
        delete selectedProducts[ productId ];

    } else {
        // Update the quantity.
        updateProductRow( productData );
    }

    // Update the totals and taxes at the bottom of the table.
    updateTotalsRows( false, productData );
}

// Use these constants for consistency and code completion
// when entering data in the products table.
const nameCellIndex = 0;
const quantCellIndex = 1;
const costCellIndex = 2;
const removeCellIndex = 3;

// Generate the content of the cell with the product
// data for the cell with the given index. If the index
// does not correspond to the ones that display data,
// return an empty string.
function getCellInnerHtml( productData, index )
{ 
    // Determine which cell the request is for
    // and construct the content.
    innerHtml = "";
    switch( index ) {
        case nameCellIndex:
            innerHtml += `<td>${productData.getName()}</td>`;
            break;
        case quantCellIndex:
            innerHtml += `<td>${productData.getQuantity()}</td>`;
            break;
        case costCellIndex:
            innerHtml += `<td>${getCurrencyString( productData.getCost() )}</td>`;
            break;
    }
    // Return the content.
    return innerHtml;
}

// Add a new row for the given product to the products table.
function addProductRow( productData )
{
    // Get a handle for the table body.
    let tBody = $( `#${productsTableId} > TBODY` )[ 0 ];

    // Get the number of rows already in the table.
    let numRows = productsTable.rows.length;

    // Insert the new row after all the products already there but
    // above the totals rows.
    let row = tBody.insertRow( numRows - numTotalsRows );

    // Set the row ID to correspond with the product ID so
    // we can find it later to adjust or remove it.
    let rowId = productData.getRowId();
    row.id = rowId;

    // Create the product information cells and update the content.
    let nameCell = row.insertCell( nameCellIndex );
    nameCell.innerHTML = getCellInnerHtml( productData, nameCellIndex );

    let quantCell = row.insertCell( quantCellIndex );
    quantCell.innerHTML = getCellInnerHtml( productData, quantCellIndex );

    let costCell = row.insertCell( costCellIndex );
    costCell.innerHTML = getCellInnerHtml( productData, costCellIndex );

    // Create a button that allows the user to reduce the quantity
    // or remove the product row from the table.
    let removeButton = document.createElement( "button" );
    removeButton.classList.add( "btn" );
    removeButton.classList.add( "btn-light" );
    removeButton.innerHTML = "Remove";

    // Set the event listener for the button.
    const productId = productData.productId;
    removeButton.addEventListener( "click", function ( e )
    {
        // The button is in its own little form, so don't let it submit.
        e.preventDefault();
        removeProductFromTable( productId );
    } );

    // Add the button to the cell.
    let removeCell = row.insertCell( removeCellIndex );
    removeCell.appendChild( removeButton );
}

// Update the quantity in the table for the given product.
function updateProductRow( productData )
{
    let rowId = productData.getRowId();
    let row = document.getElementById( rowId );
    let quantCell = row.cells[ quantCellIndex ];
    quantCell.innerHTML = getCellInnerHtml( productData, quantCellIndex );
    let costCell = row.cells[ costCellIndex ];
    costCell.innerHTML = getCellInnerHtml( productData, costCellIndex );
}

// Remove the row in the table for the given product.
function removeProductRow( productData )
{
    let rowId = productData.getRowId();
    let row = document.getElementById( rowId );
    let rowIndex = row.rowIndex;
    let table = document.getElementById( productsTableId );
    table.deleteRow( rowIndex );
}

// Generate a string that contains the html for a row that shows
// a total.
// name: either subtotal, taxes, or total
// value: the amount of the total for that row
function getTotalsRow( name, value )
{
    let row = `<td></td><td><strong>${name}</strong></td><td>${getCurrencyString( value )}</td><td></td>`;
    return row;
}

// If the products list or quantity of a product has changed,
// update the totals rows accordingly.
// isAdd: true if the quantity is increasing
// productData: data for the product.
function updateTotalsRows( isAdd, productData )
{
    // Calculate the change in the subtotal for the table.
    let multiplier = isAdd ? 1 : -1;
    let cost = productData.price * multiplier;
    tableSubTotal += cost;

    // If the subtotal is 0, remove all the totals' contents
    // rather than displaying 0.
    if( tableSubTotal == 0 ) {
        tableTaxes = 0;
        tableTotal = 0;
        subTotalsRow.innerHTML = "";
        taxesRow.innerHTML = "";
        totalRow.innerHTML = "";

    } else {

        // Calculate the taxes and the overall total.
        let taxes = cost * productData.taxRate * multiplier;
        tableTaxes += taxes;
        tableTotal += cost + taxes;

        // Update the contents of the totals rows.
        subTotalsRow.innerHTML = getTotalsRow( "Subtotal:", tableSubTotal );
        taxesRow.innerHTML = getTotalsRow( "Tax:", tableTaxes );
        totalRow.innerHTML = getTotalsRow( "Total:", tableTotal );
    }
}


// Use the selected value as the product ID to either
// use previously retrieved information about a product
// or get the product information from the database; then
// update the products table.
var productsSelectOnChange = function () 
{
    // Get the selected value. The value should be the ID of the
    // product. A value of 0 indicates that no product was selected.
    let value = productsSelect.value;
    if( value == 0 ) {
        return;
    }

    // If the information for this product has not been retrieved before,
    // use a GET call to get the information from the database.
    if( !selectedProducts.hasOwnProperty( value ) ) {
        $.getJSON(
            "/api/ProductsData/GetProduct/" + value,
            function ( data )
            {
                // Put the product information in the selectedProducts object
                // and call the function to add this to the table.
                selectedProducts[ data.productId ] = new ProductData( data );
                addProductToTable( data.productId );
            }
        );
    } else {
        // Call the function to add the product to the table.
        addProductToTable( value );
    }

    // Reset the value to 0.
    productsSelect.value = 0;
    return 0;
}

// Output the success of the ajax call to add the product to the invoice.
// Mostly for debugging.
function outputProductAddSuccess( data )
{
    let msg = "product " + data.productId + " added to invoice " + data.invoiceId;
    console.log( msg );
}

// This function performs an ajax call to add a product and its 
// quanity to an invoice in the database.
// productId: the primary key for the product
// invoiceId: the primary key for the invoice
// quantity: the number of this product included in the invoice
// success: the function to call upon success of the ajax call
// done: the function to call upon successful completion of the ajax call
// fail: the function to call upon completion of a failed ajax call
// always: the function to call upon completion of the ajax call 
//          whether or not it was successful.
function addProductsToInvoiceAjax( productId, invoiceId, quantity, success, done, fail, always )
{
    // Create the invoice and get the ID back.
    $.ajax( {
        type: "POST",
        url: "/api/ProductXInvoicesData/CreateProductXInvoice",
        contentType: "application/json",
        data: JSON.stringify( {
            "productId": productId,
            "invoiceId": invoiceId,
            "quantity": quantity
        } ),
        success: success
    } )
        // If successful, add the products to the invoice.
        .done( done )
        // If unsuccessful, do something else.
        .fail( fail )
        // Do this whether or not the call was successful.
        .always( always );
}

// Call this function after the invoice has been created and has an ID.
function addProductsToInvoice( invoiceId )
{
    // Iterate over the products that were added to the invoice.
    for( const productId in selectedProducts ) {

        // For each product, retrieve its data and call the function
        // to do the ajax call.
        let productData = selectedProducts[ productId ];
        addProductsToInvoiceAjax( productId, invoiceId, productData.quantity, outputProductAddSuccess );
    }

    // This is intended to wait until all the ajax calls have 
    // completed before redirecting to the details view of the
    // invoice so that when the details load, they are complete.
    $( document ).ajaxStop( function ()
    {
        window.location.href = "/Invoices/Details/" + invoiceId;
    } );
}

// Call this function when the form to create the invoice is submitted.
// TODO: refactor ajax calls.
function createInvoice( created, userId )
{
    // Create the invoice and get the ID back.
    // If successful, add the products to the invoice in the
    // bridging table.
    $.ajax( {
        type: "POST",
        url: "/api/InvoicesData/CreateInvoice",
        contentType: "application/json",
        data: JSON.stringify( {
            "userId": userId,
            "created": created,
            "products": []
        } ),
        success: function ( data ) { addProductsToInvoice( data.invoiceId ) },
    } )
    // If successful, add the products to the invoice.
    .done( function ( data ) {
        console.log( "response: " + data.invoiceId );
    } )
    // If unsuccessful, do something else.
    .fail( function ( jqxhr, status, error ) {
        console.log( "error :" + error );
    } )
    // Do this whether or not the call was successful.
    .always( function () {
        console.log( "complete" );
    } );
}

// Store product information in ProductData objects
// referenced by the productId.
var selectedProducts = {};

// The table element listing the products.
const productsTableId = "productsTable";
const productsTableHeadings = ( "Name", "Quantity", "Cost", "" );
var productsTable;

// Some variables to hold totals for the invoice.
var tableSubTotal = 0;
var tableTaxes = 0;
var tableTotal = 0;
var subTotalsRow;
var taxesRow;
var totalRow;
const numTotalsRows = 3;


// The dropdown for product selection.
const productsSelectId = "productsSelect";
var productsSelect;

// The invoice ID if this is an update.
var invoiceId;

window.onload = function ()
{
    // For invoice products .
    let invoiceForm = document.forms.invoiceForm;
    if( invoiceForm ) {

        // Get the handle for the table for the products.
        productsTable = document.getElementById( "productsTable" );

        // Get a handle for the body of the table.
        let tBody = $( "#productsTable > TBODY" )[ 0 ];

        // If we have an invoice ID the products table will have already been initialized.
        let invoiceId = document.getElementById( "invoiceId" );
        if( !invoiceId ) {

            // Insert rows for the totals.
            subTotalsRow = tBody.insertRow();
            taxesRow = tBody.insertRow();
            totalRow = tBody.insertRow();

        } else {
            invoiceId = parseInt( invoiceId.innerHTML );

            let numRows = tBody.rows.length - 3;
            for( let i = 1; i < numRows; i++ ) {
                let row = tBody.rows[ i ];
                const productId = row.id.substring( 4 );
                const quantity = parseInt( row.cells[ quantCellIndex ].innerHTML );
                $.getJSON(
                    "/api/ProductsData/GetProduct/" + productId,
                    function ( data )
                    {
                        // Put the product information in the selectedProducts object
                        // and call the function to add this to the table.
                        let productData = new ProductData( data );
                        productData.quantity = quantity;
                        selectedProducts[ data.productId ] = productData;
                    }
                );

                // Set the event listener for the remove button.
                let removeButton = document.getElementById( "removeId" + productId );
                removeButton.addEventListener( "click", function ( e )
                {
                    // The button is in its own little form, so don't let it submit.
                    e.preventDefault();
                    removeProductFromTable( productId );
                } );

            }
            subTotalsRow = document.getElementById( "subtotal" );
            tableSubTotal = subTotalsRow.cells[ costCellIndex ];
            taxesRow = document.getElementById( "taxes" );
            tableTaxes = taxesRow.cells[ costCellIndex ];
            totalRow = document.getElementById( "total" );
            tableTotal = totalRow.cells[ costCellIndex ];
        }

        // Get a handle for the products dropdown and set an click
        // event handler.
        productsSelect = document.getElementById( "productsSelect" );
        productsSelect.onclick = productsSelectOnChange;

        // Set a submit event handler.
        invoiceForm.addEventListener( "submit", function ( e )
        {
            // Prevent the submission by the browser, because
            // we are going to handle it with ajax.
            e.preventDefault();

            // This is a handy way of making sure the whole form is valid.
            if( !$( "#invoiceForm" ).valid() ) {
                return false;
            }

            if( !invoiceId ) {
                // Get and validate the user ID, as razor is not handling
                // this the way I would like.
                const userId = invoiceForm.userId.value;
                if( userId == "" || userId == 0 ) {

                    // If it is not valid, indicate the error and return false.
                    let userIdError = document.getElementById( "userIdError" );
                    userIdError.classList.remove( "field-validation-valid" );
                    userIdError.innerHTML = "Please select a client.";
                    return false;
                }

                // Call the method to create the invoice.
                const created = invoiceForm.created.value;
                createInvoice( created, userId );
                return true;
            }

            addProductsToInvoice( invoiceId.value );
        } );    
    }
}