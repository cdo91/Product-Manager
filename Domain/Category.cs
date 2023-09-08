using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Manager.Domain
{
    //public class Category
    //{
    //    private string? name;
    //    public string? Name
    //    {
    //        get => name;
    //        set
    //        {
    //            if (!string.IsNullOrEmpty(value))
    //            {
    //                name = value;
    //            }
    //            else
    //            {
    //                throw new ArgumentException("Namn måste vara ifylld");
    //            }
    //        }
    //    }

    //    public Category(string name)
    //    {
    //        Name = name;
    //    }
    //}

    public class Category
    {

        [Key]
        [Column("CategoryID")]
        public int CategoryId { get; set; }

        private string name; // Define the private field for the property.

        [MaxLength(50)]
        public string Name
        {
            get => name;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    name = value;
                }
                else
                {
                    throw new ArgumentException("Namn måste vara ifylld");
                }
            }
        }

        public Category(string name)
        {
            Name = name;
        }

        // Parameterless constructor required by Entity Framework Core
        public Category()
        {
        }

        public ICollection<ProductCategory> ProductCategory { get; set; } = new List<ProductCategory>();

    }
}
