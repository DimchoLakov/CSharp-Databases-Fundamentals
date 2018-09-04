using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using VaporStore.Data.Models.Enums;
using VaporStore.DataProcessor.ExportDto;
using VaporStore.DataProcessor.ImportDto;
using Formatting = Newtonsoft.Json.Formatting;

namespace VaporStore.DataProcessor
{
    using System;
    using Data;

    public static class Serializer
    {
        private const string XmlDateFormat = "yyyy-MM-dd HH:mm";


        public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {
            var games = context
                .Genres
                .Where(g => genreNames.Contains(g.Name))
                .Select(genre => new
                {
                    Id = genre.Id,
                    Genre = genre.Name,
                    Games = genre.Games
                        .Where(game => game.Purchases.Any())
                        .Select(game => new
                        {
                            Id = game.Id,
                            Title = game.Name,
                            Developer = game.Developer.Name,
                            Tags = string.Join(", ", game.GameTags.Select(gt => gt.Tag.Name)),
                            Players = game.Purchases.Count
                        })
                        .OrderByDescending(game => game.Players)
                        .ThenBy(game => game.Id)
                        .ToArray(),
                    TotalPlayers = genre.Games.Sum(a => a.Purchases.Count)
                })
                .OrderByDescending(x => x.TotalPlayers)
                .ThenBy(g => g.Id)
                .ToArray();

            var serializedGames = JsonConvert.SerializeObject(games, Formatting.Indented);

            return serializedGames;
        }

        public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
        {
            var purchaseType = Enum.Parse<PurchaseType>(storeType);

            var userPurchases = context
                .Users
                .Select(u => new ExportUserDto()
                {
                    Username = u.Username,

                    Purchases = u.Cards
                        .SelectMany(x => x.Purchases)
                        .Where(p => p.Type == purchaseType)
                        .Select(p => new ExportPurchaseDto()
                        {
                            Card = p.Card.Number,
                            Cvc = p.Card.Cvc,
                            Date = p.Date.ToString(string.Format(CultureInfo.InvariantCulture, XmlDateFormat)),
                            Game = new ExportGameDto()
                            {
                                Title = p.Game.Name,
                                Genre = p.Game.Genre.Name,
                                Price = p.Game.Price
                            }
                        })
                        .OrderBy(p => p.Date)
                        .ToArray(),

                    TotalSpent = u.Cards
                        .SelectMany(c => c.Purchases)
                        .Where(p => p.Type == purchaseType)
                        .Sum(p => p.Game.Price)
                })
                .Where(p => p.Purchases.Length > 0)
                .OrderByDescending(u => u.TotalSpent)
                .ThenBy(u => u.Username)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(ExportUserDto[]), new XmlRootAttribute("Users"));
            var xmlNamespaces = new XmlSerializerNamespaces(new XmlQualifiedName[] { XmlQualifiedName.Empty });

            var sb = new StringBuilder();
            xmlSerializer.Serialize(new StringWriter(sb), userPurchases, xmlNamespaces);

            return sb.ToString().Trim();
        }
    }
}