using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using AutoMapper;
using JobCandidateHubAPI.Models;
using JobCandidateHubAPI.Repositories;
using JobCandidateHubAPI.Services;
using Xunit;
using JobCandidateHubAPI.Controllers;

public class CandidateServiceTests
{
    private readonly CandidateService _service;
    private readonly Mock<ICandidateRepository> _repositoryMock;
    private readonly IMapper _mapper;
    private readonly Mock<ILogger<CandidatesController>> _loggerMock;

    public CandidateServiceTests()
    {
        _repositoryMock = new Mock<ICandidateRepository>();
        _loggerMock = new Mock<ILogger<CandidatesController>>();

        var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<CandidateDto, Candidate>());
        _mapper = mapperConfig.CreateMapper();

        _service = new CandidateService(_repositoryMock.Object, _mapper, _loggerMock.Object);
    }

    [Fact]
    public async Task AddOrUpdateCandidateAsync_ShouldAddNewCandidate_WhenCandidateDoesNotExist()
    {
        var candidateDto = new CandidateDto("Mohammad", "Al-Daqqa", "mohaldaqqa22@gmail.com", "Great candidate");
        _repositoryMock.Setup(repo => repo.GetByEmailAsync(candidateDto.Email))
                       .ReturnsAsync((Candidate)null);

        await _service.AddOrUpdateCandidateAsync(candidateDto);

        _repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Candidate>()), Times.Once);
    }

    [Fact]
    public async Task AddOrUpdateCandidateAsync_ShouldUpdateExistingCandidate_WhenCandidateExists()
    {
        var existingCandidate = new Candidate { Email = "mohaldaqqa@gmail.com" };
        var candidateDto = new CandidateDto("Mohammad", "Al-Daqqa", "mohaldaqqa@gmail.com", "Updated comment22");
        _repositoryMock.Setup(repo => repo.GetByEmailAsync(candidateDto.Email))
                       .ReturnsAsync(existingCandidate);

        await _service.AddOrUpdateCandidateAsync(candidateDto);

        _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Candidate>()), Times.Once);
    }
}
