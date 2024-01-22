namespace Restaurant.Application.Services
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string? Message { get; set; } = null;
        public List<string>? ErrorMessages { get; set; } = null;
    }
}