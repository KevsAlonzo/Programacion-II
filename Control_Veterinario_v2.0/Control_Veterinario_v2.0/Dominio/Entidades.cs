namespace SistemaVeterinaria.Dominio
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
    }

    public class Mascota
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Raza { get; set; }
        public double Peso { get; set; }
        public string UltimaVacuna { get; set; }
        public bool TieneCollar { get; set; }
    }

    public class Consulta
    {
        public int Id { get; set; }
        public int MascotaId { get; set; }
        public int VeterinarioId { get; set; }
        public string Motivo { get; set; }
    }
}