using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// Clase que gestiona los controles de movimiento del personaje
/// </summary>
/// <remarks>
/// Para el funcionamiento de esta clase es necesario que el elemento también  tenga asociado
/// <list type="bullet">
/// <item><see cref="Image"/></item>
/// <item><see cref="Button"/></item>
/// </list>
/// </remarks>
public class BotonPulsado : MonoBehaviour, IPointerDownHandler,
    IPointerUpHandler,IPointerExitHandler, IPointerEnterHandler
{
    /// <summary>
    /// Booleana que indica si el botón se encuentra pulsado
    /// </summary>
    bool pressed = false;
    /// <summary>
    /// Objeto que contenga la clase <see cref="Jugador"/> en la escena
    /// </summary>
    public GameObject objtJugador;
    /// <summary>
    /// Imagen del botón
    /// </summary>
    Image image;
    /// <summary>
    /// Color del botón cuando no esté pulsado
    /// </summary>
    Color defaultColor =Color.white;
    /// <summary>
    /// Color del botón cuando esté pulsado
    /// </summary>
    Color SelecttColor =new Color();
    /// <summary>
    /// En función de su valor se selecciona la función que ejecutara el boton al ser pulsado
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item>Si su valor es <see cref="Direccion.abajo"/> ejecuta la función <see cref="Jugador.moverAbajo"/> del jugador <see cref="Jugador.moverAbajo"/></item>
    /// <item>Si su valor es<see cref="Direccion.arriba"/> ejecuta la función <see cref="Jugador.moverArriba"/> del jugador <see cref="Jugador.moverArriba"/></item>
    /// <item>Si su valor es<see cref="Direccion.derecha"/> ejecuta la función <see cref="Jugador.moverDerecha"/> del jugador <see cref="Jugador.moverDerecha"/></item>
    /// <item>Si su valor es<see cref="Direccion.izquierda"/> ejecuta la función <see cref="Jugador.moverIzquierda"/> del jugador <see cref="Jugador.moverIzquierda"/></item>
    /// </list>
    /// </remarks>
    public Direccion direccion;
    /// <summary>
    /// Booleana que indica si es la primera vez que esta enable el botón
    /// </summary>
    bool yaStart;
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase esta habilitado por primera vez antes de update.
    /// En ella se inicializan las variables  
    /// </summary>
    void Start()
    {
        if (objtJugador == null)
        {
            objtJugador = GameObject.Find("Jugador");
        }
        image = GetComponent<Image>();
        defaultColor = image.color;
        SelecttColor = GetComponent<Button>().colors.pressedColor;
        pressed = false;
        yaStart = true;
    }
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase cambia al estado habilitado.
    /// </summary>
    void OnEnable()
    {
        if (yaStart)
        {
            pressed = false;
            image.color = defaultColor;
        }
    }
    /// <summary>
    /// Función que se llama en cuanto el elemento es pulsado.
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
        image.color = SelecttColor;
    }
    /// <summary>
    /// Función que se llama en cuanto el elemento es despulsado.
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
        image.color = defaultColor;
    }
    /// <summary>
    /// Función que se llama en cuanto se deje de pulsar.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        pressed = false;
        image.color = defaultColor;
    }
    /// <summary>
    /// Función que se llama en cuanto se empieza a pulsar.
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        pressed = true;
        image.color = SelecttColor;
    }
    /// <summary>
    /// Función que se llama cada frame mientras que el elemento que posee esta clase esté habilitado
    /// en ella se comprueba si se esta pulsado y ejecuta una función según el valor de <see cref="direccion"/>. 
    /// </summary>
    void Update()
    {
        if (pressed)
        {
            switch (direccion) {
                case Direccion.derecha:
                objtJugador.GetComponent<Jugador>().moverDerecha();
                    break;
                case Direccion.izquierda:
                    objtJugador.GetComponent<Jugador>().moverIzquierda();
                    break;
                case Direccion.abajo:
                    objtJugador.GetComponent<Jugador>().moverAbajo();
                    break;
                case Direccion.arriba:
                    objtJugador.GetComponent<Jugador>().moverArriba();
                    break;
                default:
                    Debug.Log("Funicon no asignada");
                    break;
            }
        }   
    } 
}
