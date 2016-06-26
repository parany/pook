using System;
using System.ComponentModel.DataAnnotations;

namespace Pook.Data.Entities
{
    public class Content
    {
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? CreatedOn { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? UpdatedOn { get; set; }

        [ScaffoldColumn(false)]
        public Guid? CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        public Guid? UpdatedBy { get; set; }

        [ScaffoldColumn(false)]
        public string SeoTitle { get; set; }
    }
}