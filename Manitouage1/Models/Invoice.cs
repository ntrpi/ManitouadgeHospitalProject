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
    }

    public class InvoiceDto
    {
        public int invoiceId {
            get; set;
        }

        public DateTime created {
            get; set;
        }
    }
}