public class Nodo
{
    public Accion dato;
    public Nodo siguiente;

    public Nodo(Accion nuevaAccion)
    {
        dato = nuevaAccion;
        siguiente = null;
    }
}