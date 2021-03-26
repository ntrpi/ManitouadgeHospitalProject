using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Manitouage1.Models
{
    public class Event
    {
        [Key]
        public int EventId
        {
            get; set;
        }
        public string Title
        {
            get; set;
        }
        public string Description
        {
            get; set;
        }
        public DateTime DateTime
        {
            get; set;
        }
        public string Location
        {
            get; set;
        }
        public string Duration
        {
            get; set;
        }
        public string ContactPerson
        {
            get; set;
        }
    }
}