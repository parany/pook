using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Pook.Data.Entities;

namespace Pook.Service.Models.Firms
{
    public class Firm
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string Address { get; set; }
    }
}
