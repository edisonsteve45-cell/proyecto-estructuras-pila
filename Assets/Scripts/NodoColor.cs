using UnityEngine;

public class NodoColor
{
    public Color color;
    public NodoColor siguiente;
    public NodoColor anterior;

    public NodoColor(Color c)
    {
        color = c;
        siguiente = null;
        anterior = null;
    }
}