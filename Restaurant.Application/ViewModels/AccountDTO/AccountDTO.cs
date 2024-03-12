
namespace Application.ViewModels.AccountDTO
{
    public class AccountDTO
    {
        public int Id { get; set; }

        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Role { get; set; }
        public bool? IsDeleted { get; set; }
    }
}