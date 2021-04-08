using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manitouage1.Models
{
    public class ProductsTotals
    {
        public ProductsTotals()
        {
        }

        public ProductsTotals( IEnumerable<ProductDto> products )
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

        public ProductsTotals( IEnumerable<Product> products )
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

        public decimal subTotal {
            get; set;
        }

        public decimal taxes {
            get; set;
        }

        public decimal total {
            get; set;
        }
    }
}