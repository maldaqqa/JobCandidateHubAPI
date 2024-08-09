using System.ComponentModel.DataAnnotations;

namespace JobCandidateHubAPI.Models
{
    public class CandidateDto
    {
        public CandidateDto(string firstName, string lastName, string email, string comment,
            string? phoneNumber = null, string? callInterval = null,
            string? linkedInUrl = null, string? gitHubUrl = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Comment = comment;
            PhoneNumber = phoneNumber;
            CallInterval = callInterval;
            LinkedInUrl = linkedInUrl;
            GitHubUrl = gitHubUrl;
        }

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Comment is required.")]
        public string Comment { get; set; }

        public string? PhoneNumber { get; set; }
        public string? CallInterval { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? GitHubUrl { get; set; }
    }
}
