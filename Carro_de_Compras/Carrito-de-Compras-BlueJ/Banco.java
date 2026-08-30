
/**
 * Write a description of class Banco here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class Banco {
    private String nombreBanco;

    public Banco(String nombreBanco) {
        this.nombreBanco = nombreBanco;
    }

    // Método que simula la acción "PAGAR" del diagrama de flujo
    public Documento procesarPago(Tarjeta tarjeta, Carrito carrito, double monto, String pinIngresado) {
        // Validamos el PIN usando la lógica del simulador
        if (tarjeta.validarPin(pinIngresado)) {
            System.out.println("Pago aprobado.");
            // Nace el documento (factura) tras la compra
            return new Documento("FAC-001", carrito, monto);
        } else {
            System.out.println("Pago rechazado. Tarjeta bloqueada o PIN incorrecto.");
            return null;
        }
    }
}