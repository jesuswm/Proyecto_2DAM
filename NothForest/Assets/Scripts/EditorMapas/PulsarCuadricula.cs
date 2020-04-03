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
    jugador,arbusto,moco,orco,tronquito,arbol,tocon,rocaGrande, ninguno
}
/// <summary>
/// Clase que se encarga de gestionar la pulsación de una cuadricula
/// </summary>
public class PulsarCuadricula : MonoBehaviour, IPointerDownHandler,
    IPointerUpHandler,IPointerExitHandler, IPointerEnterHandler
{
    static bool pulsado=false;
    /// <summary>
    /// Lista de tiles que son traspasables por el jugador
    /// </summary>
    List<eTiles> traspasables = new List<eTiles>();
    /// <summary>
    /// SeleccionDeHerramienta de la escena que almacena la herramienta actual
    /// </summary>
    SeleccionDeHerramienta selectorHerramienta;
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
    /// <summary>
    /// Color de la casilla en estado normal
    /// </summary>
    Color colorNormal;


    void Start()
    {
        cuadriculas= GameObject.Find("Celdas").GetComponent<CreadorDeCuadriculas>();
        image = GetComponent<Image>();
        colorNormal = image.color;
        pincel = GameObject.Find("Pincel").GetComponent<Dropdown>();
        selectorHerramienta = GameObject.Find("TogleHerramientas").GetComponent<SeleccionDeHerramienta>();
        imagenEvento =transform.GetChild(0).GetComponent<SpriteRenderer>();
        imagenEvento.sprite = null;
        audioSource = GetComponent<AudioSource>();
        traspasables.Add(eTiles.Cesped);
        //audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        //posicionInicialImagen=imagenEvento.transform.localPosition;
        //Debug.Log("X:"+ posicionInicialImagen.x + "y:"+ posicionInicialImagen.y + "Z:"+ posicionInicialImagen.z);
    }
    /// <summary>
    /// Función con todos los comportamientos al pulsar una casilla en función de la herramienta y el pincel
    /// </summary>
    public void pulsar()
    {
        switch (selectorHerramienta.herramientaActual)
        {
            case eHerramientas.suelo:
                if (traspasables.Contains((eTiles)(pincel.value)))
                {
                    pintarSuelo((eTiles)(pincel.value), true);
                }
                else
                {
                    actualizarEvento(eventosMapa.ninguno, true);
                    pintarSuelo((eTiles)(pincel.value), false);
                }
                break;
            case eHerramientas.enemigo:
                switch (pincel.value)
                {
                    case 0:
                        actualizarEvento(eventosMapa.moco, false);
                        break;
                    case 1:
                        actualizarEvento(eventosMapa.tronquito, false);
                        break;
                    case 2:
                        actualizarEvento(eventosMapa.orco, false);
                        break;
                }
                break;
            case eHerramientas.jugador:
                if (Evento != eventosMapa.jugador)
                {
                    cuadriculas.borrarJugadorDelMapa();
                }
                actualizarEvento(eventosMapa.jugador, false);
                break;
            case eHerramientas.arbusto:
                actualizarEvento(eventosMapa.arbusto, false);
                break;
            case eHerramientas.obstaculo:
                switch (pincel.value)
                {
                    case 0:
                        actualizarEvento(eventosMapa.arbol, false);
                        break;
                    case 1:
                        actualizarEvento(eventosMapa.tocon, false);
                        break;
                    case 2:
                        actualizarEvento(eventosMapa.rocaGrande, false);
                        break;
                }
                break;
            case eHerramientas.borrar:
                image.sprite = Resources.Load<Sprite>("CuadriculaImg");
                actualizarEvento(eventosMapa.ninguno,true);
                activo = false;
                break;
            case eHerramientas.mover:
                
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
        List<eHerramientas> herramientasMantenerPulsado=new List<eHerramientas>();
        herramientasMantenerPulsado.Add(eHerramientas.suelo);
        herramientasMantenerPulsado.Add(eHerramientas.arbusto);
        herramientasMantenerPulsado.Add(eHerramientas.borrar);
        if (herramientasMantenerPulsado.Contains(selectorHerramienta.herramientaActual))
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
        if (!bloqueoEvento && Activo && traspasable)
        {
            if (e == Evento)
            {
                if(e== eventosMapa.arbol || e== eventosMapa.tocon || e == eventosMapa.rocaGrande)
                {
                    cuadriculas.bloquearEventosAlrrededor(transform.gameObject.GetComponent<RectTransform>(),false);
                    //cuadriculas.bloquearEventosAlrrededor(rectTransform, false);
                }
                Evento = eventosMapa.ninguno;
            }
            else
            {
                if (e == eventosMapa.arbol || e == eventosMapa.tocon || e == eventosMapa.rocaGrande)
                {
                    cuadriculas.bloquearEventosAlrrededor(transform.gameObject.GetComponent<RectTransform>(), true);
                    //cuadriculas.bloquearEventosAlrrededor(rectTransform, true);
                }
                else if(Evento == eventosMapa.arbol || Evento == eventosMapa.tocon || Evento == eventosMapa.rocaGrande)
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
            case eventosMapa.tocon:
                imagenEvento.transform.localPosition = new Vector3(posicionInicialImagen.x * 2, posicionInicialImagen.y, posicionInicialImagen.z);
                imagenEvento.sprite = Resources.Load<Sprite>("ToconImg");
                break;
            case eventosMapa.rocaGrande:
                imagenEvento.transform.localPosition = new Vector3(posicionInicialImagen.x * 2, posicionInicialImagen.y, posicionInicialImagen.z);
                imagenEvento.sprite = Resources.Load<Sprite>("RocaGrandeImg");
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
        if ((bloqueoEvento || !Activo || !traspasable) && 
            (selectorHerramienta.herramientaActual!=eHerramientas.suelo &&
            selectorHerramienta.herramientaActual != eHerramientas.borrar &&
            selectorHerramienta.herramientaActual != eHerramientas.mover))
        {
            if (image.color == colorNormal)
            {
                Color noValido = colorNormal;
                noValido.a = 0.5f;
                image.color = noValido;
            }
        }
        else
        {
            if (image.color != colorNormal)
            {
                image.color = colorNormal;
            }
        }
    } 
}
