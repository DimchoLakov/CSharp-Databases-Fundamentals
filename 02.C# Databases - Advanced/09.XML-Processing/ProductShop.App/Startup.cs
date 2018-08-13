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
                var elements = document.Root.Elements();
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