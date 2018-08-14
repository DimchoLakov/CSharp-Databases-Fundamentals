using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductShop.App.DTOs;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop.App
{
    public class Startup
    {
        public static void Main()
        {
            MapperInitializer.InitializeMapper();

            var xmlString = File.ReadAllText("./../../../Resources/users.xml");

            var serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("users"));
            var deserializerdUsers = (UserDto[])serializer.Deserialize(new StringReader(xmlString));

            var users = new List<User>();

            foreach (var deserializerdUser in deserializerdUsers)
            {
                var userDto = new UserDto(deserializerdUser.FirstName, deserializerdUser.LastName, deserializerdUser.Age);

                var user = Mapper.Map<User>(userDto);

                users.Add(user);
            }

            using (var dbContext = new ProductShopDbContext())
            {
                dbContext.Database.Migrate();
                dbContext.AddRange(users);
                dbContext.SaveChanges();
            }

            //using (var streamReader = new StreamReader("./../../../Resources/users.xml"))
            //{
            //    using (var dbContext = new ProductShopDbContext())
            //    {
            //        dbContext.Database.Migrate();
            //        XDocument document = XDocument.Load(streamReader);
            //        var elements = document.Root.Elements().ToArray();

            //        foreach (var xElement in elements)
            //        {
            //            var attributes = xElement.Attributes();

            //            var firstName = attributes.FirstOrDefault(x => x.Name == "firstName");
            //            var lastName = attributes.FirstOrDefault(x => x.Name == "lastName");
            //            var age = attributes.FirstOrDefault(x => x.Name == "age");

            //            if (firstName != null && lastName != null && age != null)
            //            {
            //                UserDto userDto = new UserDto(firstName.Value, lastName.Value, int.Parse(age.Value));

            //                var user = Mapper.Map<User>(userDto);

            //                dbContext
            //                    .Users
            //                    .Add(user);

            //                dbContext.SaveChanges();
            //                Console.WriteLine($" User {firstName.Value} {lastName.Value} Added");
            //            }
            //        }
            //    }
            //}
            
        }
    }
}