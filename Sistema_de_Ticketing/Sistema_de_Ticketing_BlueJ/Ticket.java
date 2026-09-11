
/**
 * 
 * @author Kevin Alonzo
 */
import java.util.List;
import java.util.ArrayList;

public class Ticket {
    private String titulo;
    private String estado;
    private String prioridad;
    
    // Asociaciones
    private Solicitante creador;
    private Tecnico asignado;
    
    // Composición
    private List<Bitacora> historialBitacora;

    public Ticket(Solicitante creador) {
        this.creador = creador;
        this.historialBitacora = new ArrayList<>();
    }
    
    public void registrarError(String tipo, String descripcion) {
        // Lógica para añadir a la bitácora
    }
}