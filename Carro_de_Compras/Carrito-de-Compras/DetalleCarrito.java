
/**
 * Write a description of class DetalleCarrito here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class DetalleCarrito {
    // Asociación por referencia con Producto
    private Producto producto; 
    private int cantidad;

    public DetalleCarrito(Producto producto, int cantidad) {
        this.producto = producto;
        this.cantidad = cantidad;
    }
}