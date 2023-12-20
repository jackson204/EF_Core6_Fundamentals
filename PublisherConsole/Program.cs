// See https://aka.ms/new-console-template for more information
using PublisherData;

Console.WriteLine("Hello, World!");

using (var context = new PubContext())
{
    context.Database.EnsureCreated();
}


using (var context = new PubContext())
{
    var authors = context.Authors.ToList();
    foreach (var author in authors)
    {
        Console.WriteLine(author.FirstName + " " + author.LastName);
    }
}