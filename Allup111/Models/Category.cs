using System.ComponentModel.DataAnnotations.Schema;

namespace Allup111.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public bool isMain { get;set; }
        public bool IsDeactive { get; set; }
        public List<Category>? Children { get; set; }
        public Category? Parent { get; set; }
        public int? ParentId { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
