namespace JobCandidateHubAPI.Models
{
    public class Candidate
    {
        public int Id { get; set; }

        //Required fields, non nullable
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;

        //Optional fields, nullable
        public string? PhoneNumber { get; set; }
        public string? CallInterval { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? GitHubUrl { get; set; }
    }
}
