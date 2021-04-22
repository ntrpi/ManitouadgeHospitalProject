using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Manitouage1.Models.ViewModels
{
    public class ViewInvoiceProduct
    {
        public ViewInvoiceProduct( ProductDto productDto, int quantity )
        {
            productId = productDto.productId;
            productName = productDto.productName;
            taxRate = productDto.taxRate;
            price = productDto.price;
            this.quantity = quantity;
            cost = productDto.price * quantity;
        }

        public int productId {
            get; set;
        }

        [DisplayName("Name")]
        public string productName {
            get; set;
        }

        public decimal taxRate {
            get; set;
        }

        public decimal price {
            get; set;
        }

        [DisplayName( "Quantity" )]
        public int quantity {
            get; set;
        }

        [DisplayName( "Cost" )]
        [DataType( DataType.Currency )]
        public decimal cost {
            get; set;
        }
    }
}