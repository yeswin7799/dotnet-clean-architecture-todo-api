using Microsoft.EntityFrameworkCore;
using TodoApp.Infrastructure.Persistence;

namespace TodoApp.Tests.Fakes
{
    public static class TestDbFactory
    {
        public static AppDbContext Create()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }
    }
}
