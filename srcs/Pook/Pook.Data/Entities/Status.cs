using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    [Table("Status", Schema = "User")]
    public class Status : Content
    {
        public Guid StatusId { get; set; }

        public string Title { get; set; }
    }
}
