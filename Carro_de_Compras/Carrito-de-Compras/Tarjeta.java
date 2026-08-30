
/**
 * Write a description of class Tarjeta here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class Tarjeta {
    // Modificadores private (-)
    private String numero;
    private String pin;
    private boolean bloqueada;
    private int intentosFallidos;

    // Constructor
    public Tarjeta(String numero, String pin) {
        this.numero = numero;
        this.pin = pin;
        this.bloqueada = false;
        this.intentosFallidos = 0;
    }

    // Método public (+) para validar
    public boolean validarPin(String pinIngresado) {
        if (bloqueada) {
            return false;
        }
        if (this.pin.equals(pinIngresado)) {
            intentosFallidos = 0;
            return true;
        }
        intentosFallidos++;
        if (intentosFallidos >= 3) {
            bloqueada = true;
        }
        return false;
    }
}