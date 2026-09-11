
/**
 * @author (your name) 
 * @version Kevin Alonzo
 */
public class Bitacora {
    private String fechaHora;
    private String tipoEvento;
    private String descripcion;
    
    public void mostrarRegistro() {
        System.out.println(fechaHora + " - " + tipoEvento + ": " + descripcion);
    }
}