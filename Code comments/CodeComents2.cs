using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Manager.Code_comments
{
    internal class CodeComents2
    {
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

        //        SaveProductToCategory(product, categoryName, context);
        //    }
        //    else
        //    {
        //        Console.CursorVisible = false;
        //        Console.Clear();
        //        Console.WriteLine("Produkt hittades inte");
        //        Thread.Sleep(2000);
        //    }
        //}

        //static void SaveProductToCategory(Product product, string categoryName, ApplicationContext context)
        //{
        //    try
        //    {
        //        // Check if the category exists in the database
        //        var category = context.Categories.FirstOrDefault(c => c.Name == categoryName);

        //        if (category != null)
        //        {
        //            // Check if the product exists
        //            var existingProduct = context.Products.FirstOrDefault(p => p.SKU == product.SKU);

        //            if (existingProduct != null)
        //            {
        //                // Check if the product is already associated with the category
        //                var existingAssociation = context.ProductCategory
        //                    .FirstOrDefault(pc => pc.ProductID == existingProduct.ProductID && pc.CategoryID == category.CategoryID);

        //                if (existingAssociation == null)
        //                {
        //                    // Create a new ProductCategory record
        //                    var productCategory = new ProductCategory
        //                    {
        //                        ProductID = existingProduct.ProductID,
        //                        CategoryID = category.CategoryID
        //                    };

        //                    // Add the ProductCategory record to the context and save changes
        //                    context.ProductCategory.Add(productCategory);
        //                    context.SaveChanges();

        //                    Console.CursorVisible = false;
        //                    Console.Clear();
        //                    Console.WriteLine("Produkt tillagd till kategori");
        //                    Thread.Sleep(2000);
        //                }
        //                else
        //                {
        //                    throw new InvalidOperationException("Produkt redan tillagd");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            throw new InvalidOperationException("Kategorin finns inte");
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
}
