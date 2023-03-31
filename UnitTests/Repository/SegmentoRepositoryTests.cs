using Domain.Models;
using Infra.Data.Context;
using Infra.Data.Repository;

namespace UnitTests.Repository;

public class SegmentoRepositoryTests
{
    #region Add
        [Fact]
        public async void Add_SalvarNovo_success()
        {
            // Given
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(service=>service.SaveChangesAsync(It.IsAny<CancellationToken>()))
                        .ReturnsAsync(1);
            var repository = new SegmentoRepository(mockContext.Object);
            // When
            Task addTask = repository.Add(new Segmento());
            addTask.Wait();
            // Then
            Assert.True(addTask.IsCompletedSuccessfully);
        }
    #endregion
}
