using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Manitouadge.Models.ViewModels
{
    public class ListEventxDonation
    {

        public IEnumerable<EventDto> events { get; set; }
    }
}