using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    [Table("ResponsabilityType", Schema = "Book")]
    public class ResponsabilityType
    {
        public Guid ResponsabilityTypeId { get; set; }

        public string Title { get; set; }

        public string Desription { get; set; }
    }
}
