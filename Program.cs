using Microsoft.Data.SqlClient;
using Product_Manager.Data;
using Product_Manager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    //static List<Product> productList = new List<Product>();
    //static List<Category> categoryList = new List<Category>();
    //static Dictionary<string, List<Product>> productCategories = new Dictionary<string, List<Product>>();

    //static string connectionString = "Server=.;Database=ProductManager;Integrated Security=True;Encrypt=False";
    //static ApplicationContext context = new ApplicationContext(connectionString);

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
        if (!int.TryParse(Console.ReadLine(), out int price))
        {
            Console.CursorVisible = false;
            Console.Clear();
            Console.WriteLine("Pris får endast innehålla siffror");
            Thread.Sleep(2000);
            return;
        }

        Console.Write("\nÄr detta korrekt? (J)a (N)ej: ");
        ConsoleKeyInfo keyInfo = Console.ReadKey();

     
            if (keyInfo.Key == ConsoleKey.J)
            {
                Console.CursorVisible = false;
                Console.Clear();

                // Create the product
                Product product = new Product(name, sku, description, picture, price);

                if (SaveProduct(context, product))
                {
                    Console.Write("Produkt sparad");
                    Thread.Sleep(2000);
                }
            }
            else if (keyInfo.Key == ConsoleKey.N)
            {
                Console.Clear();
                return;
            }
        
    }

    static bool SaveProduct(ApplicationContext context, Product product)
    {
        // Check if SKU already exists
        var existingProduct = context.Product.FirstOrDefault(p => p.Sku == product.Sku);
        if (existingProduct != null)
        {
            Console.CursorVisible = false;
            Console.Clear();
            Console.WriteLine("SKU finns redan");
            Thread.Sleep(2000);
            return false;
        }

        // Insert the product into the database
        try
        {
            context.Product.Add(product);
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }

    static void DisplayProductDetails(Product product)
    {
        Console.Clear();
        Console.WriteLine($"Namn: {product.Name}");
        Console.WriteLine($"SKU: {product.Sku}");
        Console.WriteLine($"Beskrivning: {product.Description}");
        Console.WriteLine($"Bild (URL): {product.ImageUrl}");
        Console.WriteLine($"Pris: {product.Price}");
    }

    static void SearchProduct()
    {
        Console.CursorVisible = true;
        Console.Clear();
        Console.Write("SKU: ");
        string searchSKU = Console.ReadLine() ?? "";
        
        Product? product = SearchProductInSql(searchSKU, context); // Pass the context as an argument

        if (product != null)
        {
            DisplayProductDetails(product);

            Console.CursorVisible = false;
            Console.WriteLine("\n(R)adera (U)ppdatera");
            Console.WriteLine("\nTryck på Escape för att gå tillbaka till huvudmenyn");

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.R)
                {
                    Console.CursorVisible = true;
                    DeleteProduct(product, context); // Pass the context as an argument
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.U)
                {
                    Console.CursorVisible = true;
                    UpdateProduct(product, context); // Pass the context as an argument
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
        else
        {
            Console.CursorVisible = false;
            Console.Clear();
            Console.WriteLine("Produkt finns ej");
            Thread.Sleep(2000);
        }
        
    }

    static Product? SearchProductInSql(string searchSKU, ApplicationContext context)
    {
        // Use Entity Framework Core to query the database
        var product = context.Product.FirstOrDefault(p => p.Sku == searchSKU);

        if (product != null)
        {
            // Convert the retrieved product to your Product class
            return new Product(product.Name, product.Sku, product.Description, product.ImageUrl, product.Price);
        }

        return null;
    }

    static void DeleteProduct(Product product, ApplicationContext context)
    {
        Console.Clear();

        while (true)
        {
            DisplayProductDetails(product);
            Console.Write("\nRadera produkt? (J)a (N)ej: ");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.J)
            {
                Console.CursorVisible = false;

                try
                {
                    var existingProduct = context.Product.FirstOrDefault(p => p.Sku == product.Sku);

                    if (existingProduct != null)
                    {
                        var productCategoryEntries = context.ProductCategory.Where
                        (pc => pc.ProductId == existingProduct.ProductId).ToList();

                        context.ProductCategory.RemoveRange(productCategoryEntries);
                        context.Product.Remove(existingProduct);
                        context.SaveChanges();

                        Console.Clear();
                        Console.WriteLine("Produkt raderad");
                        Thread.Sleep(2000);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    break;
                }
            }
            else if (keyInfo.Key == ConsoleKey.N)
            {
                Console.Clear();
                break;
            }
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
            int price = int.Parse(Console.ReadLine() ?? "");

            Console.Write("\nÄr detta korrekt? (J)a (N)ej: ");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.J)
            {
                Console.CursorVisible = false;
                Console.Clear();

                try
                {
                    // Find the product in the Products DbSet
                    var productToUpdate = context.Product.FirstOrDefault(p => p.Sku == product.Sku);

                    if (productToUpdate != null)
                    {
                        // Update product details
                        productToUpdate.Name = name;
                        productToUpdate.Description = description;
                        productToUpdate.ImageUrl = picture;
                        productToUpdate.Price = price;

                        context.SaveChanges();

                        Console.WriteLine("Produkt sparad");
                        Thread.Sleep(2000);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
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
            string name = Console.ReadLine() ?? "";

            Console.Write("\nÄr detta korrekt? (J)a (N)ej: ");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.J)
            {
                Console.CursorVisible = false;
                Console.Clear();

                Category category = new Category(name);

                if (SaveCategory(context, category))
                {
                    Console.Write("Kategori sparad");
                    Thread.Sleep(2000);
                }

                break;
            }
            else if (keyInfo.Key == ConsoleKey.N)
            {
                Console.Clear();
                continue;
            }
        }
    }

    static bool SaveCategory(ApplicationContext context, Category category)
    {

        var existingCategory = context.Category.FirstOrDefault(c => c.Name == category.Name);
        if (existingCategory != null)
        {
            Console.CursorVisible = false;
            Console.Clear();
            Console.WriteLine("Kategori finns redan");
            Thread.Sleep(2000);
            return false;
        }

        try
        {
            context.Category.Add(category);
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }

    static void AddProductToCategory()
    {
        Console.CursorVisible = true;
        Console.Clear();
        Console.Write("SKU: ");
        string searchSKU = Console.ReadLine() ?? "";

        Product? product = SearchProductInSql(searchSKU, context);

        if (product != null)
        {
            Console.CursorVisible = true;
            Console.Clear();
            Console.WriteLine($"Namn: {product.Name}");
            Console.WriteLine($"SKU: {product.Sku}");

            Console.Write("Ange kategori: ");
            string categoryName = Console.ReadLine() ?? "";

            try
            {
                // Check if the category exists in the database
                var existingCategory = context.Category.FirstOrDefault(c => c.Name == categoryName);

                if (existingCategory != null)
                {
                    // Check if the product exists
                    var existingProduct = context.Product.FirstOrDefault(p => p.Sku == product.Sku);

                    if (existingProduct != null)
                    {
                        // Check if the product is already associated with the category
                        var existingProductInCategory = context.ProductCategory.FirstOrDefault
                        (pc => pc.ProductId == existingProduct.ProductId && pc.CategoryId == existingCategory.CategoryId);

                        if (existingProductInCategory == null)
                        {
                            // Create a new ProductCategory entity
                            var productCategory = new ProductCategory
                            {
                                CategoryId = existingCategory.CategoryId,
                                CategoryName = existingCategory.Name,
                                ProductId = existingProduct.ProductId,
                                Name = existingProduct.Name,
                                SKU = existingProduct.Sku,
                                Description = existingProduct.Description,
                                ImageUrl = existingProduct.ImageUrl,
                                Price = existingProduct.Price
                            };

                            // Add the ProductCategory entity to the context and save changes
                            context.ProductCategory.Add(productCategory);
                            context.SaveChanges();  

                            Console.CursorVisible = false;
                            Console.Clear();
                            Console.WriteLine("Produkt tillagd till kategori");
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            Console.CursorVisible = false;
                            Console.Clear();
                            Console.WriteLine("Produkt finns redan i kategorin");
                            Thread.Sleep(2000);
                        }
                    }
                }
                else
                {
                    throw new InvalidOperationException("Kategorin finns inte");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.CursorVisible = false;
                Console.Clear();
                Console.WriteLine(ex.Message);
                Thread.Sleep(2000);
            }
        }
        else
        {
            Console.CursorVisible = false;
            Console.Clear();
            Console.WriteLine("Produkt hittades inte");
            Thread.Sleep(2000);
        }
    }

    static void ListCategories()
    {
        Console.CursorVisible = false;
        Console.Clear();
        Console.WriteLine("Namn \n------------------------------------------------------------------");

        var categoriyWithProductCount = context.Category
            .Select(c => new
            {
                CategoryId = c.CategoryId,
                CategoryName = c.Name,
                ProductCount = c.ProductCategory.Count()
            })
            .ToList();

        foreach (var category in categoriyWithProductCount)
        {
            Console.WriteLine($"{category.CategoryName} ({category.ProductCount})");

            if (category.ProductCount > 0)
            {
                var productInCategory = context.ProductCategory
                    .Where(pc => pc.Category.CategoryId == category.CategoryId)
                    .Select(pc => new
                    {
                        ProductName = pc.Product.Name,
                        ProductPrice = pc.Product.Price
                    })
                    .ToList();

                foreach (var product in productInCategory)
                {
                    Console.WriteLine($"  {product.ProductName.PadRight(25)} {product.ProductPrice} SEK");
                }
            }
        }

        Console.WriteLine("\nTryck på Escape för att gå tillbaka till huvudmenyn");

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                Console.Clear();
                break;
            }
        }
    }

    //static List<Product> productList = new List<Product>();
    //static List<Category> categoryList = new List<Category>();
    //static Dictionary<string, List<Product>> productCategories = new Dictionary<string, List<Product>>();

    //static string connectionString = "Server=.;Database=ProductManager;Integrated Security=True;Encrypt=False";
    //static ApplicationContext context = new ApplicationContext(connectionString);
}