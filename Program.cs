using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using Product_Manager.Data;
using Product_Manager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;

class Program
{
    static ApplicationContext context = new ApplicationContext();

    static void Main()
    {
        while (true)
        {
            Console.CursorVisible = false;
            Console.Clear();
            Console.WriteLine("1. Registrera produkt");
            Console.WriteLine("2. Sök produkt");
            Console.WriteLine("3. Lägg till kategori");
            Console.WriteLine("4. Lägg till produkt till kategori");
            Console.WriteLine("5. Lista kateogrier");
            Console.WriteLine("6. Avsluta");

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            Console.WriteLine();

            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    RegisterProduct();
                    break;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    SearchProduct();
                    break;

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    AddCategory();
                    break;

                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    AddProductToCategory();
                    break;

                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    ListCategories();
                    break;

                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                    Console.Clear();
                    Console.WriteLine("Avslutar programmet...");
                    Environment.Exit(0);
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Ogiltigt alternativ. Försök igen.");
                    break;
            }
        }
    }

    static void RegisterProduct()
    {
        while (true)
        {
            Console.CursorVisible = true;
            Console.Clear();

            Console.Write("Namn: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("SKU: ");
            string sku = Console.ReadLine() ?? "";

            Console.Write("Beskrivning: ");
            string description = Console.ReadLine() ?? "";

            Console.Write("Bild (URL): ");
            string picture = Console.ReadLine() ?? "";

            Console.Write("Pris: ");
            string priceInput = Console.ReadLine() ?? "";

            Console.Write("\nÄr detta korrekt? (J)a (N)ej: ");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.J)
            {
                Console.CursorVisible = false;
                Console.Clear();

                try
                {
                    if (int.TryParse(priceInput, out int price) && price >= 0) // Försöker konvertera ett pris till ett heltal och kontrollera att det är positivt.
                    {
                        Product product = new Product(name, sku, description, picture, price); // Försöker skapa en ny produkt i Product-klassen.

                        var existingSku = context.Product.FirstOrDefault(p => p.Sku == product.Sku); // Kontrollerar om SKU redan finns i databasen.

                        if (existingSku != null)
                        {
                            throw new ArgumentException("SKU finns redan");
                        }

                        context.Product.Add(product); // Lägger till produkten i databasen och sparar ändringar.
                        context.SaveChanges();

                        Console.WriteLine("Produkt sparad");
                        Thread.Sleep(2000);
                        break;
                    }
                    else
                    {
                        throw new ArgumentException("Pris får endast innehålla siffror.");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.Clear();
                    Console.WriteLine($"Error: {ex.Message}");
                    Thread.Sleep(2000);
                    break;
                }
            }
            else if (keyInfo.Key == ConsoleKey.N) // Väljer att inte spara produkten. Går tillbaks högst upp i while-loopen.
            {
                Console.Clear();
                continue;
            }
        }
    }

    static void DisplayProductDetail(Product product)
    {
        Console.Clear();
        Console.WriteLine($"Namn: {product.Name}");
        Console.WriteLine($"SKU: {product.Sku}");
        Console.WriteLine($"Beskrivning: {product.Description}");
        Console.WriteLine($"Bild (URL): {product.ImageUrl}");
        Console.WriteLine($"Pris: {product.Price}");
    }

    static bool ReturnToProductDetails = false;

    static void SearchProduct()
    {
        Console.CursorVisible = true;
        Console.Clear();
        Console.Write("SKU: ");
        string searchSku = Console.ReadLine() ?? "";

        try
        {
            var existingProduct = context.Product.FirstOrDefault(p => p.Sku == searchSku); // Söker efter produktens sku i databasen

            while (true)
            {
                if (existingProduct != null) // Om produkten hittades
                {
                    // Hämtar de existerande produktdetaljerna
                    Product product = new Product(existingProduct.Name, existingProduct.Sku, existingProduct.Description, existingProduct.ImageUrl, existingProduct.Price); 

                    DisplayProductDetail(product); // Visar produktdetaljerna i konsolen

                    Console.CursorVisible = false;
                    Console.WriteLine("\n(R)adera (U)ppdatera");
                    Console.WriteLine("\nTryck på Escape för att gå tillbaka till huvudmenyn");

                    while (true)
                    {
                        ConsoleKeyInfo keyInfo = Console.ReadKey();

                        if (keyInfo.Key == ConsoleKey.R)
                        {
                            //Console.CursorVisible = true;
                            DeleteProduct(product, context); // Anropar funktionen för att radera produkten
                            break;
                        }
                        else if (keyInfo.Key == ConsoleKey.U)
                        {
                            //Console.CursorVisible = true;
                            UpdateProduct(product, context); // Anropar funktionen för att uppdatera produkten
                            break;
                        }
                        else if (keyInfo.Key == ConsoleKey.Escape)
                        {
                            break; // Om användaren trycker på Escape går man tillbaks till huvudmenyn
                        }
                    }

                    if (ReturnToProductDetails)
                    {
                        ReturnToProductDetails = false; // Återställer flaggan till false. 
                        continue; // Går tillbaks högst upp i den första while-loopen.
                    }
                }
                else
                {
                    throw new ArgumentException("Produkt finns ej");
                }

                break;
            }
        }
        catch (ArgumentException ex)
        {
            Console.Clear();
            Console.WriteLine($"Error: {ex.Message}");
            Thread.Sleep(2000);
            return;
        }
    }

    static void DeleteProduct(Product product, ApplicationContext context)
    {
        Console.Clear();

        DisplayProductDetail(product); // Visar produktinformation

        Console.Write("\nRadera produkt? (J)a (N)ej");
        ConsoleKeyInfo keyInfo = Console.ReadKey();

        if (keyInfo.Key == ConsoleKey.J)
        {
            Console.CursorVisible = false;

            var existingProduct = context.Product.FirstOrDefault(p => p.Sku == product.Sku); // Försöker hitta den befintliga produkten i databasen med samma SKU.

            if (existingProduct != null)
            {
                context.Product.Remove(existingProduct); //Tar bort den befintliga produkten från Product-tabellen i databasen.
                context.SaveChanges();

                Console.Clear();
                Console.WriteLine("Produkt raderad");
                Thread.Sleep(2000);
                return;
            }
        }
        else if (keyInfo.Key == ConsoleKey.N)
        {
            ReturnToProductDetails = true; // Går tillbaks till ReturnToProductDetails i SearchProduct-funktionen
            return;
        }
    }

    static void UpdateProduct(Product product, ApplicationContext context)
    {
        Console.CursorVisible = true;
        Console.Clear();

        while (true)
        {
            Console.Write("Namn: ");
            string name = Console.ReadLine() ?? "";
            Console.WriteLine($"SKU: {product.Sku}");

            Console.Write("Beskrivning: ");
            string description = Console.ReadLine() ?? "";

            Console.Write("Bild (URL): ");
            string picture = Console.ReadLine() ?? "";

            Console.Write("Pris: ");
            string priceInput = Console.ReadLine() ?? "";

            Console.Write("\nÄr detta korrekt? (J)a (N)ej: ");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.J)
            {
                Console.CursorVisible = false;
                Console.Clear();

                try
                {   
                    var productToUpdate = context.Product.FirstOrDefault(p => p.Sku == product.Sku); // Söker efter produktens sku i databasen.

                    if (int.TryParse(priceInput, out int price) && price >= 0) // Försöker konvertera ett pris till ett heltal och kontrollera att det är positivt.
                    {
                        if (productToUpdate != null) // Om alla värden inte är null går vi till uppdatera produkt
                        {
                            productToUpdate.Name = name;
                            productToUpdate.Description = description; // Uppdaterar produktdetaljerna i databasen
                            productToUpdate.ImageUrl = picture;     
                            productToUpdate.Price = price;

                            context.SaveChanges();

                            Console.WriteLine("Produkt sparad");
                            Thread.Sleep(2000);
                            break;
                        }
                    }
                    else
                    {
                        throw new Exception("Pris får endast innehålla siffror.");
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine($"Error: {ex.Message}");
                    Thread.Sleep(2000);
                    break;
                }
            }
            else if (keyInfo.Key == ConsoleKey.N)
            {
                Console.Clear();
                continue;
            }
        }
    }

    static void AddCategory()
    {
        while (true)
        {
            Console.CursorVisible = true;
            Console.Clear();

            Console.Write("Namn: ");
            string categoryName = Console.ReadLine() ?? "";

            Console.Write("\nÄr detta korrekt? (J)a (N)ej: ");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.J)
            {
                Console.CursorVisible = false;
                Console.Clear();

                try
                {
                    Category category = new Category(categoryName);  // Försöker skapa en kategori med angivet namn.

                    var existingCategory = context.Category.FirstOrDefault(c => c.Name == category.Name); // Kontrollerar om kategorin redan finns i databasen.

                    if (existingCategory != null)
                    {
                        throw new ArgumentException("Kategori finns redan"); // Om kategorin redan finns, kastas ett felmeddelande.
                    }
                    else
                    {
                        context.Category.Add(category); // Om kategorin inte finns, läggs den till i databasen.
                        context.SaveChanges();
                        Console.Write("Kategori sparad");
                        Thread.Sleep(2000);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine($"Error: {ex.Message}");
                    Thread.Sleep(2000);
                    break;
                }
            }
            else if (keyInfo.Key == ConsoleKey.N)
            {
                Console.Clear();
                continue; // Om användaren inte bekräftar, fortsätter man högst upp i while-loopen.
            }
        }
    }

    static void AddProductToCategory()
    {
        Console.CursorVisible = true;
        Console.Clear();
        Console.Write("SKU: ");
        string searchSku = Console.ReadLine() ?? "";

        try
        {
            var existingProduct = context.Product.FirstOrDefault(p => p.Sku == searchSku);

            if (existingProduct != null)
            {
                Console.CursorVisible = true;
                Console.Clear();
                Console.WriteLine($"Namn: {existingProduct.Name}");
                Console.WriteLine($"SKU: {existingProduct.Sku}");
                Console.Write("Ange kategori: ");
                string categoryName = Console.ReadLine() ?? "";

                var existingCategory = context.Category.FirstOrDefault(c => c.Name == categoryName);

                if (existingCategory != null)
                {
                    
                    if (!existingCategory.Product.Contains(existingProduct))
                    {
                        existingCategory.Product.Add(existingProduct);
                        context.SaveChanges();

                        Console.CursorVisible = false;
                        Console.Clear();
                        Console.WriteLine("Produkt tillagd");
                        Thread.Sleep(2000);
                        return;
                    }
                    else
                    {
                        throw new ArgumentException("Produkt redan tillagd");
                    }
                }
                else
                {
                    throw new ArgumentException("Kategorin finns inte");
                }
            }
            else
            {
                throw new ArgumentException("Produkt hittades inte");
            }
        }
        catch (ArgumentException ex)
        {
            Console.Clear();
            Console.WriteLine($"Error: {ex.Message}");
            Thread.Sleep(2000);
            return;
        }
    }

    static void ListCategories()
    {
        Console.CursorVisible = false;
        Console.Clear();
        Console.WriteLine("Namn \n------------------------------------------------------------------");

        var productsInCategories = context.Category.Include(c => c.Product).ToList();

        foreach (var category in productsInCategories)
        {
            Console.WriteLine($"{category.Name} ({category.Product.Count})");

            foreach (var product in category.Product)
            {
                Console.WriteLine($"  {product.Name.PadRight(25)} {product.Price} SEK");
            }
        }

        Console.WriteLine("\nTryck på Escape för att gå tillbaka till huvudmenyn");

        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        if (keyInfo.Key == ConsoleKey.Escape)
        {
            Console.Clear();
            return;
        }
    }
}