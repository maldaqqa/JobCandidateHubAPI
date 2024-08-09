using JobCandidateHubAPI.Models;
using JobCandidateHubAPI.Repositories;
using AutoMapper;
using JobCandidateHubAPI.Controllers;

namespace JobCandidateHubAPI.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CandidatesController> _logger;

        public CandidateService(ICandidateRepository repository, IMapper mapper, ILogger<CandidatesController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddOrUpdateCandidateAsync(CandidateDto candidateDto)
        {
            try
            {
                var existingCandidate = await _repository.GetByEmailAsync(candidateDto.Email);
                if (existingCandidate == null)
                {
                    var candidate = _mapper.Map<Candidate>(candidateDto);
                    await _repository.AddAsync(candidate);
                }
                else
                {
                    _mapper.Map(candidateDto, existingCandidate);
                    await _repository.UpdateAsync(existingCandidate);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the AddOrUpdateCandidateAsync method.");
                throw;
            }
        }

    }
}
