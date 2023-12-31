namespace PublisherDomain;

public class Book
{
    public int BookId { get; set; }
    
    public string Title { get; set; }
    
    public DateTime PublishedOn { get; set; }

    public decimal BasePrice { get; set; }
    
    public virtual Author Author { get; set; }
    
    public int? AuthorId { get; set; }
}
