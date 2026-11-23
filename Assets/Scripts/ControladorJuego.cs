using UnityEngine;
using TMPro;

public class ControladorJuego : MonoBehaviour
{
    // Referencias de escena y UI
    public GameObject bloquePrefab;
    public TextMeshProUGUI textoHistorial;

    // Iconos visuales que representan cada color de la paleta
    public GameObject iconoRojo;
    public GameObject iconoAzul;
    public GameObject iconoVerde;
    public GameObject iconoAmarillo;

    // Estructuras de datos principales (lógica desacoplada de Unity)
    private Pila pilaAcciones = new Pila();
    private ListaCircularColores paletaColores = new ListaCircularColores();

    // Dimensiones del lienzo (32x32 celdas)
    private int anchoCuadricula = 32;
    private int altoCuadricula = 32;

    void Start()
    {
        GenerarLineasCuadricula();

        // Carga inicial de colores en la lista circular
        paletaColores.Agregar(Color.red);
        paletaColores.Agregar(Color.blue);
        paletaColores.Agregar(Color.green);
        paletaColores.Agregar(Color.yellow);

        ActualizarInterfazVisual();
        ActualizarPanelCodigo("SISTEMA LISTO.\nUsa FLECHAS ARRIBA/ABAJO para cambiar el color cíclicamente.");
    }

    void Update()
    {
        LeerEntradaColor();
        LeerEntradaDibujo();
        LeerEntradaDeshacer();
    }

    // Navegación por la lista circular de colores
    void LeerEntradaColor()
    {
        // Flecha arriba: retrocede al nodo anterior
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            paletaColores.Anterior();
            ActualizarInterfazVisual();
            ActualizarPanelCodigo("SE EJECUTÓ (LISTA CIRCULAR - ANTERIOR):\n\n" +
                "public void Anterior() {\n" +
                "    if (actual != null) {\n" +
                "        actual = actual.anterior;\n" +
                "    }\n" +
                "}\n\n" +
                "Apuntador movido al nodo previo.");
        }

        // Flecha abajo: avanza al nodo siguiente
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            paletaColores.Siguiente();
            ActualizarInterfazVisual();
            ActualizarPanelCodigo("SE EJECUTÓ (LISTA CIRCULAR - SIGUIENTE):\n\n" +
                "public void Siguiente() {\n" +
                "    if (actual != null) {\n" +
                "        actual = actual.siguiente;\n" +
                "    }\n" +
                "}\n\n" +
                "Apuntador movido al siguiente nodo.");
        }
    }

    // Instancia un bloque en la cuadrícula y registra la acción en la pila (Push)
    void LeerEntradaDibujo()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        // Convierte la posición del clic en pantalla a coordenadas de la cuadrícula
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = Mathf.RoundToInt(mousePos.x);
        int y = Mathf.RoundToInt(mousePos.y);

        // Verifica que el clic esté dentro del rango 0..31 en X y Y
        if (x >= 0 && x < anchoCuadricula && y >= 0 && y < altoCuadricula)
        {
            Color colorSeleccionado = paletaColores.ObtenerColorActual();
            GameObject nuevoObjeto = Instantiate(bloquePrefab, new Vector3(x, y, 0), Quaternion.identity);
            nuevoObjeto.GetComponent<SpriteRenderer>().color = colorSeleccionado;

            Accion nuevaAccion = new Accion(new Vector2Int(x, y), colorSeleccionado, nuevoObjeto);
            pilaAcciones.Push(nuevaAccion);

            ActualizarPanelCodigo("SE EJECUTÓ (PUSH):\n\n" +
                "public void Push(Accion nuevaAccion) {\n" +
                "    Nodo nuevoNodo = new Nodo(nuevaAccion);\n" +
                "    nuevoNodo.siguiente = cima;\n" +
                "    cima = nuevoNodo;\n" +
                "    contador++;\n" +
                "}\n\n" +
                "Posición: (" + x + ", " + y + ")\n" +
                "Elementos en Pila: " + pilaAcciones.ObtenerTamaño());
        }
        else
        {
            ActualizarPanelCodigo("ADVERTENCIA:\nClic fuera del tablero de 32x32.\nCoordenadas inválidas: (" + x + ", " + y + ")");
        }
    }

    // Deshace el último trazo extrayendo la cima de la pila (Pop)
    void LeerEntradaDeshacer()
    {
        if (!Input.GetKeyDown(KeyCode.Z)) return;

        if (!pilaAcciones.EstaVacia())
        {
            Accion accionQuitada = pilaAcciones.Pop();
            if (accionQuitada.objetoVisual != null)
            {
                Destroy(accionQuitada.objetoVisual);
            }

            ActualizarPanelCodigo("SE EJECUTÓ (POP / UNDO):\n\n" +
                "public Accion Pop() {\n" +
                "    Accion dato = cima.accion;\n" +
                "    cima = cima.siguiente;\n" +
                "    contador--;\n" +
                "    return dato;\n" +
                "}\n\n" +
                "Elementos restantes: " + pilaAcciones.ObtenerTamaño());
        }
        else
        {
            ActualizarPanelCodigo("ADVERTENCIA:\nPila vacía. No hay nada que deshacer.");
        }
    }

    // Resalta visualmente el color activo según lo que devuelva la lista circular
    void ActualizarInterfazVisual()
    {
        Color actual = paletaColores.ObtenerColorActual();

        EstablecerEstadoVisual(iconoRojo, actual == Color.red);
        EstablecerEstadoVisual(iconoAzul, actual == Color.blue);
        EstablecerEstadoVisual(iconoVerde, actual == Color.green);
        EstablecerEstadoVisual(iconoAmarillo, actual == Color.yellow);
    }

    // Ajusta escala y opacidad para diferenciar el color seleccionado del resto
    void EstablecerEstadoVisual(GameObject icono, bool seleccionado)
    {
        if (icono == null) return;

        SpriteRenderer sr = icono.GetComponent<SpriteRenderer>();
        if (seleccionado)
        {
            icono.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
            if (sr != null) { Color c = sr.color; c.a = 1.0f; sr.color = c; }
        }
        else
        {
            icono.transform.localScale = new Vector3(1.0f, 1.0f, 1f);
            if (sr != null) { Color c = sr.color; c.a = 0.4f; sr.color = c; }
        }
    }

    // Crea las líneas divisorias verticales y horizontales de la cuadrícula
    void GenerarLineasCuadricula()
    {
        for (int i = 0; i <= anchoCuadricula; i++)
        {
            float posX = i - 0.5f;
            GameObject lineaV = Instantiate(bloquePrefab, new Vector3(posX, 15.5f, 0), Quaternion.identity);
            lineaV.transform.localScale = new Vector3(0.05f, 32f, 1f);
            lineaV.GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, 0.5f);
        }

        for (int j = 0; j <= altoCuadricula; j++)
        {
            float posY = j - 0.5f;
            GameObject lineaH = Instantiate(bloquePrefab, new Vector3(15.5f, posY, 0), Quaternion.identity);
            lineaH.transform.localScale = new Vector3(32f, 0.05f, 1f);
            lineaH.GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, 0.5f);
        }
    }

    // Muestra en el panel lateral el código ejecutado y el estado actual
    void ActualizarPanelCodigo(string mensaje)
    {
        if (textoHistorial != null)
        {
            textoHistorial.text = "--- PANEL EDUCATIVO ---\n\n" + mensaje;
        }
    }
}