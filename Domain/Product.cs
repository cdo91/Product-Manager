using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Manager.Domain
{
    [Index(nameof(Sku), IsUnique = true)]
    public class Product
    {
        private string name;
        private string sku;
        private string description;
        private string picture;
        private int price;

        [Key]
        [Column("ProductID")]
        public int ProductId { get; set; }

        [MaxLength (50)]
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

        [MaxLength(50)]
        public string Sku
        {
            get => sku;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    sku = value;
                }
                else
                {
                    throw new ArgumentException("SKU måste vara ifylld");
                }
            }
        }

        [MaxLength(100)]
        public string Description
        {
            get => description;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    description = value;
                }
                else
                {
                    throw new ArgumentException("Beskrivning måste vara ifylld");
                }
            }
        }

        [MaxLength(100)]
        public string ImageUrl
        {
            get => picture;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    picture = value;
                }
                else
                {
                    throw new ArgumentException("Bild måste vara ifylld");
                }
            }
        }

        public int Price
        {
            get => price;
            set
            {
                if (value != null && value >= 0) // You can adjust the validation as needed
                {
                    price = value;
                }
                else
                {
                    throw new ArgumentException("Pris måste vara ett positivt heltal");
                }
            }
        }

        public Product(string name, string sku, string description, string picture, int price) // Changed the price parameter to int
        {
            Name = name;
            Sku = sku;
            Description = description;
            ImageUrl = picture;
            Price = price; // Note that you pass an int here instead of a string
        }

        public Product()
        {
        }
    }

    //public class Product
    //{
    //    // Parameterless constructor required by Entity Framework Core
    //    public Product()
    //    {
    //    }

    //    [Key]
    //    [Column("ProductID")]
    //    public int ProductID { get; set; }

    //    public string Name { get; set; }

    //    public string SKU { get; set; }

    //    public string Description { get; set; }

    //    public string Picture { get; set; }

    //    public int Price { get; set; }

    //    public Product(string name, string sku, string description, string picture, int price)
    //    {
    //        Name = name;
    //        SKU = sku;
    //        Description = description;
    //        Picture = picture;
    //        Price = price;
    //    }
    //}

}
