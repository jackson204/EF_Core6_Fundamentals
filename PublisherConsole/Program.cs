// See https://aka.ms/new-console-template for more information
using PublisherData;

Console.WriteLine("Hello, World!");

using (var context = new PubContext())
{
    context.Database.EnsureCreated();
}