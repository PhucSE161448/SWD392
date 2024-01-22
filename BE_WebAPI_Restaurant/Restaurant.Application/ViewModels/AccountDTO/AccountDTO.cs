
namespace Application.ViewModels.AccountDTO
{
    public class AccountDTO
    {
        public int Id { get; set; }

        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? PasswordHash { get; set; }
        public int Status { get; set; }
        public string? PhoneNumber { get; set; }
        public string? RoleName { get; set; }
    }
}