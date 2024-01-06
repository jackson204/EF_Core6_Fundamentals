using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

namespace InMemoryTests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        var builder = new DbContextOptionsBuilder<PubContext>();
        builder.UseInMemoryDatabase("TestDb");
        using (var context = new PubContext(builder.Options))
        {
            var author = new Author() { FirstName = "a", LastName = "b" };
            context.Authors.Add(author);

            Assert.AreEqual(EntityState.Added, context.Entry(author).State);
        }
    }
}
