using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Manager.Domain;

public class ProductCategory
{
    [Key]
    public int ProductCategoryId { get; set; }

    // Foreign key relationships
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int ProductId { get; set; }    
    public string Name { get; set; }
    public string SKU { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public int Price { get; set; }

    // Navigation properties
    public Product Product { get; set; }
    public Category Category { get; set; }

}
