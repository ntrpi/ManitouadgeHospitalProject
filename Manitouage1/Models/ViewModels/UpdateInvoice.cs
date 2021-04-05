using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manitouage1.Models.ViewModels
{
    public class UpdateInvoice
    {
        public InvoiceDto invoiceDto {
            get; set;
        }

        public IEnumerable<ProductDto> productDtos {
            get; set;
        }

        public IEnumerable<string> applicationUsers {
            get; set;
        }
        //public IEnumerable<ApplicationUser> applicationUsers {
        //    get; set;
        //}
    }
}