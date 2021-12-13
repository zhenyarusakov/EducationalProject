

using System.Threading.Tasks;
using EC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ep.Infrastructure.Tests.Helpers
{
    public static class DbContextFactory
    {
        private const string DbName = "EducationalProject";

        public static async Task<EPContext> CreateContext()
        {
            DbContextOptions<EPContext> builder = new DbContextOptionsBuilder<EPContext>()
                .UseInMemoryDatabase(DbName)
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            EPContext context = new EPContext(builder);
            return context;
        }
    }
}