using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manitouage1.Models
{
    public class Invoice
    {
        [Key]
        public int invoiceId {
            get; set;
        }

        public DateTime created {
            get; set;
        }

        [ForeignKey( "ApplicationUser" )]
        // ApplicationUser.Id
        public string Id {
            get; set;
        }

        public virtual ApplicationUser ApplicationUser {
            get; set;
        }

        public virtual IList<ProductXInvoice> ProductXInvoices {
            get; set;
        }

        public InvoiceDto getDto()
        {
            return new InvoiceDto {
                invoiceId = invoiceId,
                created = created,
                Id = Id
            };
        }
    }

    public class InvoiceDto
    {
        [DisplayName( "Invoice Number" )]
        public int invoiceId {
            get; set;
        }

        [DisplayName( "Date" )]
        public DateTime created {
            get; set;
        }

        // ApplicationUser.Id
        public string Id {
            get; set;
        }
    }
}