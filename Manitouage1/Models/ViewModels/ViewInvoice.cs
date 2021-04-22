// Created by Sandra Kupfer 2021/03

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manitouage1.Models.ViewModels
{
    public class ViewInvoice
    {
        public InvoiceDto invoiceDto {
            get; set;
        }

        public ApplicationUser applicationUser {
            get; set;
        }

        public IEnumerable<ViewInvoiceProduct> invoiceProducts {
            get; set;
        }

        public ProductTotals totals {
            get; set;
        }
    }
}