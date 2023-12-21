// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

Console.WriteLine("Hello, World!");

using (var context = new PubContext())
{
    context.Database.EnsureCreated();
}

// QueryAuthors();
// AddAuthor();
// QueryAuthors();

AddAuthorWithBook();
GetAuthorsWithBooks();

void GetAuthorsWithBooks()
{
    using var pubContext = new PubContext();
    foreach (var author in pubContext.Authors.Include(a => a.Book))
    {
        Console.WriteLine(author.FirstName + " " + author.LastName);
        foreach (var book in author.Book)
        {
            Console.WriteLine("  " + book.Title);
        }
    }
}

void QueryAuthors()
{
    using (var context = new PubContext())
    {
        var authors = context.Authors.ToList();
        foreach (var author in authors)
        {
            Console.WriteLine(author.FirstName + " " + author.LastName);
        }
    }
}

void AddAuthor()
{
    var author = new Author() { FirstName = "Julie", LastName = "Lerman" };
    using var pubContext = new PubContext();
    pubContext.Authors.Add(author);
    pubContext.SaveChanges();
}

void AddAuthorWithBook()
{
    var author1 = new Author()
    {
        FirstName = "Julie3",
        LastName = "Lerman3",
        Book = new List<Book>()
        {
            new Book()
            {
                Title = "EF Core in Action 2",
                PublishedOn = new DateTime(2018, 10, 1),
                BasePrice = 35.99m
            },
            new Book()
            {
                Title = "EF Core in Action 3",
                PublishedOn = new DateTime(2018, 10, 1),
                BasePrice = 35.99m
            }
        }
    };
    using var pubContext = new PubContext();
    pubContext.Authors.Add(author1);
    pubContext.SaveChanges();
}
