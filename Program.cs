using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static List<Product> productList = new List<Product>();

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Registrera produkt");
            Console.WriteLine("2. Sök produkt");
            Console.WriteLine("3. Avsluta");

            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (choice)
            {
                case '1':
                    RegisterProduct();
                    break;

                case '2':
                    SearchProduct();
                    break;

                case '3':
                    Console.Clear();
                    Console.WriteLine("Avslutar programmet...");
                    return;

                default:
                    Console.Clear();
                    Console.WriteLine("Ogiltigt alternativ. Försök igen.");
                    break;
            }
        }
    }

    static void RegisterProduct()
    {
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
        string price = Console.ReadLine() ?? "";

        productList.Add(new Product(name, sku, description, picture, price));

        Console.Write("\nÄr detta korrekt? (J)a (N)ej: ");
        char confirmation = Console.ReadKey().KeyChar;

        if (char.ToUpper(confirmation) == 'J')
        {
            Console.Clear();
            Console.WriteLine("Produkt sparad.");
            Thread.Sleep(2000);
        }
    }

    static void SearchProduct()
    {
        static void DisplayProductDetails(Product product)
        {
            Console.Clear();
            Console.WriteLine($"Namn: {product.Name}");
            Console.WriteLine($"SKU: {product.SKU}");
            Console.WriteLine($"Beskrivning: {product.Description}");
            Console.WriteLine($"Bild (URL): {product.Picture}");
            Console.WriteLine($"Pris: {product.Price}");
        }

        Console.Clear();
        Console.Write("SKU: ");
        string searchSKU = Console.ReadLine() ?? "";

        bool found = false;
        bool deleteProduct = false;

        foreach (Product product in productList)
        {
            if (product.SKU.Equals(searchSKU, StringComparison.OrdinalIgnoreCase))
            {
                DisplayProductDetails(product);

                Console.WriteLine("\n(R)adera");
                Console.WriteLine("\nTryck på Escape för att gå tillbaka till huvudmenyn");

                found = true;

                while (true)
                {
                    ConsoleKeyInfo keyInfoInner = Console.ReadKey(true);
                    if (keyInfoInner.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                    else if (keyInfoInner.Key == ConsoleKey.R)
                    {
                        deleteProduct = true;
                        break;
                    }
                }

                if (deleteProduct)
                {
                    Console.Clear();
                    DisplayProductDetails(product);
                    Console.Write("\nRadera produkt? (J)a (N)ej: ");

                    while (true)
                    {
                        ConsoleKeyInfo keyInfoInner = Console.ReadKey(true);
                        if (keyInfoInner.Key == ConsoleKey.J)
                        {
                            productList.Remove(product);
                            Console.Clear();
                            Console.WriteLine("Produkt raderad");
                            Thread.Sleep(2000);
                            break;
                        }
                        else if (keyInfoInner.Key == ConsoleKey.N)
                        {
                            break;
                        }
                    }
                }

                break;
            }
        }

        if (!found)
        {
            Console.Clear();
            Console.WriteLine("Produkt finns ej");
            Thread.Sleep(2000);
        }
    }
}

class Product
{
    public string Name { get; set; }
    public string SKU { get; set; }
    public string Description { get; set; }
    public string Picture { get; set; }
    public string Price { get; set; }

    public Product(string name, string sku, string description, string picture, string price)
    {
        Name = name;
        SKU = sku;
        Description = description;
        Picture = picture;
        Price = price;
    }
}
