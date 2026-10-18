namespace PrestamoBiblioteca.Dominio
{
    // Clase base para demostrar herencia y polimorfismo
    public abstract class Persona
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }

        protected Persona(string codigo, string nombre, string correo, string telefono)
        {
            Codigo = codigo;
            Nombre = nombre;
            Correo = correo;
            Telefono = telefono;
        }

        public abstract string ObtenerInformacion();
    }

    // Clase derivada Lector
    public class Lector : Persona
    {
        public Lector(string codigo, string nombre, string correo, string telefono)
            : base(codigo, nombre, correo, telefono)
        {
        }

        public override string ObtenerInformacion()
        {
            return $"Lector: {Nombre} | Codigo: {Codigo} | Correo: {Correo}";
        }
    }
}
