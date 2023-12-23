// See https://aka.ms/new-console-template for more information
using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

Console.WriteLine("Hello, World!");
var pubContext1 = new PubContext();

// using (var context = new PubContext())
// {
//     context.Database.EnsureCreated();
// }

// QueryAuthors();
// AddAuthor();
// QueryAuthors();

// AddAuthorWithBook();
// GetAuthorsWithBooks();

// QueryFilters();
// AddSomeMoreAuthors();
SkipAndTakeAuthors();




void SkipAndTakeAuthors()
{
    var pageSize = 2;
    for(int i = 0; i < 5; i++)
    {
        Console.WriteLine("Page " + i);
        pubContext1.Authors.Skip(i*pageSize).Take(pageSize).ToList().ForEach(a =>
        {
            
            Console.WriteLine(a.FirstName + " " + a.LastName);
        });
    }
}

void AddSomeMoreAuthors()
{
    pubContext1.Authors.Add(new Author(){FirstName = "Rowan", LastName = "Lerman"});
    pubContext1.Authors.Add(new Author(){FirstName = "Don", LastName = "Jones"});
    pubContext1.Authors.Add(new Author(){FirstName = "Jim", LastName = "Christopher"});
    pubContext1.Authors.Add(new Author(){FirstName = "Stepthen", LastName = "Haunts"});
    pubContext1.SaveChanges();
}

void QueryFilters()
{
    pubContext1.Authors.Where(r => r.FirstName == "Julie").ToList();

    var julie = "Julie";
    pubContext1.Authors.Where(r => r.FirstName == julie).ToList();
}

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
