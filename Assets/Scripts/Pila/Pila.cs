public class Pila
{
    private Nodo cima;
    private int contador;

    public Pila()
    {
        cima = null;
        contador = 0;
    }

    public void Push(Accion nuevaAccion)
    {
        Nodo nuevoNodo = new Nodo(nuevaAccion);
        nuevoNodo.siguiente = cima;
        cima = nuevoNodo;
        contador++;
    }

    public Accion Pop()
    {
        if (EstaVacia()) return null;

        Accion accionRemovida = cima.dato;
        cima = cima.siguiente;
        contador--;
        return accionRemovida;
    }

    public bool EstaVacia()
    {
        return cima == null;
    }

    public int ObtenerTamaño()
    {
        return contador;
    }

    
}