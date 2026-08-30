
/**
 * Write a description of class Carrito here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
import java.util.List;
import java.util.ArrayList;

public class Carrito {
    // Composición Fuerte con DetalleCarrito
    private List<DetalleCarrito> listaDetalles;

    public Carrito() {
        // La lista nace estrictamente con el Carrito
        this.listaDetalles = new ArrayList<>(); 
    }

    // Método público (+)
    public double calcularSubtotal() {
        return 0.0; // Lógica a implementar luego en C#
    }
}