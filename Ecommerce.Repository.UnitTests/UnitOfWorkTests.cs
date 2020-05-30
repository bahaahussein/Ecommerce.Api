using Ecommerce.SqlData;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ecommerce.Repository.UnitTests
{
    public class UnitOfWorkTests
    {
        [Fact]
        public async Task SaveChangesAsync_WhenCalled_ShouldCallSaveDbContext()
        {
            var sqlDbContextMock = new Mock<SqlDbContext>();
            var unitOfWork = new UnitOfWork(sqlDbContextMock.Object);

            await unitOfWork.SaveChangesAsync();

            sqlDbContextMock.Verify(x => x.SaveChangesAsync(default(CancellationToken)), Times.Once);
        }
    }
}