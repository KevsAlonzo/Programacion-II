
/**
 *
 * @author Kevin Alonzo
 */
public abstract class Persona {
    // Atributos
    protected String id;
    protected String nombre;
    protected String correo;

    // Polimorfismo
    public abstract void registrar();
    
    public void mostrarInformacion() {
        System.out.println("ID: " + id + " | Nombre: " + nombre);
    }
}