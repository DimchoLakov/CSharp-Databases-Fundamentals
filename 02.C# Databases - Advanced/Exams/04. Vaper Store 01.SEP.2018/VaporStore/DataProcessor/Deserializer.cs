using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using VaporStore.Data.Models;
using VaporStore.Data.Models.Enums;
using VaporStore.DataProcessor.ImportDto;

namespace VaporStore.DataProcessor
{
    using System;
    using Data;

    public static class Deserializer
    {
        private const string DateFormat = "yyyy-MM-dd";

        private const string XmlDateFormat = "dd/MM/yyyy HH:mm";

        private const string FailureMsg = "Invalid Data";

        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            var deserializedGames = JsonConvert.DeserializeObject<GameDto[]>(jsonString);

            var sb = new StringBuilder();

            var games = new List<Game>();

            var developers = new List<Developer>();
            var tags = new List<Tag>();
            var genres = new List<Genre>();

            foreach (var gameDto in deserializedGames)
            {
                if (!IsValid(gameDto) || gameDto.Tags.Length == 0)
                {
                    sb.AppendLine(FailureMsg);
                    continue;
                }

                var areTagsValid = true;
                foreach (var dtoTag in gameDto.Tags)
                {
                    if (string.IsNullOrWhiteSpace(dtoTag))
                    {
                        areTagsValid = false;
                        break;
                    }
                }

                if (!areTagsValid)
                {
                    sb.AppendLine(FailureMsg);
                    continue;
                }

                var date = DateTime.ParseExact(gameDto.ReleaseDate, DateFormat, CultureInfo.InvariantCulture);

                var game = new Game()
                {
                    Name = gameDto.Name,
                    Price = gameDto.Price,
                    ReleaseDate = date
                };

                var dev = developers.FirstOrDefault(d => d.Name == gameDto.DeveloperName);

                if (dev == null)
                {
                    dev = new Developer()
                    {
                        Name = gameDto.DeveloperName
                    };
                    developers.Add(dev);

                    game.Developer = dev;
                }
                else
                {
                    game.Developer = dev;
                }

                var gen = genres.FirstOrDefault(g => g.Name == gameDto.GenreName);

                if (gen == null)
                {
                    gen = new Genre()
                    {
                        Name = gameDto.GenreName
                    };
                    genres.Add(gen);

                    game.Genre = gen;
                }
                else
                {
                    game.Genre = gen;
                }

                var currentTags = new List<Tag>();
                foreach (var gameDtoTag in gameDto.Tags)
                {
                    var newTag = tags.FirstOrDefault(t => t.Name == gameDtoTag);
                    if (newTag == null)
                    {
                        newTag = new Tag()
                        {
                            Name = gameDtoTag
                        };

                        tags.Add(newTag);
                    }

                    currentTags.Add(newTag);
                }

                var gameTags = new List<GameTag>();
                foreach (var currentTag in currentTags)
                {
                    gameTags.Add(new GameTag()
                    {
                        Tag = currentTag
                    });
                }

                game.GameTags = gameTags;

                games.Add(game);

                sb.AppendLine($"Added {game.Name} ({game.Genre.Name}) with {gameTags.Count} tags");
            }

            context.Games.AddRange(games);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            var deserializedUsers = JsonConvert.DeserializeObject<UserDto[]>(jsonString);

            var users = new List<User>();

            var sb = new StringBuilder();
            var counter = 1;
            foreach (var userDto in deserializedUsers)
            {
                if (counter == 1)
                {
                    sb.AppendLine(FailureMsg);
                    counter++;
                    continue;
                }

                if (!IsValid(userDto))
                {
                    sb.AppendLine(FailureMsg);
                    continue;
                }

                if (userDto.Cards.Length == 0)
                {
                    sb.AppendLine(FailureMsg);
                    continue;
                }

                foreach (var cardDto in userDto.Cards)
                {
                    if (!IsValid(cardDto))
                    {
                        sb.AppendLine(FailureMsg);
                        continue;
                    }
                }

                var user = new User()
                {
                    FullName = userDto.FullName,
                    Username = userDto.Username,
                    Email = userDto.Email,
                    Age = userDto.Age
                };

                var cards = new List<Card>();

                foreach (var cardDto in userDto.Cards)
                {
                    var card = new Card()
                    {
                        Cvc = cardDto.Cvc,
                        Number = cardDto.Number,
                        Type = Enum.Parse<CardType>(cardDto.Type)
                    };
                    cards.Add(card);
                }
                user.Cards = cards;
                users.Add(user);

                sb.AppendLine($"Imported {user.Username} with {cards.Count} cards");
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(PurchaseDto[]), new XmlRootAttribute("Purchases"));
            var deserializedPurchases = (PurchaseDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();

            var purchases = new List<Purchase>();

            foreach (var purchaseDto in deserializedPurchases)
            {
                if (!IsValid(purchaseDto))
                {
                    sb.AppendLine(FailureMsg);
                    continue;
                }

                var isTypeValid = Enum.TryParse(purchaseDto.Type, out PurchaseType purType);

                if (!isTypeValid)
                {
                    sb.AppendLine(FailureMsg);
                    continue;
                }

                var card = context.Cards.FirstOrDefault(c => c.Number == purchaseDto.Card);
                if (card == null)
                {
                    sb.AppendLine(FailureMsg);
                    continue;
                }

                var game = context.Games.FirstOrDefault(g => g.Name == purchaseDto.Title);
                if (game == null)
                {
                    sb.AppendLine(FailureMsg);
                    continue;
                }

                var date = DateTime.ParseExact(purchaseDto.Date, XmlDateFormat, CultureInfo.InvariantCulture);

                var purchase = new Purchase()
                {
                    Game = game,
                    Card = card,
                    Date = date,
                    ProductKey = purchaseDto.Key,
                    Type = purType
                };

                purchases.Add(purchase);

                sb.AppendLine($"Imported {purchaseDto.Title} for {card.User.Username}");
            }

            context.AddRange(purchases);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResult, true);
        }
    }
}