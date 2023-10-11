using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Manager.Domain;

public class Category
{
    public int CategoryId { get; set; } // Unik identifierare för kategorin i databasen.

    private string name; // Definierar det privata fältet för egenskapen.

    [MaxLength(50)]
    public string Name
    {
        get => name; // Hämtar namnet för kategorin.
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                name = value; // Tilldelar värdet till kategorins namn om det inte är tomt.
            }
            else
            {
                throw new ArgumentException("Namn måste vara ifylld"); // Kastar ett undantag om namnet är tomt.
            }
        }
    }

    public Category(string name) // Konstruktor för att skapa en ny produkt med alla attribut.
    {
        Name = name;
    }

    public Category() // En tom Standardkonstruktor för en produkt som EF core använder för att kunna skapa nya instanser av Product-klassen.
    {
    }

    public ICollection<Product> Product { get; set; } = new List<Product>(); // En samling av produktkobjekt som är kopplade till denna klass.
}
