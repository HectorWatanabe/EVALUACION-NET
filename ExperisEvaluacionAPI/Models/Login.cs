namespace ExperisEvaluacionAPI.Models
{
    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RefreshRequest
    {
        public string AccessToken { get; set;}
        public string RefreshToken { get; set;}
    }
}
