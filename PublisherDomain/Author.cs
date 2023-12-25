using Microsoft.VisualBasic;

namespace PublisherDomain;

public class Author
{
    public Author()
    {
        Books = new HashSet<Book>();
    }

    public int AuthorId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public virtual ICollection<Book> Books { get; set; }
}
