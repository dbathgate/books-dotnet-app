using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorApp.Repository;

[Table("books")]
public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("name")]
    [Required]
    public string? Name { get; set; }
    
    [Column("author")]
    [Required]
    public string? Author { get; set; }
}