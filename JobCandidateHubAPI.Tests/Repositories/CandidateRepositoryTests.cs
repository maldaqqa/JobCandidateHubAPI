using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using JobCandidateHubAPI.Data;
using JobCandidateHubAPI.Models;
using JobCandidateHubAPI.Repositories;
using Xunit;
using Moq;
using JobCandidateHubAPI.Controllers;

public class CandidateRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly CandidateRepository _repository;
    private readonly Mock<ILogger<CandidatesController>> _loggerMock;

    public CandidateRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ApplicationDbContext(options);
        _loggerMock = new Mock<ILogger<CandidatesController>>();
        _repository = new CandidateRepository(_context, _loggerMock.Object);
    }

    [Fact]
    public async Task AddAsync_ShouldAddCandidate()
    {
        Random rnd = new Random();
        int randomNum = rnd.Next(1, 1000);
        var candidate = new Candidate { Email = "mohaldaqqa" + randomNum.ToString() + "@gmail.com", FirstName = "Mohammad", LastName = "Al-Daqqa", Comment = "Great candidate" };

        await _repository.AddAsync(candidate);

        var retrievedCandidate = await _context.Candidates.FindAsync(candidate.Id);
        Assert.NotNull(retrievedCandidate);
        Assert.Equal(candidate.Email, retrievedCandidate.Email);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateCandidate()
    {
        var candidate = new Candidate { Email = "mohaldaqqa@gmail.com", FirstName = "Mohammad", LastName = "Al-Daqqa", Comment = "Great candidate" };
        await _repository.AddAsync(candidate);

        candidate.Comment = "Updated comment";

        await _repository.UpdateAsync(candidate);

        var updatedCandidate = await _context.Candidates.FindAsync(candidate.Id);
        Assert.Equal("Updated comment", updatedCandidate.Comment);
    }
}
