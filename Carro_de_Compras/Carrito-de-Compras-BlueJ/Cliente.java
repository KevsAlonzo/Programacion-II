
/**
 * Write a description of class Cliente here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class Cliente {
    private String nombreCompleto;
    private Tarjeta metodoPago;       
    private Carrito carritoCompras;   

    public Cliente(String nombreCompleto, Tarjeta metodoPago) {
        this.nombreCompleto = nombreCompleto;
        this.metodoPago = metodoPago;
        this.carritoCompras = new Carrito(); // Composición estricta
    }
}