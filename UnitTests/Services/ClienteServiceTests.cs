using AutoMapper;
using Domain.Interfaces.Repository;
using Domain.Models;
using DTO;
using Microsoft.EntityFrameworkCore;
using Services.Services;
using UnitTests.Helper;

namespace UnitTests.Services;

public class ClienteServiceTests
{
    private readonly IMapper _mapper;

    public ClienteServiceTests()
    {
        var config = new MapperConfiguration(cfg=>{
            cfg.CreateMap<ClienteDTO,Cliente>().ReverseMap();
        });
        _mapper = config.CreateMapper();
    }

    #region AddCliente
        [Fact]
        public async Task AddCliente_InvalidObj_ThowsException()
        {
            // Given
            var mockRepo = GetRepositoryMock();
            var service = new ClienteService(mockRepo.Object,_mapper);
            // When
            var exception = await Record.ExceptionAsync(()=>service.AddCliente(DummyData.ClienteInvalido));
            // Then
            Assert.IsType<FluentValidation.ValidationException>(exception);
        }

        [Fact]
        public async Task AddCliente_ValidObj_Failed()
        {
            // Given
            var mockRepo=GetRepositoryMock();
            mockRepo.Setup(c=>c.Add(It.IsAny<Cliente>()))
                    .Throws(new DbUpdateException());
            var service = new ClienteService(mockRepo.Object,_mapper);
            // When
            var exception = await Record.ExceptionAsync(()=>service.AddCliente(DummyData.ClienteValido));
            // Then
            Assert.IsType<DbUpdateException>(exception);
        }

        [Fact]
        public async Task AddCliente_ValidObj_Success()
        {
            // Given
            var mockRepo=GetRepositoryMock();
            var service = new ClienteService(mockRepo.Object,_mapper);
            // When
            Task add = service.AddCliente(DummyData.ClienteValido);
            add.Wait();
            // Then
            Assert.Equal(TaskStatus.RanToCompletion,add.Status);
        }
    #endregion

    #region GetCliente 

    [Fact]
    public async Task GetCliente_invalidid_dontfind()
    {
        // Given
        var mockRepo = GetRepositoryMock();
        mockRepo.Setup(c=>c.GetbyId(It.IsAny<string>()))
                .ReturnsAsync((Cliente)null);
                var service = new ClienteService(mockRepo.Object,_mapper);
        // When
        var result = await service.GetCliente(new Guid().ToString());
        // Then
        Assert.Null(result);
    }

    [Fact]
    public async Task GetCliente_validid_returnisnotnullanddto()
    {
        // Given
        var mockRepo = GetRepositoryMock();
        mockRepo.Setup(c=>c.GetbyId(It.IsAny<string>()))
                .ReturnsAsync(new Cliente());
                var service = new ClienteService(mockRepo.Object,_mapper);
        // When
        var result = await service.GetCliente(new Guid().ToString());
        // Then
        Assert.NotNull(result);
        Assert.IsType<ClienteDTO>(result);
    }
        
    #endregion
    
    #region GetCliente
        [Fact]
        public async Task GetCliente_ReturnNull()
        {
            // Given
            var mockRepo = GetRepositoryMock();
            mockRepo.Setup(c=>c.GetAll())
                    .ReturnsAsync((List<Cliente>)null);
                    var service = new ClienteService(mockRepo.Object,_mapper);
            // When
            var result = await service.GetClientes();
            // Then
            Assert.Null(result);
        }
        [Fact]
        public async Task GetCliente_ReturnList()
        {
            // Given
            var mockRepo = GetRepositoryMock();
            mockRepo.Setup(c=>c.GetAll())
                    .ReturnsAsync(new List<Cliente>());
                    var service = new ClienteService(mockRepo.Object,_mapper);
            // When
            var result = await service.GetClientes();
            // Then
            Assert.NotNull(result);
            Assert.IsType<List<ClienteDTO>>(result);
        }
    #endregion
    
    #region UpdateCliente
        [Fact]
        public async Task UpdateCliente_InvalidObj_ThowsException()
        {
            // Given
            var mockRepo = GetRepositoryMock();
            var service = new ClienteService(mockRepo.Object,_mapper);
            // When
            var exception = await Record.ExceptionAsync(()=>service.UpdateCliente(DummyData.ClienteInvalido));
            // Then
            Assert.IsType<FluentValidation.ValidationException>(exception);
        }
        [Fact]
        public async Task UpdateCliente_ValidObj_Failed()
        {
            // Given
            var mockRepo=GetRepositoryMock();
            mockRepo.Setup(c=>c.GetbyId(It.IsAny<string>()))
                    .ReturnsAsync(new Cliente());
            mockRepo.Setup(c=>c.Update(It.IsAny<Cliente>()))
                    .Throws(new DbUpdateException());
            var service = new ClienteService(mockRepo.Object,_mapper);
            // When
            DummyData.ClienteValido.Id= new Guid().ToString();
            var exception = await Record.ExceptionAsync(()=>service.UpdateCliente(DummyData.ClienteValido));
            // Then
            Assert.IsType<DbUpdateException>(exception);
        }
        [Fact]
        public async Task UpdateCliente_ValidObj_Success()
        {
            // Given
            var mockRepo=GetRepositoryMock();
            mockRepo.Setup(c=>c.GetbyId(It.IsAny<string>()))
                    .ReturnsAsync(new Cliente());
            var service = new ClienteService(mockRepo.Object,_mapper);
            // When
            DummyData.ClienteValido.Id= new Guid().ToString();
            Task add = service.UpdateCliente(DummyData.ClienteValido);
            add.Wait();
            // Then
            Assert.Equal(TaskStatus.RanToCompletion,add.Status);
        }
    #endregion

    #region DeleteCliente
        [Fact]
        public async Task DeleteCliente_ValidObj_Failed()
        {
            // Given
            var mockRepo=GetRepositoryMock();
            mockRepo.Setup(c=>c.GetbyId(It.IsAny<string>()))
                    .ReturnsAsync(new Cliente());
            mockRepo.Setup(c=>c.Delete(It.IsAny<Cliente>()))
                    .Throws(new DbUpdateException());
            var service = new ClienteService(mockRepo.Object,_mapper);
            // When
            DummyData.ClienteValido.Id= new Guid().ToString();
            var exception = await Record.ExceptionAsync(()=>service.DeleteCliente(DummyData.ClienteValido.Id));
            // Then
            Assert.IsType<DbUpdateException>(exception);
        }
        [Fact]
        public async Task DeleteCliente_ValidObj_Success()
        {
            // Given
            var mockRepo=GetRepositoryMock();
            mockRepo.Setup(c=>c.GetbyId(It.IsAny<string>()))
                    .ReturnsAsync(new Cliente());
            var service = new ClienteService(mockRepo.Object,_mapper);
            // When
            DummyData.ClienteValido.Id= new Guid().ToString();
            Task add = service.DeleteCliente(DummyData.ClienteValido.Id);
            add.Wait();
            // Then
            Assert.Equal(TaskStatus.RanToCompletion,add.Status);
        }
    #endregion
    private Mock<IClienteRepository> GetRepositoryMock()
    {
       return new Mock<IClienteRepository>(); 
    }
}

