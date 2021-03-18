using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manitouage1.Models.ViewModels
{
    public class ViewInvoice
    {
        public class Totals
        {
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

        public InvoiceDto invoiceDto {
            get; set;
        }

        public IEnumerable<ProductDto> productDtos {
            get; set;
        }

        private Totals totals;

        public Totals getTotals()
        {
            if( totals == null ) {
                decimal subTotal = 0;
                decimal taxes = 0;
                decimal total = 0;
                foreach( ProductDto product in productDtos ) {
                    subTotal += product.price;
                    taxes += product.price * product.taxRate;
                    total += subTotal + taxes;
                }
                totals = new Totals {
                    subTotal = subTotal,
                    taxes = taxes,
                    total = total
                };
            }
            return totals;
        }

        public decimal getTaxes()
        {
            decimal subTotal = 0;
            foreach( ProductDto product in productDtos ) {
                subTotal += product.price;
            }
            return subTotal;
        }
    }
}