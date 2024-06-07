namespace ExperisEvaluacionAPI.AppSettingsDTOs
{
    public class JwtAppSettingsDTO
    {
        public string AccessTokenSecret { get; set; }
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}
