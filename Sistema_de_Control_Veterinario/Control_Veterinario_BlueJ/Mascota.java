
/**
 * Write a description of class Carnet here.
 * 
 * @author Kevin Alonzo
 */
public class Mascota {
    // Atributos privados
    private String nombre;
    private String raza; // Tomado del audio
    private double peso; // Tomado del triaje
    private Cliente dueno; // Asociación
    private Carnet carnetVacunacion; // Composición

    public Mascota(String nombre, String raza, Cliente dueno) {
        this.nombre = nombre;
        this.raza = raza;
        this.dueno = dueno;
        this.carnetVacunacion = new Carnet(); // Nace con su carnet
    }
    
    // Método público
    public void actualizarPeso(double nuevoPeso) {
        this.peso = nuevoPeso;
    }
}