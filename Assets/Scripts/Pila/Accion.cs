using UnityEngine;

public class Accion
{
    public Vector2Int posicion;
    public Color colorBloque;
    public GameObject objetoVisual;

    public Accion(Vector2Int pos, Color color, GameObject obj)
    {
        posicion = pos;
        colorBloque = color;
        objetoVisual = obj;
    }
}