// Created by Sandra Kupfer 2021/03

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace Manitouage1.Models
{
    public class Product
    {
        [Key]
        public int productId {
            get; set;
        }

        [DisplayName( "Product Name" )]
        [Required(ErrorMessage = "Please enter the name of the product or service.")]
        public string productName {
            get; set;
        }

        [DisplayName( "Price" )]
        [Required(ErrorMessage = "Please enter the price of the product or service.")]
        public decimal price {
            get; set;
        }

        [DisplayName( "Tax" )]
        public decimal taxRate {
            get; set;
        }

        public virtual IList<ProductXInvoice> ProductXInvoices {
            get; set;
        }

        public ProductDto getDto()
        {
            return new ProductDto {
                productId = productId,
                productName = productName,
                price = price,
                taxRate = taxRate
            };
        }
    }

    public class ProductDto
    {
        [DisplayName( "Product ID" )]
        public int productId {
            get; set;
        }

        [DisplayName( "Product Name" )]
        public string productName {
            get; set;
        }

        [DisplayName( "Price" )]
        [DataType( DataType.Currency )]
        public decimal price {
            get; set;
        }

        [DisplayName( "Tax" )]
        [DataType( DataType.Currency )]
        public decimal taxRate {
            get; set;
        }
    }
}