using System;

namespace PrestamoBiblioteca.Dominio
{
    public class Prestamo
    {
        public int LectorId { get; set; }
        public int LibroId { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaLimite { get; set; }
        public DateTime? FechaDevolucion { get; set; }

        // Si FechaDevolucion es null, el préstamo sigue activo
        public bool EstaActivo => !FechaDevolucion.HasValue;

        public Prestamo(int lectorId, int libroId, DateTime fechaPrestamo, DateTime fechaLimite)
        {
            LectorId = lectorId;
            LibroId = libroId;
            FechaPrestamo = fechaPrestamo;
            FechaLimite = fechaLimite;
            FechaDevolucion = null;
        }
    }
}
