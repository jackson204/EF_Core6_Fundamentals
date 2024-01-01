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
// GetAuthors();
// InsertNewAuthorWithNewBook();
// InsertNewAuthorWith2NewBook();
// AddNewBookToExistingAuthorInMemory();
// AddNewBookToExistingAuthorInMemoryViaBook();
ConnectExistingAeistAndCoverObjects();

void ConnectExistingAeistAndCoverObjects()
{
    // var artist = new Artist()
    // {
    //     FirstNmae = "test",
    //     LastName = "test LastName",
    // };
    // var artist2 = new Artist()
    // {
    //     FirstNmae = "test 2",
    //     LastName = "test LastName 2",
    // };
    // var cover = new Cover()
    // {
    //     DesignIdeas = "test DesignIdeas",
    // };
    // pubContext1.Artists.Add(artist);
    // pubContext1.Artists.Add(artist2);
    // pubContext1.Covers.Add(cover);
    // pubContext1.SaveChanges();

    var artistA = pubContext1.Artists.Find(1);
    var artistB = pubContext1.Artists.Find(2);
    var cover = pubContext1.Covers.Find(1);
    cover.Artists.Add(artistA);
    cover.Artists.Add(artistB);
    pubContext1.SaveChanges();
}

#region 看結果

// EagerLoadBooksWithAuthors();
// EagerLoadBooksWithAuthorsAddAsNoTracking();

#endregion

void EagerLoadBooksWithAuthors()
{
    var authors = pubContext1.Authors.Include(r => r.Books).ToList();
    authors.ForEach(r =>
    {
        Console.WriteLine($"{r.FirstName} , Count: {r.Books.Count}");
        foreach (var book in r.Books)
        {
            Console.WriteLine($"  {book.Title}");
        }
    });

    Console.WriteLine("----");

    var authors1 = pubContext1.Authors
        .Include(r =>
            r.Books
                .Where(book => book.PublishedOn >= new DateTime(2020, 1, 1))
                .OrderBy(book => book.Title))
        .ToList();
    authors1.ForEach(r =>
    {
        Console.WriteLine($"{r.FirstName} , Count: {r.Books.Count}");
        foreach (var book1 in r.Books)
        {
            Console.WriteLine($"  {book1.Title}");
        }
    });
}

void EagerLoadBooksWithAuthorsAddAsNoTracking()
{
    var authors = pubContext1.Authors.Include(r => r.Books).ToList();
    authors.ForEach(r =>
    {
        Console.WriteLine($"{r.FirstName} , Count: {r.Books.Count}");
        foreach (var book in r.Books)
        {
            Console.WriteLine($"  {book.Title}");
        }
    });

    Console.WriteLine("----");

    var authors1 = pubContext1.Authors.AsNoTracking()
        .Include(r =>
            r.Books
                .Where(book => book.PublishedOn >= new DateTime(2020, 1, 1))
                .OrderBy(book => book.Title))
        .ToList();
    authors1.ForEach(r =>
    {
        Console.WriteLine($"{r.FirstName} , Count: {r.Books.Count}");
        foreach (var book1 in r.Books)
        {
            Console.WriteLine($"  {book1.Title}");
        }
    });
}

void AddNewBookToExistingAuthorInMemoryViaBook()
{
    var book = new Book()
    {
        Title = "Shift 2 ",
        PublishedOn = new DateTime(2021, 2, 2),
        AuthorId = 1
    };
    pubContext1.Books.Add(book);
    pubContext1.SaveChanges();
}

void AddNewBookToExistingAuthorInMemory()
{
    var author = pubContext1.Authors.FirstOrDefault(a => a.LastName == "Rutledge");
    if (author != null)
    {
        author.Books.Add(new Book()
        {
            Title = "Wool 2", PublishedOn = new DateTime(2019, 1, 1), BasePrice = 19.99m,
        });
        pubContext1.Authors.Add(author);
    }
    pubContext1.SaveChanges();
}

void InsertNewAuthorWith2NewBook()
{
    var author = new Author() { FirstName = "Don", LastName = "Jones" };
    author.Books.Add(new Book()
    {
        Title = "The Never ", PublishedOn = new DateTime(2019, 1, 1)
    });
    author.Books.Add(new Book()
    {
        Title = "The Never 2", PublishedOn = new DateTime(2019, 1, 1)
    });
    pubContext1.Authors.Add(author);
    pubContext1.SaveChanges();
}

void InsertNewAuthorWithNewBook()
{
    var author = new Author() { FirstName = "Lynda", LastName = "Rutledge" };
    author
        .Books.Add(new Book()
        {
            Title = "Faith Bass Darling's Last Garage Sale", PublishedOn = new DateTime(2021, 2, 1)
        });
    pubContext1.Authors.Add(author);
    pubContext1.SaveChanges();
}

void GetAuthors()
{
    var authors = pubContext1.Authors.ToList();
}

// AddAuthorWithBook();

// GetAuthorsWithBooks();

// QueryFilters();
// AddSomeMoreAuthors();
// SkipAndTakeAuthors();

// InsertAuthor();
// RetrieveAndUpdateAuthor();
// VariousOperations();
// AddBooks();

void AddBooks()
{
    pubContext1.Books.Add(new Book()
    {
        Title = "EF Core in Action test",
        PublishedOn = DateTime.Now
    });
    pubContext1.SaveChanges();
}

void VariousOperations()
{
    var author = pubContext1.Authors.Find(2);
    author.FirstName = "test";
    var author1 = new Author()
    {
        FirstName = "Dan",
        LastName = "Appleman"
    };
    pubContext1.Add(author1);
    pubContext1.SaveChanges();
}

void RetrieveAndUpdateAuthor()
{
    var author = pubContext1.Authors.FirstOrDefault(a => a.FirstName == "Frnak" && a.LastName == "Lerman");
    if (author != null)
    {
        author.FirstName = "Frnak222";
        Console.WriteLine($"before save {pubContext1.ChangeTracker.DebugView.ShortView}");

        pubContext1.ChangeTracker.DetectChanges();

        Console.WriteLine($"after save {pubContext1.ChangeTracker.DebugView.ShortView}");
        pubContext1.SaveChanges();
    }
}

void InsertAuthor()
{
    var entity = new Author()
    {
        FirstName = "Frnak2",
        LastName = "Lerman2",
    };
    pubContext1.Authors.Add(entity);
    pubContext1.SaveChanges();
}

void SkipAndTakeAuthors()
{
    var pageSize = 2;
    for(int i = 0; i < 5; i++)
    {
        Console.WriteLine("Page " + i);
        pubContext1.Authors.Skip(i * pageSize).Take(pageSize).ToList().ForEach(a =>
        {
            Console.WriteLine(a.FirstName + " " + a.LastName);
        });
    }
}

void AddSomeMoreAuthors()
{
    pubContext1.Authors.Add(new Author() { FirstName = "Rowan", LastName = "Lerman" });
    pubContext1.Authors.Add(new Author() { FirstName = "Don", LastName = "Jones" });
    pubContext1.Authors.Add(new Author() { FirstName = "Jim", LastName = "Christopher" });
    pubContext1.Authors.Add(new Author() { FirstName = "Stepthen", LastName = "Haunts" });
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
    foreach (var author in pubContext.Authors.Include(a => a.Books))
    {
        Console.WriteLine(author.FirstName + " " + author.LastName);
        foreach (var book in author.Books)
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
        Books = new List<Book>()
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
