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

    addOneToQuantity()
    {
        this.quantity++;
    }

    getRowId()
    {
        return "trId" + this.productId;
    }

    getName()
    {
        return this.productName;
    }

    getQuantity()
    {
        return this.quantity;
    }

    getCost()
    {
        return this.quantity * this.price;
    }

    getTaxes()
    {
        return this.getCost() * this.taxRate;
    }
}

var currencyFormatter = new Intl.NumberFormat( 'en-US', {
    style: 'currency',
    currency: 'USD',
} );


function getCurrencyString( amount )
{
    let currencyString = currencyFormatter.format( amount );
    return currencyString;
}

function addProductToTable( productId )
{
    let productData = selectedProducts[ productId ];
    productData.quantity++;

    if( productData.quantity == 1 ) {
        addProductRow( productData );
    } else {
        updateProductRow( productData );
    }
    updateTotalsRows( true, productData );
}

function removeProductFromTable( productId )
{
    let productData = selectedProducts[ productId ];
    productData.quantity--;
    if( productData.quantity == 0 ) {
        removeProductRow( productData );
        delete selectedProducts[ productId ];
    } else {
        updateProductRow( productData );
    }
    updateTotalsRows( false, productData );
}

const nameCellIndex = 0;
const quantCellIndex = 1;
const costCellIndex = 2;
const removeCellIndex = 3;

function getCellInnerHtml( productData, index )
{ 
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
    return innerHtml;
}

function addProductRow( productData )
{
    let tBody = $( `#${productsTableId} > TBODY` )[ 0 ];
    let numRows = productsTable.rows.length;
    let row = tBody.insertRow( numRows - numTotalsRows );
    let rowId = productData.getRowId();
    row.id = rowId;

    let nameCell = row.insertCell( nameCellIndex );
    nameCell.innerHTML = getCellInnerHtml( productData, nameCellIndex );

    let quantCell = row.insertCell( quantCellIndex );
    quantCell.innerHTML = getCellInnerHtml( productData, quantCellIndex );

    let costCell = row.insertCell( costCellIndex );
    costCell.innerHTML = getCellInnerHtml( productData, costCellIndex );

    let removeButton = document.createElement( "button" );
    removeButton.classList.add( "btn" );
    removeButton.classList.add( "btn-light" );
    removeButton.innerHTML = "Remove";

    const productId = productData.productId;
    removeButton.addEventListener( "click", function ( e )
    {
        e.preventDefault();
        removeProductFromTable( productId );
    } );
    let removeCell = row.insertCell( removeCellIndex );
    removeCell.appendChild( removeButton );
}

function updateProductRow( productData )
{
    let rowId = productData.getRowId();
    let row = document.getElementById( rowId );
    let quantCell = row.cells[ quantCellIndex ];
    quantCell.innerHTML = getCellInnerHtml( productData, quantCellIndex );
    let costCell = row.cells[ costCellIndex ];
    costCell.innerHTML = getCellInnerHtml( productData, costCellIndex );
}

function removeProductRow( productData )
{
    let rowId = productData.getRowId();
    let row = document.getElementById( rowId );
    let rowIndex = row.rowIndex;
    let table = document.getElementById( productsTableId );
    table.deleteRow( rowIndex );
}

function getTotalsRow( name, value )
{
    let row = `<td></td><td><strong>${name}</strong></td><td>${getCurrencyString( value )}</td><td></td>`;
    return row;
}

function updateTotalsRows( isAdd, productData )
{
    let multiplier = isAdd ? 1 : -1;
    let cost = productData.price * multiplier;
    tableSubTotal += cost;

    if( tableSubTotal == 0 ) {
        tableTaxes = 0;
        tableTotal = 0;
        subTotalsRow.innerHTML = "";
        taxesRow.innerHTML = "";
        totalRow.innerHTML = "";

    } else {
        let taxes = cost * productData.taxRate * multiplier;
        tableTaxes += taxes;
        tableTotal += cost + taxes;

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
    productsSelect.value = 0;
    return 0;
}

function outputProductAddSuccess( data )
{
    let msg = "product " + data.productId + " added to invoice " + data.invoiceId;
    console.log( msg );
}

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
    for( const productId in selectedProducts ) {
        let productData = selectedProducts[ productId ];
        addProductsToInvoiceAjax( productId, invoiceId, productData.quantity, outputProductAddSuccess );
    }
    $( document ).ajaxStop( function ()
    {
        window.location.href = "/Invoices/Details/" + invoiceId;
    } );
}

// Call this function when the form to create the invoice is submitted.
function createInvoice( created, userId )
{
    // Create the invoice and get the ID back.
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
var numTotalsRows = 3;


// The dropdown for product selection.
const productsSelectId = "productsSelect";
var productsSelect;

window.onload = function ()
{
    // For invoices Create view.
    productsTable = document.getElementById( "productsTable" );
    if( productsTable !== undefined ) {
        let tBody = $( "#productsTable > TBODY" )[ 0 ];
        subTotalsRow = tBody.insertRow();
        taxesRow = tBody.insertRow();
        totalRow = tBody.insertRow();
        productsSelect = document.getElementById( "productsSelect" );
        productsSelect.onclick = productsSelectOnChange;

        let invoiceCreateForm = document.forms.invoiceCreateForm;
        invoiceCreateForm.addEventListener( "submit", function ( e )
        {
            e.preventDefault();

            if( !$( "#invoiceCreateForm" ).valid() ) {
                return false;
            }

            const userId = invoiceCreateForm.userId.value;

            if( userId == "" || userId == 0 ) {
                let userIdError = document.getElementById( "userIdError" );
                userIdError.classList.remove( "field-validation-valid" );
                userIdError.innerHTML = "Please select a client.";
                return false;
            }

            const created = invoiceCreateForm.created.value;
            createInvoice( created, userId );
        } );    
    }
}