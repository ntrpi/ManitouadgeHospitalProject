// Created by Sandra Kupfer 2021/03

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
        public enum Status
        {
            Created,
            Issued,
            Paid
        }

        [Key]
        public int invoiceId {
            get; set;
        }

        public DateTime created {
            get; set;
        }

        public DateTime? issued {
            get; set;
        }

        public DateTime? paid {
            get; set;
        }

        public Status status {
            get; set;
        }

        [Required(ErrorMessage = "Please select a client.")]
        [ForeignKey( "ApplicationUser" )]
        // ApplicationUser.Id
        public string userId {
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
                issued = issued,
                paid = paid,
                status = status,
                userId = userId
            };
        }
    }

    public class InvoiceDto
    {
        [DisplayName( "Invoice Number" )]
        public int invoiceId {
            get; set;
        }

        [DisplayName( "Date Created" )]
        [DataType( DataType.Date )]
        public DateTime created {
            get; set;
        }

        [DisplayName( "Date Issued" )]
        [DataType( DataType.Date )]
        public DateTime? issued {
            get; set;
        }

        [DisplayName( "Date Paid" )]
        [DataType( DataType.Date )]
        public DateTime? paid {
            get; set;
        }

        [DisplayName( "Status" )]
        public Invoice.Status status {
            get; set;
        }

        [DisplayName( "User ID" )]
        // ApplicationUser.Id
        public string userId {
            get; set;
        }
    }
}