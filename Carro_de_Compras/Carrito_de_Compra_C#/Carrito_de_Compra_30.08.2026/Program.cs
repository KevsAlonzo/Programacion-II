using System;
using System.Collections.Generic;
namespace Carrito.Logica;

public class Producto
    {
        public string strCodigo { get; set; }
        public string strNombre { get; set; }
        public double dblPrecio { get; set; }
        public int intStock {  get; set; }
        //Constructor
        public Producto(string strCodigo, string strNombre, double dblPrecio, int intStock)
        {
            this.strCodigo = strCodigo;
            this.strNombre = strNombre;
            this.dblPrecio = dblPrecio;
            this.intStock = intStock;
        }

        public bool verificarStock(int intcantidad)
        {
            return intcantidad > 0 && this.intStock >= intcantidad;
        }
        public void mostrarProducto()
        {
            Console.WriteLine($"{strNombre}{intStock}");
        }
    }

    public class  Detalle
    {
        public Producto objProducto { get; set; }
        public int intCantidad { get; set; }
        //Constructor
        public Detalle(Producto objProducto, int intCantidad)
        {
            this.objProducto = objProducto;
            this.intCantidad = intCantidad;
        }
        public double calcularSubtotal()
        {
            if (objProducto == null) return 0.0;
            return objProducto.dblPrecio * intCantidad;
        }

        public void mostrarDetalle()
        {
            double dblSubtotal = calcularSubtotal();
            Console.WriteLine($"{objProducto.strNombre}{dblSubtotal}");
        }
    }


public class Carrito
{
    public List<Detalle> lstDetalle { get; set; }
    //Constructor
    public Carrito() 
    { 
        this.lstDetalle = new List<Detalle>(); 
    }

    public bool agregarCarrito(int intCantidad, Producto objProducto)
    {
        //Validaciones
        if (!objProducto.verificarStock(intCantidad))
            { 
               Console.WriteLine("Sin stock");
               return false;
            }

        lstDetalle.Add(new Detalle(objProducto, intCantidad));
    }

    public void mostrarCarrito()
    {
        if(lstDetalle.Count > 0)
        {
            //Error
            return;
        }
        foreach (var item in lstDetalle)
        {
            item.mostrarDetalle();
        }
    }
}