
/**
 * Write a description of class Carnet here.
 * 
 * @author Kevin Alonzo
 */
public class Carnet {
    private String ultimaVacuna;
    private boolean tieneCollarRabia; // Tomado del audio
    
    public Carnet() {
        this.ultimaVacuna = "Ninguna";
        this.tieneCollarRabia = false;
    }
    
    public void registrarVacuna(String nombreVacuna, boolean collarRabia) {
        this.ultimaVacuna = nombreVacuna;
        this.tieneCollarRabia = collarRabia;
    }
}