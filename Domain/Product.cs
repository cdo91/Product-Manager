using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Manager.Domain;

[Index(nameof(Sku), IsUnique = true)] // Använder ett index för att säkerställa att SKU är unikt för varje produkt.
public class Product
{
    private string name;
    private string sku;
    private string description;
    private string imageUrl;

    public int ProductId { get; set; } // Unik identifierare för produkt i databasen.

    [MaxLength (50)] // Anger att det maximala tillåtna längden för denna strängattribut är 50 tecken.
    public string Name
    {
        get => name; // Hämtar namnet för produkten.
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                name = value; // Tilldelar värdet till produktens namn om det inte är tomt.
            }
            else
            {
                throw new ArgumentException("Namn måste vara ifylld"); // Kastar ett undantag om namnet är tomt.
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
        get => imageUrl;
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                imageUrl = value;
            }
            else
            {
                throw new ArgumentException("Bild måste vara ifylld");
            }
        }
    }

    public int Price { get; set; }

    public Product(string name, string sku, string description, string imageUrl, int price) // Konstruktor för att skapa en ny produkt med alla attribut.
    {
        Name = name;
        Sku = sku;
        Description = description;
        ImageUrl = imageUrl;
        Price = price;
    }

    public Product() // Standardkonstruktor för en produkt som EF core använder för att kunna skapa nya instanser av Product-klassen.
    {
    }

    public ICollection<Category> Category { get; set; } = new List<Category>(); // En samling av kategoriobjekt som är kopplade till denna klass.
}
