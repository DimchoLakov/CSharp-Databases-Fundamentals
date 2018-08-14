using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ProductShop.Data;

namespace ProductShop.App
{
    public class Startup
    {
        public static void Main()
        {
            using (var streamReader = new StreamReader("./../../../Resources/users.xml"))
            {
                XDocument document = XDocument.Load(streamReader);
                var elements = document.Root.Elements().ToArray();

                foreach (var xElement in elements)
                {
                    var attributes = xElement.Attributes();

                    foreach (var xAttribute in attributes)
                    {
                        Console.WriteLine($"{xAttribute.Name} - {xAttribute.Value}");
                    }

                    Console.WriteLine(new string('-', 20));
                }
            }

            using (var streamReader = new StreamReader("./../../../Resources/products.xml"))
            {
                XDocument document = XDocument.Load(streamReader);
                var elements = document.Root.Elements();
            }

            using (var streamReader = new StreamReader("./../../../Resources/categories.xml"))
            {
                XDocument document = XDocument.Load(streamReader);
                var elements = document.Root.Elements();
            }
            
            using (var dbContext = new ProductShopDbContext())
            {
                
            }
        }
    }
}