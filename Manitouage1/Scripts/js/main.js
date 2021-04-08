function processGetProductResult( data, productQuantities ) {

    let name = data.productName;
    let price = data.price;
    if( productQuantities.hasOwnProperty( name ) ) {

    } else {

    }
}


window.onload = function ()
{
    // For invoice create.
    let productQuantities = {};
    productsTable = document.getElementById( "productsTable" );
    productsSelect = document.getElementById( "productsSelect" );
    productsSelect.onchange = function () {
        var value = productsSelect.value;
        if( value == 0 ) {
            return;
        }
        $.getJSON(
            "/api/ProductsData/GetProduct/" + value,
            function ( data ) {
                var tBody = $( "#productsTable > TBODY" )[ 0 ];
                var row = tBody.insertRow();
                var price = ( data.price ).toLocaleString( 'en-US', {
                    style: 'currency',
                    currency: 'USD',
                } );
                row.innerHTML = "<td>" + data.productName + "</td><td>" + price + "</td>";
                alert( data.productName );
            }
        );
    }
}