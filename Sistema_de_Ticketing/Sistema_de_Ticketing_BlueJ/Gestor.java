
/**
 * 
 * @author Kevin Alonzo
 */
import java.util.List;

public class Gestor {
    // Maneja listas de la clase padre y de tickets
    private List<Tecnico> lstTecnicos;
    private List<Solicitante> lstSolicitantes;
    private List<Ticket> lstTickets;
    
    // Asociación con el flujo
    private FlujoTicket controlFlujo;

    public Ticket crearTicket(String titulo, Solicitante sol) {
        return new Ticket(sol);
    }
}