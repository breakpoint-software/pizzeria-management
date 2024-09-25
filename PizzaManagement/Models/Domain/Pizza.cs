using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Domain
{
    [Table("Pizza")]
    public class Pizza
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(7, 2)")]
        public decimal Price { get; set; }
    }

}
