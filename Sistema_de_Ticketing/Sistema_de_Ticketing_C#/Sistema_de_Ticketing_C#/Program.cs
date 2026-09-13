namespace TicketSoporte.logica
{
    public class Usuario
    {
        public string strCodigo { get; set; }
        public string strNombre { get; set; }
        public string strCorreo { get; set; }
        public Usuario (string strCodigo, string strNombre, string strCorreo)
        {
            this.strCodigo = strCodigo;
            this.strNombre = strNombre;
            this.strCorreo = strCorreo;
        }
        public abstract obtenerRol()
    }
}

namespace SoporteTicket.Logica
{
    public list<Tecnico> lstTecnicos { get; set; };
    public list<Solicitante> lstSolicitantes { get; set; };
    public list<Ticket> lstTickets { get; set; };
    public GestorTicket()
    {
        lstTecnicos = new list<Tecnico>();
        lstSolicitantes = new list<Solicitante>();
        lstTickets = new list<Ticket>();
    }
    public Ticket crearTicket(int intnumero, string strAsunto, string strDescripcion, string strCategoria, string strPrioridad, Solicitante objSolicitante, Tecnico objTecnico)
    {
        //Agregar todas las validaciones restantes en el código
        Ticket objTicket = new Ticket(intnumero, strAsunto, strDescripcion, strCategoria, strPrioridad, objSolicitante);
        //Agregar un método para asignar al mejor técnico disponible según la categoría del ticket (prioridad, en turno, etc.)
        objTicket.asignarTecnico(objTecnico);
        lstTickets.add(objTicket);
        return objTicket;
    }
    public Ticket buscarTicket(int intnumero)
    {
        Ticket objTicket = lstTickets.find(t => t.intnumero == intnumero);
        if (objTicket == null)
        {
            Console.WriteLine("Ticket no existe.");
            return objTicket;
        }
    }
}

//Hace falra agregar los metodos restantes para que el programa funcione correctamente y que compile.