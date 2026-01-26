namespace front.Models
{
    public class LoginViewModel
    {
        public string Login { get; set; } = string.Empty;
        public string MotDePasse { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
    }
}
