namespace PublisherDomain;

public class Author
{
    public Author()
    {
        Book = new List<Book>();
    }

    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public List<Book> Book { get; set; }
}
