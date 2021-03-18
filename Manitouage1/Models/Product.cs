using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        [Required]
        public string productName {
            get; set;
        }

        [Required]
        public decimal price {
            get; set;
        }

        public decimal taxRate {
            get; set;
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