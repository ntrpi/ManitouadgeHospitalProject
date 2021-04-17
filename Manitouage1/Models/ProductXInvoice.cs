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
    public class ProductXInvoice
    {
        [Key]
        public int id {
            get; set;
        }

        [ForeignKey( "Product" )]
        public int productId {
            get; set;
        }

        public virtual Product Product {
            get; set;
        }

        [ForeignKey( "Invoice" )]
        public int invoiceId {
            get; set;
        }

        public virtual Invoice Invoice {
            get; set;
        }

        public int quantity {
            get; set;
        }

        public ProductXInvoiceDto getDto()
        {
            return new ProductXInvoiceDto {
                id = id,
                productId = productId,
                invoiceId = invoiceId,
                quantity = quantity
            };
        }
    }

    public class ProductXInvoiceDto
    {
        public int id {
            get; set;
        }

        [DisplayName( "Product Id" )]
        public int productId {
            get; set;
        }

        [DisplayName( "Invoice Id" )]
        public int invoiceId {
            get; set;
        }

        [DisplayName( "Quantity" )]
        public int quantity {
            get; set;
        }

    }
}