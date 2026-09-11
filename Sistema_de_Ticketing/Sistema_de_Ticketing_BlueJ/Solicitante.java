
/**
 * 
 * @author Kevin Alonzo
 */
public class Solicitante extends Persona {
    private String departamento;
    private String extension;

    // Polimorfismo
    @Override
    public void registrar() {
        System.out.println("Registrando solicitante del departamento: " + departamento);
    }
}