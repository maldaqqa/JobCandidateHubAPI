using JobCandidateHubAPI.Models;

namespace JobCandidateHubAPI.Repositories
{
    public interface ICandidateRepository
    {
        Task<Candidate?> GetByEmailAsync(string email);
        Task AddAsync(Candidate candidate);
        Task UpdateAsync(Candidate candidate);
    }
}
