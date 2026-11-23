# Editor Interactivo de Pixel Art — Pila y Lista Circular Doblemente Enlazada

Proyecto final de la asignatura **Estructuras de Datos (Grupo E193)** en las **Unidades Tecnológicas de Santander (UTS)**. Consiste en un editor interactivo de pixel art sobre una cuadrícula de `32x32` desarrollado en **C#** y **Unity**, enfocado en la implementación manual de estructuras de datos dinámicas y desacopladas de la interfaz gráfica.

## Estructuras de Datos Implementadas

1. **Pila dinámica (`LIFO` — *Last In, First Out*) en `Pila.cs`:**
   - Gestiona el historial de acciones de dibujo para la función **Deshacer (`Undo`)** mediante nodos enlazados (`Nodo` y `Accion`).
   - Operaciones principales: `Push`, `Pop`, `EstaVacia` y `ObtenerTamano`, ejecutándose en tiempo constante $O(1)$.

2. **Lista Circular Doblemente Enlazada en `ListaCircularColores.cs`:**
   - Gestiona la rotación continua y bidireccional de la paleta de colores (`Rojo`, `Azul`, `Verde`, `Amarillo`) mediante nodos con enlaces `siguiente` y `anterior` (`NodoColor`).
   - Operaciones principales: `Agregar`, `ObtenerColorActual`, `Siguiente` y `Anterior`, ejecutándose en tiempo constante $O(1)$.

## Arquitectura y Scripts Principales

El código fuente se encuentra en `Assets/Scripts/`:

- `Accion.cs`: Almacena los datos de cada trazo (posición `(x, y)`, color y referencia al objeto visual en escena).
- `Pila.cs`: Implementación manual de la pila dinámica y sus nodos.
- `ListaCircularColores.cs`: Implementación manual de la lista circular doblemente enlazada sin dependencias de interfaz.
- `ControladorJuego.cs`: Coordina las entradas de teclado y ratón en Unity, la cuadrícula de `32x32`, los indicadores visuales de color y el panel educativo en tiempo real.

## Controles de Uso

- **Clic izquierdo** dentro de la cuadrícula (`32x32`): Pinta una celda con el color activo y apila la acción (`Push`).
- **Tecla `Z`**: Deshace el último trazo realizado (`Pop`).
- **Flecha Arriba / Flecha Abajo**: Navega hacia atrás o hacia adelante de forma cíclica por la paleta de colores.

## Cómo Abrir el Proyecto en Unity

1. Clona o descarga este repositorio en tu equipo.
2. Abre **Unity Hub** y haz clic en **Add** (o *Agregar proyecto desde disco*).
3. Selecciona la carpeta raíz del repositorio (`proyecto-estructuras-pila`).
4. Abre la escena principal dentro de `Assets/Scenes` y presiona **Play**.