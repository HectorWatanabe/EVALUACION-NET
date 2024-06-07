using ExperisEvaluacionAPI.AppSettingsDTOs;

namespace ExperisEvaluacionAPI
{
    public class AppSettings
    {
        public IConfiguration ConfigRoot { get; }

        public AppSettings(IConfiguration configuration)
        {
            ConfigRoot = configuration;
        }

        public JwtAppSettingsDTO GetJwtAppSetting() => ConfigRoot.GetSection("Jwt").Get<JwtAppSettingsDTO>();
    }
}
