using AutoMapper;
using JobCandidateHubAPI.Models;

namespace JobCandidateHubAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CandidateDto, Candidate>();
        }
    }
}
