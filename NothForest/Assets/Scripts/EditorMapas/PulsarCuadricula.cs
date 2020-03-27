using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading;
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
    Vector3 posicionInicialImagen=new Vector3(43.2f,75.2f,13.25063f);
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
    /// <summary>
    /// AudioSource asociado a la casilla
    /// </summary>
    AudioSource audioSource;
     
    void Start()
    {
        cuadriculas= GameObject.Find("Celdas").GetComponent<CreadorDeCuadriculas>();
        image = GetComponent<Image>();
        pincel = GameObject.Find("Pincel").GetComponent<Dropdown>();
        togleTraspasable= GameObject.Find("TogleTraspasable").GetComponent<Toggle>();
        selectorHerramienta = GameObject.Find("Herramienta").GetComponent<Dropdown>();
        imagenEvento=transform.GetChild(0).GetComponent<SpriteRenderer>();
        imagenEvento.sprite = null;
        audioSource = GetComponent<AudioSource>();
        //audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        //posicionInicialImagen=imagenEvento.transform.localPosition;
        //Debug.Log("X:"+ posicionInicialImagen.x + "y:"+ posicionInicialImagen.y + "Z:"+ posicionInicialImagen.z);
    }
    /// <summary>
    /// Función con todos los comportamientos al pulsar una casilla en función de la herramienta y el pincel
    /// </summary>
    public void pulsar()
    {
        switch (selectorHerramienta.value)
        {
            case 0:
                pintarSuelo((eTiles)(pincel.value), togleTraspasable.isOn);
                break;
            case 1:
                switch (pincel.value)
                {
                    case 0:
                        actualizarEvento(eventosMapa.moco,false);
                        break;
                    case 1:
                        actualizarEvento(eventosMapa.tronquito,false);
                        break;
                    case 2:
                        actualizarEvento(eventosMapa.orco,false);
                        break;
                }
                break;
            case 2:
                if (Evento != eventosMapa.jugador)
                {
                    cuadriculas.borrarJugadorDelMapa();
                }
                actualizarEvento(eventosMapa.jugador,false);
                break;
            case 3:
                actualizarEvento(eventosMapa.arbusto,false);
                break;
            case 4:
                //if (!bloqueoEvento)
                //{
                //cuadriculas.bloquearEventosAlrrededor(transform.gameObject.GetComponent<RectTransform>(),true);
                actualizarEvento(eventosMapa.arbol,false);
                //}
                break;
            case 5:
                image.sprite = Resources.Load<Sprite>("CuadriculaImg");
                actualizarEvento(eventosMapa.ninguno,true);
                activo = false;
                break;
            case 6:
                
                break;
        }
    }
    /// <summary>
    /// Función que establece el suelo de una casilla y si es traspasable
    /// </summary>
    /// <param name="tile">Tile del suelo que queremos establecer en la casilla</param>
    /// <param name="traspasable">Booleana que indica si la casilla es traspasable o no</param>
    public void pintarSuelo(eTiles tile,bool traspasable)
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }
        image.sprite = Tiles.obtenerTile(tile).sprite;
        Tile = tile;
        Traspasable = traspasable;
        activo = true;
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
    /// <param name="silenciado">Indica si emite sonido de error al intentar colocar un evento en una casilla no valida</param>
    public void actualizarEvento(eventosMapa e, bool silenciado)
    {
        if (cuadriculas == null)
        {
            cuadriculas = GameObject.Find("Celdas").GetComponent<CreadorDeCuadriculas>();
        }
        if (!bloqueoEvento && Activo)
        {
            if (e == Evento)
            {
                if(e== eventosMapa.arbol)
                {
                    cuadriculas.bloquearEventosAlrrededor(transform.gameObject.GetComponent<RectTransform>(),false);
                    //cuadriculas.bloquearEventosAlrrededor(rectTransform, false);
                }
                Evento = eventosMapa.ninguno;
            }
            else
            {
                if (e == eventosMapa.arbol)
                {
                    cuadriculas.bloquearEventosAlrrededor(transform.gameObject.GetComponent<RectTransform>(), true);
                    //cuadriculas.bloquearEventosAlrrededor(rectTransform, true);
                }
                else if(Evento == eventosMapa.arbol)
                {
                    cuadriculas.bloquearEventosAlrrededor(transform.gameObject.GetComponent<RectTransform>(), false);
                    //cuadriculas.bloquearEventosAlrrededor(rectTransform, false);
                }
                Evento = e;
            }
            //Debug.Log(Evento.ToString());
            actualizarImagenEvento();
        }
        else
        {
            if (!silenciado)
            {
                GetComponent<AudioSource>().Play();
            }
        }
        //GetComponent<AudioSource>().mute=false;
    }

    /// <summary>
    /// Función que actualiza la imagen de la casilla en el editor
    /// </summary>
    private void actualizarImagenEvento()
    {
        if(imagenEvento == null){
            imagenEvento = transform.GetChild(0).GetComponent<SpriteRenderer>();
            posicionInicialImagen = new Vector3(43.2f, 75.2f, 13.25063f);
        }
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
        if(imagenEvento.sprite==null && Evento != eventosMapa.ninguno)
        {
            actualizarImagenEvento();
        }
    } 
}
