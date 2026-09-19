
/**
 * Write a description of class Carnet here.
 * 
 * @author Kevin Alonzo
 */
public class Consulta {
    private String motivoVisita; // Preventivo o Enfermedad
    private String sintomas; // Ejemplo: vómitos, problemas estomacales
    private String diagnostico; // Ej: Gastritis
    
    // Asociaciones
    private Mascota paciente;
    private Veterinario doctorAsignado;

    public Consulta(Mascota paciente, Veterinario doctorAsignado, String motivoVisita) {
        this.paciente = paciente;
        this.doctorAsignado = doctorAsignado;
        this.motivoVisita = motivoVisita;
    }
    
    public void registrarDiagnostico(String sintomas, String diagnostico) {
        this.sintomas = sintomas;
        this.diagnostico = diagnostico;
    }
}