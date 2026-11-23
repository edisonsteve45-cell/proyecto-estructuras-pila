using UnityEngine;

public class ListaCircularColores
{
    private NodoColor actual;

    public ListaCircularColores()
    {
        actual = null;
    }

   
    public void Agregar(Color c)
    {
        NodoColor nuevo = new NodoColor(c);
        
        if (actual == null)
        {
            actual = nuevo;
            actual.siguiente = actual;
            actual.anterior = actual;
        }
        else
        {
            NodoColor ultimo = actual.anterior;
            
            ultimo.siguiente = nuevo;
            nuevo.anterior = ultimo;
            
            nuevo.siguiente = actual;
            actual.anterior = nuevo;
        }
    }

   
    public void Siguiente()
    {
        if (actual != null)
        {
            actual = actual.siguiente;
        }
    }

    public void Anterior()
    {
        if (actual != null)
        {
            actual = actual.anterior;
        }
    }



    public Color ObtenerColorActual()
    {
        return actual != null ? actual.color : Color.red;
    }

}