using Microsoft.EntityFrameworkCore;
using PublisherData;

namespace InMemoryTests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        var builder = new DbContextOptionsBuilder<PubContext>();
        builder.UseInMemoryDatabase("TestDb");
    }
}
