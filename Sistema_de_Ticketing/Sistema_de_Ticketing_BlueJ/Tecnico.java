
/**
 * 
 * @author Kevin Alonzo
 */
public class Tecnico extends Persona {
    private String especialidad;
    private int nivelSoporte;

    // Polimorfismo: Su propia versión de registrar()
    @Override
    public void registrar() {
        System.out.println("Registrando técnico especialista en: " + especialidad);
    }
}