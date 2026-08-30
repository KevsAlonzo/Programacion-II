
/**
 * Write a description of class Documento here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class Documento {
    private String numeroFactura;
    private double totalPagado;
    private boolean estaPagada;
    // Nueva relación: La factura contiene el detalle del carrito pagado
    private Carrito carritoPagado; 

    public Documento(String numeroFactura, Carrito carritoPagado, double totalPagado) {
        this.numeroFactura = numeroFactura;
        this.carritoPagado = carritoPagado;
        this.totalPagado = totalPagado;
        this.estaPagada = true; 
    }
}