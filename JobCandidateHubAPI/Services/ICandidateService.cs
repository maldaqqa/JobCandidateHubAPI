using JobCandidateHubAPI.Models;

namespace JobCandidateHubAPI.Services
{
    public interface ICandidateService
    {
        Task AddOrUpdateCandidateAsync(CandidateDto candidateDto);
    }
}
