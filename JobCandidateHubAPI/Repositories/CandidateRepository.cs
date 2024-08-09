using JobCandidateHubAPI.Controllers;
using JobCandidateHubAPI.Data;
using JobCandidateHubAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobCandidateHubAPI.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CandidatesController> _logger;


        public CandidateRepository(ApplicationDbContext context, ILogger<CandidatesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Candidate?> GetByEmailAsync(string email)
        {
            try
            {
                return await _context.Candidates.FirstOrDefaultAsync(c => c.Email == email);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while accessing the database.");
                throw;
            }
        }

        public async Task AddAsync(Candidate candidate)
        {
            try
            {
                await _context.Candidates.AddAsync(candidate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while adding a candidate to the database.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding a candidate.");
                throw;
            }
        }

        public async Task UpdateAsync(Candidate candidate)
        {
            try
            {
                _context.Candidates.Update(candidate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "A concurrency error occurred while updating a candidate.");
                throw;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while updating a candidate in the database.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating a candidate.");
                throw;
            }
        }

    }
}
