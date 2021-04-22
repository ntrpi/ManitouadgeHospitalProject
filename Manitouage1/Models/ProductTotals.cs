using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Manitouage1.Models.ViewModels;

namespace Manitouage1.Models
{
    public class ProductTotals
    {
        public ProductTotals()
        {
        }

        public ProductTotals( IEnumerable<ProductDto> products )
        {
            if( products == null ) {
                return;
            }
            subTotal = 0;
            taxes = 0;
            foreach( ProductDto product in products ) {
                addProduct( product );
            }
        }

        public ProductTotals( IEnumerable<Product> products )
        {
            if( products == null ) {
                return;
            }
            subTotal = 0;
            taxes = 0;
            foreach( Product product in products ) {
                addProduct( product );
            }
        }

        public ProductTotals( IEnumerable<ViewInvoiceProduct> products )
        {
            if( products == null ) {
                return;
            }
            subTotal = 0;
            taxes = 0;
            foreach( ViewInvoiceProduct product in products ) {
                addProduct( product.cost, product.taxRate );
            }
        }

        public void addProduct( Product product )
        {
            addProduct( product.price, product.taxRate );
        }

        public void addProduct( ProductDto product )
        {
            addProduct( product.price, product.taxRate );
        }

        public void addProduct( decimal price, decimal taxRate )
        {
            subTotal += price;
            taxes += price * taxRate;
            total = subTotal + taxes;
        }

        public void removeProduct( Product product )
        {
            removeProduct( product.price, product.taxRate );
        }

        public void removeProduct( ProductDto product )
        {
            removeProduct( product.price, product.taxRate );
        }

        public void removeProduct( decimal price, decimal taxRate )
        {
            if( total == 0 ) {
                return;
            }
            subTotal -= price;
            taxes -= price * taxRate;
            total = subTotal + taxes;
        }

        [DisplayName( "Subtotal:" )]
        [DataType( DataType.Currency )]

        public decimal subTotal {
            get; set;
        }

        [DisplayName( "Tax:" )]
        [DataType( DataType.Currency )]
        public decimal taxes {
            get; set;
        }

        [DisplayName( "Total" )]
        [DataType( DataType.Currency )]
        public decimal total {
            get; set;
        }
    }
}