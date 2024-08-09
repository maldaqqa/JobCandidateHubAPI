using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using JobCandidateHubAPI.Controllers;
using JobCandidateHubAPI.Models;
using JobCandidateHubAPI.Services;
using Xunit;
using Microsoft.AspNetCore.Http;

public class CandidatesControllerTests
{
    private readonly CandidatesController _controller;
    private readonly Mock<ICandidateService> _serviceMock;
    private readonly Mock<ILogger<CandidatesController>> _loggerMock;

    public CandidatesControllerTests()
    {
        _serviceMock = new Mock<ICandidateService>();
        _loggerMock = new Mock<ILogger<CandidatesController>>();
        _controller = new CandidatesController(_serviceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task AddOrUpdateCandidate_ShouldReturnOk_WhenDtoIsValid()
    {
        var candidateDto = new CandidateDto("Mohammad", "Al-Daqqa", "mohaldaqqa@gmail.com", "Great candidate",
            "+962798288727", "9AM - 5PM", "https://www.linkedin.com/in/mohammad-aldaqqa\r\n", "https://github.com/maldaqqa\r\n");
        _serviceMock.Setup(service => service.AddOrUpdateCandidateAsync(candidateDto))
                    .Returns(Task.CompletedTask);

        var result = await _controller.AddOrUpdateCandidate(candidateDto);

        var okResult = Assert.IsType<OkResult>(result);
        Assert.NotNull(okResult);
    }

    [Fact]
    public async Task AddOrUpdateCandidate_ShouldReturnBadRequest_WhenEmailIsInvalid()
    {
        var candidateDto = new CandidateDto("Mohammad", "Al-Daqqa", "invalid-email", "Great candidate");

        _controller.ModelState.AddModelError("Email", "Invalid email address.");

        var result = await _controller.AddOrUpdateCandidate(candidateDto);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(badRequestResult);

        var errors = (SerializableError)badRequestResult.Value;
        var emailErrors = errors["Email"] as IEnumerable<string>;

        Assert.NotNull(emailErrors);
        Assert.Contains("Invalid email address.", emailErrors);
    }


    [Fact]
    public async Task AddOrUpdateCandidate_ShouldReturnBadRequest_WhenRequiredFieldsAreMissing()
    {
        var candidateDto = new CandidateDto(null, "Al-Daqqa", "mohaldaqqa@gmail.com", "Great candidate");
        _controller.ModelState.AddModelError("FirstName", "First name is required.");

        var result = await _controller.AddOrUpdateCandidate(candidateDto);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(badRequestResult);

        var errors = (SerializableError)badRequestResult.Value;
        var firstNameErrors = errors["FirstName"] as IEnumerable<string>;

        Assert.NotNull(firstNameErrors);
        Assert.Contains("First name is required.", firstNameErrors);
    }


    [Fact]
    public async Task AddOrUpdateCandidate_ShouldCallServiceMethodOnce_WhenDtoIsValid()
    {
        var candidateDto = new CandidateDto("Mohammad", "Al-Daqqa", "mohaldaqqa2@gmail.com", "Great candidate");
        _serviceMock.Setup(service => service.AddOrUpdateCandidateAsync(candidateDto))
                    .Returns(Task.CompletedTask);

        await _controller.AddOrUpdateCandidate(candidateDto);

        _serviceMock.Verify(service => service.AddOrUpdateCandidateAsync(candidateDto), Times.Once);
    }

    [Fact]
    public async Task AddOrUpdateCandidate_ShouldReturnInternalServerError_WhenServiceThrowsException()
    {
        var candidateDto = new CandidateDto("Mohammad", "Al-Daqqa", "mohaldaqqa3@gmail.com", "Great candidate");
        _serviceMock.Setup(service => service.AddOrUpdateCandidateAsync(candidateDto))
                    .ThrowsAsync(new Exception("Service error"));

        var result = await _controller.AddOrUpdateCandidate(candidateDto);

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        var responseMessage = objectResult.Value as string;
        Assert.NotNull(responseMessage);
        Assert.Equal("An error occurred while processing your request.", responseMessage);
    }

}
