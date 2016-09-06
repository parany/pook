using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    [Table("Status", Schema = "User")]
    public class Status : Content
    {
        public string Title { get; set; }
    }
}
