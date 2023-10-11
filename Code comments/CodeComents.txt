using Microsoft.Data.SqlClient;
using Product_Manager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Manager.Code_comments
{
    //static bool SaveProduct(Product product)
    //{
    //    using (var connection = new SqlConnection(connectionString))
    //    {
    //        connection.Open();

    //        // Check if SKU already exists
    //        string checkSkuSql = "SELECT COUNT(*) FROM Products WHERE SKU = @SKU";
    //        using (var checkSkuCommand = new SqlCommand(checkSkuSql, connection))
    //        {
    //            checkSkuCommand.Parameters.AddWithValue("@SKU", product.SKU);
    //            int skuCount = (int)checkSkuCommand.ExecuteScalar();

    //            if (skuCount > 0)
    //            {
    //                Console.CursorVisible = false;
    //                Console.Clear();
    //                Console.WriteLine("SKU finns redan");
    //                Thread.Sleep(2000);
    //                return false;
    //            }
    //        }

    //        // Insert the product into the database
    //        string insertSql = @"
    //    INSERT INTO Products (
    //        Name, 
    //        SKU, 
    //        Description, 
    //        Picture,
    //        Price
    //    ) VALUES (
    //        @Name, 
    //        @SKU, 
    //        @Description, 
    //        @Picture,
    //        @Price
    //    )
    //    ";

    //        using (var command = new SqlCommand(insertSql, connection))
    //        {
    //            command.Parameters.AddWithValue("@Name", product.Name);
    //            command.Parameters.AddWithValue("@SKU", product.SKU);
    //            command.Parameters.AddWithValue("@Description", product.Description);
    //            command.Parameters.AddWithValue("@Picture", product.Picture);
    //            command.Parameters.AddWithValue("@Price", product.Price);

    //            try
    //            {
    //                command.ExecuteNonQuery();
    //                return true;
    //            }
    //            catch (Exception ex)
    //            {
    //                Console.WriteLine($"Error: {ex.Message}");
    //                return false;
    //            }
    //        }
    //    }
    //}

    //static Product? SearchProductInSql(string searchSKU)
    //{
    //    const string sql = @"
    //    SELECT Name, 
    //           SKU, 
    //           Description,
    //           Picture,
    //           Price
    //    FROM Product
    //    WHERE SKU = @SKU
    //    ";

    //    using (var connection = new SqlConnection(connectionString))
    //    {
    //        using (var searchProductCommand = new SqlCommand(sql, connection))
    //        {
    //            searchProductCommand.Parameters.AddWithValue("@SKU", searchSKU);

    //            connection.Open();

    //            using (var reader = searchProductCommand.ExecuteReader())
    //            {
    //                if (reader.Read())
    //                {
    //                    string name = reader["Name"]?.ToString() ?? "";
    //                    string sku = reader["SKU"]?.ToString() ?? "";
    //                    string description = reader["Description"]?.ToString() ?? "";
    //                    string picture = reader["Picture"]?.ToString() ?? "";
    //                    int price = int.Parse(reader["Price"]?.ToString() ?? "");

    //                    return new Product(name, sku, description, picture, price);
    //                }
    //            }
    //        }
    //    }

    //    return null;
    //}

    //static void AddProductToCategory()
    //{
    //    Console.CursorVisible = true;
    //    Console.Clear();
    //    Console.Write("SKU: ");
    //    string searchSKU = Console.ReadLine() ?? "";

    //    Product? product = SearchProductInSql(searchSKU, context);

    //    if (product != null)
    //    {
    //        Console.CursorVisible = true;
    //        Console.Clear();
    //        Console.WriteLine($"Namn: {product.Name}");
    //        Console.WriteLine($"SKU: {product.SKU}");

    //        Console.Write("Ange kategori: ");
    //        string categoryName = Console.ReadLine() ?? "";

    //        try
    //        {
    //            using (var connection = new SqlConnection(connectionString))
    //            {
    //                connection.Open();

    //                // Check if the category exists in SQL
    //                using (var checkCategoryCommand = new SqlCommand("SELECT CategoryID FROM Categories WHERE Name = @Name", connection))
    //                {
    //                    checkCategoryCommand.Parameters.AddWithValue("@Name", categoryName);
    //                    var categoryId = checkCategoryCommand.ExecuteScalar();

    //                    if (categoryId != null)
    //                    {
    //                        // Check if the product is already associated with the category
    //                        using (var checkAssociationCommand = new SqlCommand("SELECT 1 FROM ProductCategory WHERE ProductID = (SELECT ProductID FROM Product WHERE SKU = @SKU) AND CategoryID = @CategoryID", connection))
    //                        {
    //                            checkAssociationCommand.Parameters.AddWithValue("@SKU", searchSKU);
    //                            checkAssociationCommand.Parameters.AddWithValue("@CategoryID", (int)categoryId);
    //                            var existingAssociation = checkAssociationCommand.ExecuteScalar();

    //                            if (existingAssociation == null)
    //                            {
    //                                // Add the product to the category
    //                                using (var addProductToCategoryCommand = new SqlCommand("INSERT INTO ProductCategory (ProductID, CategoryID) VALUES ((SELECT ProductID FROM Product WHERE SKU = @SKU), @CategoryID)", connection))
    //                                {
    //                                    addProductToCategoryCommand.Parameters.AddWithValue("@SKU", searchSKU);
    //                                    addProductToCategoryCommand.Parameters.AddWithValue("@CategoryID", (int)categoryId);
    //                                    addProductToCategoryCommand.ExecuteNonQuery();
    //                                    Console.CursorVisible = false;
    //                                    Console.Clear();
    //                                    Console.WriteLine("Produkt tillagd");
    //                                    Thread.Sleep(2000);
    //                                }
    //                            }
    //                            else
    //                            {
    //                                throw new InvalidOperationException("Produkt redan tillagd");
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        throw new InvalidOperationException("Kategorin finns inte");
    //                    }
    //                }
    //            }
    //        }
    //        catch (InvalidOperationException ex)
    //        {
    //            Console.CursorVisible = false;
    //            Console.Clear();
    //            Console.WriteLine(ex.Message);
    //            Thread.Sleep(2000);
    //        }
    //    }
    //    else
    //    {
    //        Console.CursorVisible = false;
    //        Console.Clear();
    //        Console.WriteLine("Produkt hittades inte");
    //        Thread.Sleep(2000);
    //    }
    //}

    //static void DeleteProduct(Product product)
    //    {
    //        Console.Clear();

    //        while (true)
    //        {
    //            DisplayProductDetails(product);
    //            Console.Write("\nRadera produkt? (J)a (N)ej: ");
    //            ConsoleKeyInfo keyInfo = Console.ReadKey();

    //            if (keyInfo.Key == ConsoleKey.J)
    //            {
    //                Console.CursorVisible = false;

    //                using (var connection = new SqlConnection(connectionString))
    //                {
    //                    connection.Open();

    //                    try
    //                    {
    //                        // Delete records in ProductCategory that reference this product's SKU
    //                        using (var deleteProductCategoryCommand = new SqlCommand("DELETE FROM ProductCategory WHERE ProductID IN (SELECT ProductID FROM Product WHERE SKU = @SKU)", connection))
    //                        {
    //                            deleteProductCategoryCommand.Parameters.AddWithValue("@SKU", product.SKU);
    //                            deleteProductCategoryCommand.ExecuteNonQuery();
    //                        }

    //                        // Delete the product from the Product table
    //                        using (var deleteProductCommand = new SqlCommand("DELETE FROM Product WHERE SKU = @SKU", connection))
    //                        {
    //                            deleteProductCommand.Parameters.AddWithValue("@SKU", product.SKU);
    //                            deleteProductCommand.ExecuteNonQuery();
    //                        }
    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        Console.WriteLine($"Error: {ex.Message}");
    //                    }

    //                    connection.Close();
    //                }

    //                Console.Clear();
    //                Console.WriteLine("Produkt raderad");
    //                Thread.Sleep(2000);
    //                break;
    //            }
    //            else if (keyInfo.Key == ConsoleKey.N)
    //            {
    //                Console.Clear();
    //                break;
    //            }
    //        }
    //    }

    //static void UpdateProduct(Product product)
    //{
    //    Console.CursorVisible = true;
    //    Console.Clear();

    //    while (true)
    //    {
    //        Console.Write("Namn: ");
    //        string name = Console.ReadLine() ?? "";
    //        Console.WriteLine($"SKU: {product.SKU}");

    //        Console.Write("Beskrivning: ");
    //        string description = Console.ReadLine() ?? "";

    //        Console.Write("Bild (URL): ");
    //        string picture = Console.ReadLine() ?? "";

    //        Console.Write("Pris: ");
    //        int price = int.Parse(Console.ReadLine() ?? "");

    //        Console.Write("\nÄr detta korrekt? (J)a (N)ej: ");
    //        ConsoleKeyInfo keyInfo = Console.ReadKey();

    //        if (keyInfo.Key == ConsoleKey.J)
    //        {
    //            Console.CursorVisible = false;
    //            Console.Clear();

    //            try
    //            {
    //                using (var connection = new SqlConnection(connectionString))
    //                {
    //                    connection.Open();

    //                    // SQL command to update the product
    //                    string sql = @"
    //                UPDATE Product
    //                SET Name = @Name,
    //                    Description = @Description,
    //                    Picture = @Picture,
    //                    Price = @Price
    //                WHERE SKU = @SKU
    //                ";

    //                    using (var updateCommand = new SqlCommand(sql, connection))
    //                    {
    //                        updateCommand.Parameters.AddWithValue("@Name", name);
    //                        updateCommand.Parameters.AddWithValue("@Description", description);
    //                        updateCommand.Parameters.AddWithValue("@Picture", picture);
    //                        updateCommand.Parameters.AddWithValue("@Price", price);
    //                        updateCommand.Parameters.AddWithValue("@SKU", product.SKU);

    //                        updateCommand.ExecuteNonQuery();
    //                    }
    //                }

    //                Console.WriteLine("Produkt sparad");
    //                Thread.Sleep(2000);
    //                break;
    //            }
    //            catch (Exception ex)
    //            {
    //                Console.WriteLine($"Error: {ex.Message}");
    //            }
    //        }
    //        else if (keyInfo.Key == ConsoleKey.N)
    //        {
    //            Console.Clear();
    //            continue;
    //        }
    //    }
    //}

    //static void AddCategory()
    //{
    //    Console.CursorVisible = true;

    //    while (true)
    //    {
    //        Console.Clear();
    //        Console.Write("Namn: ");
    //        string name = Console.ReadLine() ?? "";

    //        Console.Write("\nÄr detta korrekt? (J)a (N)ej: ");
    //        ConsoleKeyInfo keyInfo = Console.ReadKey();

    //        if (keyInfo.Key == ConsoleKey.J)
    //        {
    //            Console.CursorVisible = false;
    //            Console.Clear();

    //            // Create a new Category instance, which will handle the validation
    //            Category category = new Category(name);

    //            string insertCategorySql = @"
    //                INSERT INTO Categories (Name)
    //                VALUES (@Name)
    //            ";

    //            using (var connection = new SqlConnection(connectionString))
    //            {
    //                using (var addCategoryCommand = new SqlCommand(insertCategorySql, connection))
    //                {
    //                    addCategoryCommand.Parameters.AddWithValue("@Name", category.Name);

    //                    try
    //                    {
    //                        connection.Open();
    //                        addCategoryCommand.ExecuteNonQuery();
    //                        Console.Write("Kategori sparad");
    //                        Thread.Sleep(2000);
    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        Console.WriteLine($"Error: {ex.Message}");
    //                    }
    //                }
    //            }

    //            break;
    //        }
    //        else if (keyInfo.Key == ConsoleKey.N)
    //        {
    //            Console.Clear();
    //            continue;
    //        }
    //    }
    //}

    //static void AddProductToCategory()
    //{
    //    Console.CursorVisible = true;
    //    Console.Clear();
    //    Console.Write("SKU: ");
    //    string searchSKU = Console.ReadLine() ?? "";

    //    Product? product = SearchProductInSql(searchSKU, context);

    //    if (product != null)
    //    {
    //        Console.CursorVisible = true;
    //        Console.Clear();
    //        Console.WriteLine($"Namn: {product.Name}");
    //        Console.WriteLine($"SKU: {product.SKU}");

    //        Console.Write("Ange kategori: ");
    //        string categoryName = Console.ReadLine() ?? "";

    //        try
    //        {
    //            using (var connection = new SqlConnection(connectionString))
    //            {
    //                connection.Open();

    //                // Check if the category exists in SQL
    //                using (var checkCategoryCommand = new SqlCommand("SELECT CategoryID FROM Categories WHERE Name = @Name", connection))
    //                {
    //                    checkCategoryCommand.Parameters.AddWithValue("@Name", categoryName);
    //                    var categoryId = checkCategoryCommand.ExecuteScalar();

    //                    if (categoryId != null)
    //                    {
    //                        // Check if the product is already associated with the category
    //                        using (var checkAssociationCommand = new SqlCommand("SELECT 1 FROM ProductCategory WHERE ProductID = (SELECT ProductID FROM Product WHERE SKU = @SKU) AND CategoryID = @CategoryID", connection))
    //                        {
    //                            checkAssociationCommand.Parameters.AddWithValue("@SKU", searchSKU);
    //                            checkAssociationCommand.Parameters.AddWithValue("@CategoryID", (int)categoryId);
    //                            var existingAssociation = checkAssociationCommand.ExecuteScalar();

    //                            if (existingAssociation == null)
    //                            {
    //                                // Add the product to the category
    //                                using (var addProductToCategoryCommand = new SqlCommand("INSERT INTO ProductCategory (ProductID, CategoryID) VALUES ((SELECT ProductID FROM Product WHERE SKU = @SKU), @CategoryID)", connection))
    //                                {
    //                                    addProductToCategoryCommand.Parameters.AddWithValue("@SKU", searchSKU);
    //                                    addProductToCategoryCommand.Parameters.AddWithValue("@CategoryID", (int)categoryId);
    //                                    addProductToCategoryCommand.ExecuteNonQuery();
    //                                    Console.CursorVisible = false;
    //                                    Console.Clear();
    //                                    Console.WriteLine("Produkt tillagd");
    //                                    Thread.Sleep(2000);
    //                                }
    //                            }
    //                            else
    //                            {
    //                                throw new InvalidOperationException("Produkt redan tillagd");
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        throw new InvalidOperationException("Kategorin finns inte");
    //                    }
    //                }
    //            }
    //        }
    //        catch (InvalidOperationException ex)
    //        {
    //            Console.CursorVisible = false;
    //            Console.Clear();
    //            Console.WriteLine(ex.Message);
    //            Thread.Sleep(2000);
    //        }
    //    }
    //    else
    //    {
    //        Console.CursorVisible = false;
    //        Console.Clear();
    //        Console.WriteLine("Produkt hittades inte");
    //        Thread.Sleep(2000);
    //    }
    //}

    //static void SaveProductToCategory(Product product, string categoryName)
    //{
    //    try
    //    {
    //        using (var connection = new SqlConnection(connectionString))
    //        {
    //            connection.Open();

    //            // Check if the category exists in SQL
    //            using (var checkCategoryCommand = new SqlCommand("SELECT CategoryID FROM Categories WHERE Name = @Name", connection))
    //            {
    //                checkCategoryCommand.Parameters.AddWithValue("@Name", categoryName);
    //                var categoryId = checkCategoryCommand.ExecuteScalar();

    //                if (categoryId != null)
    //                {
    //                    // Check if the product is already associated with the category
    //                    using (var checkAssociationCommand = new SqlCommand("SELECT 1 FROM ProductCategory WHERE ProductID = (SELECT ProductID FROM Product WHERE SKU = @SKU) AND CategoryID = @CategoryID", connection))
    //                    {
    //                        checkAssociationCommand.Parameters.AddWithValue("@SKU", product.SKU);
    //                        checkAssociationCommand.Parameters.AddWithValue("@CategoryID", (int)categoryId);
    //                        var existingAssociation = checkAssociationCommand.ExecuteScalar();

    //                        if (existingAssociation == null)
    //                        {
    //                            // Add the product to the category
    //                            using (var addProductToCategoryCommand = new SqlCommand("INSERT INTO ProductCategory (ProductID, CategoryID) VALUES ((SELECT ProductID FROM Products WHERE SKU = @SKU), @CategoryID)", connection))
    //                            {
    //                                addProductToCategoryCommand.Parameters.AddWithValue("@SKU", product.SKU);
    //                                addProductToCategoryCommand.Parameters.AddWithValue("@CategoryID", (int)categoryId);
    //                                addProductToCategoryCommand.ExecuteNonQuery();
    //                                Console.CursorVisible = false;
    //                                Console.Clear();
    //                                Console.WriteLine("Produkt tillagd");
    //                                Thread.Sleep(2000);
    //                            }
    //                        }
    //                        else
    //                        {
    //                            throw new InvalidOperationException("Produkt redan tillagd");
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    throw new InvalidOperationException("Kategorin finns inte");
    //                }
    //            }
    //        }
    //    }
    //    catch (InvalidOperationException ex)
    //    {
    //        Console.CursorVisible = false;
    //        Console.Clear();
    //        Console.WriteLine(ex.Message);
    //        Thread.Sleep(2000);
    //    }
    //}
}
