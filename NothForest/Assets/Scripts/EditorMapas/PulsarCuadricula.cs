using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
/// <summary>
/// Enumerado que contiene los diferentes elementos que pueden situarse sobre las casillas
/// </summary>
public enum eventosMapa
{
    jugador,arbusto,moco,orco,tronquito,arbol,ninguno
}
/// <summary>
/// Clase que se encarga de gestionar la pulsación de una cuadricula
/// </summary>
public class PulsarCuadricula : MonoBehaviour, IPointerDownHandler,
    IPointerUpHandler,IPointerExitHandler, IPointerEnterHandler
{
    static bool pulsado=false;
    /// <summary>
    /// Dropdown de la escena con la lista de herramientas
    /// </summary>
    Dropdown selectorHerramienta;
    /// <summary>
    /// Dropdown de la escena con la lista de pinceles
    /// </summary>
    Dropdown pincel;
    /// <summary>
    /// Imagen de fondo de la cuadricula en la que se visualiza el tile
    /// </summary>
    Image image;
    /// <summary>
    /// Imagen de la cuadricula en la que se visualiza el evento colocado sobre ella
    /// </summary>
    SpriteRenderer imagenEvento;
    /// <summary>
    /// Bolean que indica si la celda ha de tenerse encuentra a la hora de guardar el mapa
    /// </summary>
    bool activo = false;
    /// <summary>
    /// Bolean que indica si la celda es traspasable para el jugador
    /// </summary>
    bool traspasable =true;
    /// <summary>
    /// Toggle de la escena con la que se establece si estraspasable o no la celda
    /// </summary>
    Toggle togleTraspasable;
    /// <summary>
    /// Tile asociado a la casilla
    /// </summary>
    eTiles tile;
    /// <summary>
    /// Posicion de la <see cref="imagenEvento"/>
    /// </summary>
    Vector3 posicionInicialImagen;
    /// <summary>
    /// Evento asociado a la casilla 
    /// </summary>
    eventosMapa evento=eventosMapa.ninguno;
    /// <summary>
    /// Booleana que indica si se pueden poner eventos sobre esta casilla
    /// </summary>
    public bool bloqueoEvento=false;
    /// <summary>
    /// Creador de cuadriculas que de la escena
    /// </summary>
    CreadorDeCuadriculas cuadriculas;
    /// <summary>
    /// Pemite obtener el valor de la variable <see cref="activo"/>
    /// </summary>
    public bool Activo { get => activo;  }
    /// <summary>
    /// Pemite obtener o establecer el valor de la variable <see cref="evento"/>
    /// </summary>
    internal eventosMapa Evento { get => evento; set => evento = value; }
    /// <summary>
    /// Pemite obtener o establecer el valor de la variable <see cref="traspasable"/>
    /// </summary>
    public bool Traspasable { get => traspasable; set => traspasable = value; }
    /// <summary>
    /// Pemite obtener o establecer el valor de la variable <see cref="tile"/>
    /// </summary>
    public eTiles Tile { get => tile; set => tile = value; }

    void Start()
    {
        cuadriculas= GameObject.Find("Celdas").GetComponent<CreadorDeCuadriculas>();
        image = GetComponent<Image>();
        pincel = GameObject.Find("Pincel").GetComponent<Dropdown>();
        togleTraspasable= GameObject.Find("TogleTraspasable").GetComponent<Toggle>();
        selectorHerramienta = GameObject.Find("Herramienta").GetComponent<Dropdown>();
        imagenEvento=transform.GetChild(0).GetComponent<SpriteRenderer>();
        imagenEvento.sprite = null;
        posicionInicialImagen=imagenEvento.transform.localPosition;
    }
    /// <summary>
    /// Función con todos los comportamientos al pulsar una casilla en función de la herramienta y el pincel
    /// </summary>
    public void pulsar()
    {
        switch (selectorHerramienta.value)
        {
            case 0:
               
                    image.sprite = Tiles.obtenerTile((eTiles)pincel.value).sprite;
                    Tile = (eTiles)(pincel.value);
                    Traspasable = togleTraspasable.isOn;
                    activo = true;
                break;
            case 1:
                switch (pincel.value)
                {
                    case 0:
                        actualizarEvento(eventosMapa.moco);
                        break;
                    case 1:
                        actualizarEvento(eventosMapa.tronquito);
                        break;
                    case 2:
                        actualizarEvento(eventosMapa.orco);
                        break;
                }
                break;
            case 2:
                if (Evento != eventosMapa.jugador)
                {
                    cuadriculas.borrarJugadorDelMapa();
                }
                actualizarEvento(eventosMapa.jugador);
                break;
            case 3:
                actualizarEvento(eventosMapa.arbusto);
                break;
            case 4:
                //if (!bloqueoEvento)
                //{
                //cuadriculas.bloquearEventosAlrrededor(transform.gameObject.GetComponent<RectTransform>(),true);
                actualizarEvento(eventosMapa.arbol);
                //}
                break;
            case 5:
                image.sprite = Resources.Load<Sprite>("CuadriculaImg");
                actualizarEvento(eventosMapa.ninguno);
                activo = false;
                break;
            case 6:
                
                break;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        pulsar();
        List<int> herramientasMantenerPulsado=new List<int>();
        herramientasMantenerPulsado.Add(0);
        herramientasMantenerPulsado.Add(5);
        herramientasMantenerPulsado.Add(3);
        if (herramientasMantenerPulsado.Contains(selectorHerramienta.value))
        {
            pulsado = true;
        }
    }
    /// <summary>
    /// Función que establece el valor de <see cref="evento"/> en función de un parámetro si <see cref="bloqueoEvento"/> es false
    /// si el evento que se pasa como parámetro ya es el mismo que tiene en ese momento se establecera <see cref="eventosMapa.ninguno"/>
    /// </summary>
    /// <param name="e">Evento que queremos situar en la casilla</param>
    public void actualizarEvento(eventosMapa e)
    {
        if (!bloqueoEvento)
        {
            if (e == Evento)
            {
                if(e== eventosMapa.arbol)
                {
                    cuadriculas.bloquearEventosAlrrededor(transform.gameObject.GetComponent<RectTransform>(),false);
                }
                Evento = eventosMapa.ninguno;
            }
            else
            {
                if (e == eventosMapa.arbol)
                {
                    cuadriculas.bloquearEventosAlrrededor(transform.gameObject.GetComponent<RectTransform>(), true);
                }
                else if(Evento == eventosMapa.arbol)
                {
                    cuadriculas.bloquearEventosAlrrededor(transform.gameObject.GetComponent<RectTransform>(), false);
                }
                Evento = e;
            }
            actualizarImagenEvento();
        }
    }
    /// <summary>
    /// Función que actualiza la imagen de la casilla en el editor
    /// </summary>
    private void actualizarImagenEvento()
    {
        switch (Evento)
        {
            case eventosMapa.ninguno:
                imagenEvento.sprite = null;
                break;
            case eventosMapa.jugador:
                imagenEvento.transform.localPosition = posicionInicialImagen;
                imagenEvento.sprite = Resources.Load<Sprite>("PersonajeImg");
                break;
            case eventosMapa.moco:
                imagenEvento.transform.localPosition = posicionInicialImagen;
                imagenEvento.sprite = Resources.Load<Sprite>("MocoImg");
                break;
            case eventosMapa.tronquito:
                imagenEvento.transform.localPosition = posicionInicialImagen;
                imagenEvento.sprite = Resources.Load<Sprite>("TronquitoImg");
                break;
            case eventosMapa.orco:
                imagenEvento.transform.localPosition = posicionInicialImagen;
                imagenEvento.sprite = Resources.Load<Sprite>("OrcoImg");
                break;
            case eventosMapa.arbusto:
                imagenEvento.transform.localPosition= new Vector3(posicionInicialImagen.x, posicionInicialImagen.y / 2, posicionInicialImagen.z);
                imagenEvento.sprite = Resources.Load<Sprite>("ArbustoImg");
                break;
            case eventosMapa.arbol:
                imagenEvento.transform.localPosition = new Vector3(posicionInicialImagen.x*2, posicionInicialImagen.y, posicionInicialImagen.z);
                imagenEvento.sprite = Resources.Load<Sprite>("ArbolImg");
                break;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        pulsado = false;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (pulsado)
        {
            pulsar();
        }
    }
    
    void Update()
    {
        
    } 
}
