using System;
using System.Globalization;
using System.Linq;
using System.Text;
using BookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop
{
    using BookShop.Data;
    using BookShop.Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using (var dbContext = new BookShopContext())
            {
                //string ageRestrictionType = Console.ReadLine();
                //var booksByAgeRestriction = GetBooksByAgeRestriction(dbContext, ageRestrictionType);

                //var goldenBooksWithLessThan5000Copies = GetGoldenBooks(dbContext);

                //string booksByPrice = GetBooksByPrice(dbContext);

                //int year = int.Parse(Console.ReadLine());
                //var booksNotReleasedIn = GetBooksNotRealeasedIn(dbContext, year); //Spelling mistake "Released"

                //string categoriesInput = Console.ReadLine();
                //var booksByCategory = GetBooksByCategory(dbContext, categoriesInput);

                //string dateInput = Console.ReadLine();
                //var booksReleasedBefore = GetBooksReleasedBefore(dbContext, dateInput);

                //string input = Console.ReadLine();
                //var authorsEndingIn = GetAuthorNamesEndingIn(dbContext, input);

                //string input = Console.ReadLine();
                //string bookTitlesContaining = GetBookTitlesContaining(dbContext, input);

                //string input = Console.ReadLine();
                //string booksByAuthor = GetBooksByAuthor(dbContext, input);

                //int lengthCheck = int.Parse(Console.ReadLine());
                //int booksCounts = CountBooks(dbContext, lengthCheck);

                //var copiesByAuthor = CountCopiesByAuthor(dbContext);

                //var totalProfitByCategory = GetTotalProfitByCategory(dbContext);

                //var mostRecentBooks = GetMostRecentBooks(dbContext);

                //IncreasePrices(dbContext);

                //int removedBooksCount = RemoveBooks(dbContext);
            }
        }

        public static int RemoveBooks(BookShopContext dbContext)
        {
            var booksToRemove = dbContext
                .Books
                .Where(b => b.Copies < 4200)
                .ToArray();

            int initialBooksCount = dbContext
                .Books
                .Count();

            dbContext
                .Books
                .RemoveRange(booksToRemove);

            dbContext
                .SaveChanges();

            int afterDeleteBooksCount = dbContext
                .Books
                .Count();

            int removedBooksCount = initialBooksCount - afterDeleteBooksCount;

            return removedBooksCount;
        }

        public static void IncreasePrices(BookShopContext dbContext)
        {
            var books = dbContext
                .Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToArray();

            foreach (var book in books)
            {
                book.Price += 5;
            }

            dbContext.SaveChanges();
        }

        public static string GetMostRecentBooks(BookShopContext dbContext)
        {
            var mostRecentBooks = dbContext
                .Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    TopThreeBooks = c.CategoryBooks
                        .Select(b => new
                        {
                            b.Book.Title,
                            b.Book.ReleaseDate
                        })
                        .OrderByDescending(b => b.ReleaseDate)
                        .Take(3)
                })
                .OrderBy(c => c.CategoryName)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var mostRecentBook in mostRecentBooks)
            {
                sb.AppendLine($"--{mostRecentBook.CategoryName}");

                foreach (var recentBook in mostRecentBook.TopThreeBooks)
                {
                    sb.AppendLine($"{recentBook.Title} ({recentBook.ReleaseDate.Value.Year})");
                }
            }

            return sb.ToString().Trim();
        }

        public static string GetTotalProfitByCategory(BookShopContext dbContext)
        {
            var totalProfitByCategory = dbContext
                .Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    TotalProfit = c.CategoryBooks.Sum(b => b.Book.Price * b.Book.Copies)
                })
                .OrderByDescending(c => c.TotalProfit)
                .ThenBy(c => c.CategoryName)
                .Select(c => $"{c.CategoryName} ${c.TotalProfit:f2}")
                .ToArray();

            return string.Join(Environment.NewLine, totalProfitByCategory);
        }

        public static string CountCopiesByAuthor(BookShopContext dbContext)
        {
            var copiesByAuthor = dbContext
                .Books
                .GroupBy(a => new
                {
                    AuthorFullName = a.Author.FirstName + " " + a.Author.LastName
                })
                .Select(g => new
                {
                    AuthorFullname = g.Key.AuthorFullName,
                    TotalCopies = g.Sum(book => book.Copies)
                })
                .OrderByDescending(a => a.TotalCopies)
                .Select(a => $"{a.AuthorFullname} - {a.TotalCopies}")
                .ToArray();

            return string.Join(Environment.NewLine, copiesByAuthor);
        }

        public static int CountBooks(BookShopContext dbContext, int lengthCheck)
        {
            var booksCount = dbContext
                .Books
                .Count(b => b.Title.Length > lengthCheck);

            return booksCount;
        }

        public static string GetBooksByAuthor(BookShopContext dbContext, string input)
        {
            string[] booksByAuthor = dbContext
                .Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(b => $"{b.Title} ({b.Author.FirstName} {b.Author.LastName})")
                .ToArray();

            return string.Join(Environment.NewLine, booksByAuthor);
        }

        public static string GetBookTitlesContaining(BookShopContext dbContext, string input)
        {
            string[] bookTitlesContaining = dbContext
                .Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, bookTitlesContaining);
        }

        public static string GetAuthorNamesEndingIn(BookShopContext dbContext, string input)
        {
            var authorNamesEndingIn = dbContext
                .Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => $"{a.FirstName} {a.LastName}")
                .OrderBy(a => a)
                .ToArray();

            return string.Join(Environment.NewLine, authorNamesEndingIn);
        }

        public static string GetBooksReleasedBefore(BookShopContext dbContext, string dateInput)
        {
            DateTime date = DateTime.ParseExact(dateInput, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var booksReleasedBefore = dbContext
                .Books
                .Where(b => b.ReleaseDate < date)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:f2}")
                .ToArray();

            return string.Join(Environment.NewLine, booksReleasedBefore);
        }

        public static string GetBooksByCategory(BookShopContext dbContext, string input)
        {
            string[] categories = input
                .Split()
                .Select(x => x.ToLower())
                .ToArray();

            var booksByCategory = dbContext
                .Books
                .Where(b => b.BookCategories.Any(c => categories.Contains(c.Category.Name.ToLower())))
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, booksByCategory);
        }

        public static string GetBooksNotRealeasedIn(BookShopContext dbContext, int year)
        {
            var booksNotReleasedIn = dbContext
                .Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, booksNotReleasedIn);
        }

        public static string GetBooksByPrice(BookShopContext dbContext)
        {
            var booksByPrice = dbContext
                .Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => new
                {
                    BookTitlePrice = $"{b.Title} - ${b.Price:f2}"
                })
                .Select(b => b.BookTitlePrice)
                .ToArray();


            return string.Join(Environment.NewLine, booksByPrice);
        }

        public static string GetGoldenBooks(BookShopContext dbContext)
        {
            var goldenBooksTitles = dbContext
                .Books
                .Where(b => b.EditionType == EditionType.Gold &&
                            b.Copies < 5000)
                .Select(b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, goldenBooksTitles);
        }

        public static string GetBooksByAgeRestriction(BookShopContext dbContext, string input)
        {
            var bookTitles = dbContext
                .Books
                .Where(b => b.AgeRestriction.ToString().Equals(input, StringComparison.OrdinalIgnoreCase))
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .OrderBy(t => t);

            return string.Join(Environment.NewLine, bookTitles);
        }
    }
}
