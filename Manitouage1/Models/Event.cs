﻿using System;
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
        [Required]
        public string Title
        {
            get; set;
        }
        [Required]
        public string Description
        {
            get; set;
        }
        [Required]
        public DateTime DateTime
        {
            get; set;
        }
        [Required]
        public string Location
        {
            get; set;
        }
        [Required]
        public Decimal Duration
        {
            get; set;
        }
        [Required]
        public string ContactPerson
        {
            get; set;
        }
    }
}