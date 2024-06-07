namespace ExperisEvaluacionAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public char Genero { get; set; }
        public string Roles { get; set; }
        public bool Bloquear { get; set; }
        public DateTime MarcaTemporalCreacion { get; set; }
        public string UsuarioCreador { get; set; }
        public DateTime? MarcaTemporalActualizacion { get; set; }
        public string UsuarioActualizador { get; set; }
        public DateTime? MarcaTemporalEliminado { get; set; }
        public string UsuarioEliminador { get; set; }
        public bool EstadoEliminado { get; set; }
    }
}
