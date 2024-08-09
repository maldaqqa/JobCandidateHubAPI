using JobCandidateHubAPI.Models;
using JobCandidateHubAPI.Repositories;
using AutoMapper;

namespace JobCandidateHubAPI.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _repository;
        private readonly IMapper _mapper;

        public CandidateService(ICandidateRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddOrUpdateCandidateAsync(CandidateDto candidateDto)
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
    }
}
