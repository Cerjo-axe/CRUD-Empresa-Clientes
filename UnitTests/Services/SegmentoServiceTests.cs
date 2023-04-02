using AutoMapper;
using Domain.Interfaces.Repository;
using Domain.Models;
using DTO;
using Microsoft.EntityFrameworkCore;
using Services.Services;
using UnitTests.Helper;

namespace UnitTests.Services;

public class SegmentoServiceTests
{
    private readonly IMapper _mapper;

    public SegmentoServiceTests()
    {
        var config = new MapperConfiguration(cfg=>{
            cfg.CreateMap<SegmentoDTO,Segmento>().ReverseMap();
        });
        _mapper = config.CreateMapper();
    }

    #region AddSegment
        [Fact]
        public async Task AddSegment_InvalidObj_ThowsException()
        {
            // Given
            var mockRepo = GetRepositoryMock();
            var service = new SegmentoService(mockRepo.Object,_mapper);
            // When
            var exception = await Record.ExceptionAsync(()=>service.AddSegment(DummyData.SegmentoInvalido));
            // Then
            Assert.IsType<FluentValidation.ValidationException>(exception);
        }

        [Fact]
        public async Task AddSegment_ValidObj_Failed()
        {
            // Given
            var mockRepo=GetRepositoryMock();
            mockRepo.Setup(c=>c.Add(It.IsAny<Segmento>()))
                    .Throws(new DbUpdateException());
            var service = new SegmentoService(mockRepo.Object,_mapper);
            // When
            var exception = await Record.ExceptionAsync(()=>service.AddSegment(DummyData.SegmentoValido));
            // Then
            Assert.IsType<DbUpdateException>(exception);
        }

        [Fact]
        public async Task AddSegment_ValidObj_Success()
        {
            // Given
            var mockRepo=GetRepositoryMock();
            var service = new SegmentoService(mockRepo.Object,_mapper);
            // When
            var obj = DummyData.SegmentoValido;
            obj.Id= new Guid().ToString();
            Task add = service.AddSegment(obj);
            add.Wait();
            // Then
            Assert.Equal(TaskStatus.RanToCompletion,add.Status);
        }
    #endregion

    #region GetSegmento 

    [Fact]
    public async Task GetSegmento_invalidid_dontfind()
    {
        // Given
        var mockRepo = GetRepositoryMock();
        mockRepo.Setup(c=>c.GetbyId(It.IsAny<string>()))
                .ReturnsAsync((Segmento)null);
                var service = new SegmentoService(mockRepo.Object,_mapper);
        // When
        var result = await service.GetSegmento(new Guid().ToString());
        // Then
        Assert.Null(result);
    }

    [Fact]
    public async Task GetSegmento_validid_returnisnotnullanddto()
    {
        // Given
        var mockRepo = GetRepositoryMock();
        mockRepo.Setup(c=>c.GetbyId(It.IsAny<string>()))
                .ReturnsAsync(new Segmento());
                var service = new SegmentoService(mockRepo.Object,_mapper);
        // When
        var result = await service.GetSegmento(new Guid().ToString());
        // Then
        Assert.NotNull(result);
        Assert.IsType<SegmentoDTO>(result);
    }
        
    #endregion
    
    #region GetSegmentos
        [Fact]
        public async Task GetSegmentos_ReturnNull()
        {
            // Given
            var mockRepo = GetRepositoryMock();
            mockRepo.Setup(c=>c.GetAll())
                    .ReturnsAsync((List<Segmento>)null);
                    var service = new SegmentoService(mockRepo.Object,_mapper);
            // When
            var result = await service.GetSegmentos();
            // Then
            Assert.Null(result);
        }
        [Fact]
        public async Task GetSegmentos_ReturnList()
        {
            // Given
            var mockRepo = GetRepositoryMock();
            mockRepo.Setup(c=>c.GetAll())
                    .ReturnsAsync(new List<Segmento>());
                    var service = new SegmentoService(mockRepo.Object,_mapper);
            // When
            var result = await service.GetSegmentos();
            // Then
            Assert.NotNull(result);
            Assert.IsType<List<SegmentoDTO>>(result);
        }
    #endregion
    
    #region UpdateSegment
        [Fact]
        public async Task UpdateSegment_InvalidObj_ThowsException()
        {
            // Given
            var mockRepo = GetRepositoryMock();
            var service = new SegmentoService(mockRepo.Object,_mapper);
            // When
            var exception = await Record.ExceptionAsync(()=>service.UpdateSegment(DummyData.SegmentoInvalido));
            // Then
            Assert.IsType<FluentValidation.ValidationException>(exception);
        }
        [Fact]
        public async Task UpdateSegment_ValidObj_Failed()
        {
            // Given
            var mockRepo=GetRepositoryMock();
            mockRepo.Setup(c=>c.GetbyId(It.IsAny<string>()))
                    .ReturnsAsync(new Segmento());
            mockRepo.Setup(c=>c.Update(It.IsAny<Segmento>()))
                    .Throws(new DbUpdateException());
            var service = new SegmentoService(mockRepo.Object,_mapper);
            // When
            DummyData.SegmentoValido.Id= new Guid().ToString();
            var exception = await Record.ExceptionAsync(()=>service.UpdateSegment(DummyData.SegmentoValido));
            // Then
            Assert.IsType<DbUpdateException>(exception);
        }
        [Fact]
        public async Task UpdateSegment_ValidObj_Success()
        {
            // Given
            var mockRepo=GetRepositoryMock();
            mockRepo.Setup(c=>c.GetbyId(It.IsAny<string>()))
                    .ReturnsAsync(new Segmento());
            var service = new SegmentoService(mockRepo.Object,_mapper);
            // When
            DummyData.SegmentoValido.Id= new Guid().ToString();
            Task add = service.UpdateSegment(DummyData.SegmentoValido);
            add.Wait();
            // Then
            Assert.Equal(TaskStatus.RanToCompletion,add.Status);
        }
    #endregion

    #region DeleteSegment
        [Fact]
        public async Task DeleteSegment_ValidObj_Failed()
        {
            // Given
            var mockRepo=GetRepositoryMock();
            mockRepo.Setup(c=>c.GetbyId(It.IsAny<string>()))
                    .ReturnsAsync(new Segmento());
            mockRepo.Setup(c=>c.Delete(It.IsAny<Segmento>()))
                    .Throws(new DbUpdateException());
            var service = new SegmentoService(mockRepo.Object,_mapper);
            // When
            DummyData.SegmentoValido.Id= new Guid().ToString();
            var exception = await Record.ExceptionAsync(()=>service.DeleteSegment(DummyData.SegmentoValido.Id));
            // Then
            Assert.IsType<DbUpdateException>(exception);
        }
        [Fact]
        public async Task DeleteSegment_ValidObj_Success()
        {
            // Given
            var mockRepo=GetRepositoryMock();
            mockRepo.Setup(c=>c.GetbyId(It.IsAny<string>()))
                    .ReturnsAsync(new Segmento());
            var service = new SegmentoService(mockRepo.Object,_mapper);
            // When
            DummyData.SegmentoValido.Id= new Guid().ToString();
            Task add = service.DeleteSegment(DummyData.SegmentoValido.Id);
            add.Wait();
            // Then
            Assert.Equal(TaskStatus.RanToCompletion,add.Status);
        }
    #endregion
    private Mock<ISegmentoRepository> GetRepositoryMock()
    {
       return new Mock<ISegmentoRepository>(); 
    }
}
