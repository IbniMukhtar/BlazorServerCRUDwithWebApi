using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.api.Dmain.Entities
{
    [Table("Products")]
    public record Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; init; }

        [Column("Product_name")]
        [MaxLength(100)]
        public string? ProductName { get; init; }

        [Column("Product_code")]
        [MaxLength(10)]
        public string? ProductCode { get; init; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; init; }
    }
}