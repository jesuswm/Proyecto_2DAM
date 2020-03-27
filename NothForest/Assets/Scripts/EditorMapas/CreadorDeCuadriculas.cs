using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
/// <summary>
/// Clase que gestiona el conjunto de celdas que permiten la edición del mapa
/// </summary>
public class CreadorDeCuadriculas : MonoBehaviour
{
    
    /// <summary>
    /// Numero de celdas que componen una fila del mapa
    /// </summary>
    public int ancho;
    /// <summary>
    /// Numero de filas que componen el mapa
    /// </summary>
    public int alto;
    /// <summary>
    /// Tilemap que se usa de referencia para la colocación de celdas en la escena.
    /// </summary>
    Tilemap tilemap;
    /// <summary>
    /// Lista de celdas del editor
    /// </summary>
    List<GameObject> cuadriculas=new List<GameObject>();
    GameObject PanelHerramientas;
    GenerarMapa generadorMapa;
    /// <summary>
    /// Función que guarda el mapa actual del editor en el archivo mapa.dat
    /// </summary>
    public void guardarMapa()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string patch = Application.persistentDataPath + "/mapa.dat";
        FileStream stream = new FileStream(patch, FileMode.Create);
        List<ObjetoMapa> objetos=new List<ObjetoMapa>();
        foreach (GameObject gameObject in cuadriculas) {
            RectTransform rectTrans = gameObject.GetComponent<RectTransform>();
            if (gameObject.GetComponent<PulsarCuadricula>().Activo) {
                rectTrans = gameObject.GetComponent<RectTransform>();
                objetos.Add(new TileMapa(tilemap.WorldToCell(rectTrans.position).x,
                    tilemap.WorldToCell(rectTrans.position).y,0, gameObject.GetComponent<PulsarCuadricula>().Tile, gameObject.GetComponent<PulsarCuadricula>().Traspasable));
            }
            switch (gameObject.GetComponent<PulsarCuadricula>().Evento)
            {
                case eventosMapa.arbusto:
                    objetos.Add(new ArbustoMapa(tilemap.WorldToCell(rectTrans.position).x,
                        tilemap.WorldToCell(rectTrans.position).y, 0));
                    break;
                case eventosMapa.jugador:
                    objetos.Add(new JugadorMapa(tilemap.WorldToCell(rectTrans.position).x,
                        tilemap.WorldToCell(rectTrans.position).y, 0));
                    break;
                case eventosMapa.moco:
                    objetos.Add(new EnemigoMapa(tilemap.WorldToCell(rectTrans.position).x,
                        tilemap.WorldToCell(rectTrans.position).y, 0,eEnemigo.Moco));
                    break;
                case eventosMapa.tronquito:
                    objetos.Add(new EnemigoMapa(tilemap.WorldToCell(rectTrans.position).x,
                        tilemap.WorldToCell(rectTrans.position).y, 0, eEnemigo.Tronquito));
                    break;
                case eventosMapa.orco:
                    objetos.Add(new EnemigoMapa(tilemap.WorldToCell(rectTrans.position).x,
                        tilemap.WorldToCell(rectTrans.position).y, 0, eEnemigo.Orco));
                    break;
                case eventosMapa.arbol:
                    objetos.Add(new ObstaculosMapa(tilemap.WorldToCell(rectTrans.position).x,
                        tilemap.WorldToCell(rectTrans.position).y, 0, eObstaculos.Arbol));
                    break;
            }
        }
        formatter.Serialize(stream, objetos);
        stream.Close();
        Debug.Log("MapaGuardado");
    }
    /// <summary>
    /// Función que carga la scena donde se generan loa mapas apartir de un archivo.
    /// </summary>
    public void JugarMapa()
    {
        SceneManager.LoadScene(3);
        //guardarMapa();
        //PanelHerramientas.SetActive(false);
        //generadorMapa.generarMapa();
        //Destroy(this.transform.gameObject);
    }
    /// <summary>
    /// Función que recorre la lista de <see cref="cuadriculas"/>. 
    /// En caso de que el valor de la cuadricula en la variable <see cref="PulsarCuadricula.evento"/> 
    /// fuera <see cref="eventosMapa.jugador"/> se cambia a <see cref="eventosMapa.ninguno"/>
    /// </summary>
    public void borrarJugadorDelMapa()
    {
        foreach (GameObject gameObject in cuadriculas)
        {
            if(gameObject.GetComponent<PulsarCuadricula>().Evento == eventosMapa.jugador){
                gameObject.GetComponent<PulsarCuadricula>().actualizarEvento(eventosMapa.ninguno,false);
            }
        }
    }
    /// <summary>
    /// Función que cambia el valor de la variable <see cref="PulsarCuadricula.bloqueoEvento"/> a las casillas adyacentes a una posición
    /// </summary>
    /// <param name="rectTrans">Transform de la casilla de la que queramos bloquear sus adyacentes</param>
    /// <param name="bloquear">valor que queremos establecer en la 
    /// variabe <see cref="PulsarCuadricula.bloqueoEvento"/> de las casillas adyacentes
    /// </param>
    public void bloquearEventosAlrrededor(RectTransform rectTrans,bool bloquear)
    {
        List<Vector3Int> buscados = new List<Vector3Int>();
        Vector3Int pos = tilemap.WorldToCell(rectTrans.position);
        buscados.Add(new Vector3Int(pos.x + 1, pos.y, pos.z));
        buscados.Add(new Vector3Int(pos.x, pos.y + 1, pos.z));
        buscados.Add(new Vector3Int(pos.x + 1, pos.y + 1, pos.z));
        foreach (GameObject gameObject in cuadriculas)
        {
            //if (buscados.Contains(tilemap.WorldToCell(gameObject.GetComponent<RectTransform>().position)))
            //{
            //    gameObject.GetComponent<PulsarCuadricula>().actualizarEvento(eventosMapa.ninguno);
            //    gameObject.GetComponent<PulsarCuadricula>().bloqueoEvento = bloquear;
            //}
            if (buscados.Contains(tilemap.WorldToCell(gameObject.GetComponent<RectTransform>().position)))
            {
                Vector3Int posicion = tilemap.WorldToCell(gameObject.GetComponent<RectTransform>().position);
                if (bloquear && gameObject.GetComponent<PulsarCuadricula>().bloqueoEvento)
                {
                    List<Vector3Int> posiblesPosicionesPadre=new List<Vector3Int>();
                    posiblesPosicionesPadre.Add(new Vector3Int(posicion.x, posicion.y - 1, posicion.z));
                    posiblesPosicionesPadre.Add(new Vector3Int(posicion.x-1, posicion.y, posicion.z));
                    foreach (GameObject gameO in cuadriculas)
                    {
                        if (posiblesPosicionesPadre.Contains(tilemap.WorldToCell(gameO.GetComponent<RectTransform>().position)) 
                            && gameO.GetComponent<PulsarCuadricula>().Evento==eventosMapa.arbol)
                        {
                            gameO.GetComponent<PulsarCuadricula>().actualizarEvento(eventosMapa.ninguno,true);
                        }
                    }
                }
                gameObject.GetComponent<PulsarCuadricula>().actualizarEvento(eventosMapa.ninguno, true);
                gameObject.GetComponent<PulsarCuadricula>().bloqueoEvento = bloquear; 
            }
        }
    }
    /// <summary>
    /// Función que genera las cuadriculas del editor.
    /// </summary>
    void GenerarCuadriculas()
    {
        GameObject cuadricula = Resources.Load<GameObject>("cuadricula");
        int x = 0;
        int y = 0;
        tilemap = GameObject.Find("Traspasable").GetComponent<Tilemap>();
        for (int i = 0; i < alto; i++)
        {
            for (int j = 0; j < ancho; j++)
            {
                GameObject gameObject = Instantiate(cuadricula, transform, true);
                RectTransform rectTrans = gameObject.GetComponent<RectTransform>();
                rectTrans.localScale = new Vector3(1, 1, 1);
                rectTrans.position = tilemap.CellToWorld(new Vector3Int(x, y, 0));
                cuadriculas.Add(gameObject);
                x++;
            }
            y++;
            x = 0;
        }
    }
    /// <summary>
    /// Función que carga el mapa guardado en el archivo mapa.dat y lo carga en el editor
    /// </summary>
    public void CargarMapaAlEditor()
    {
        BorrarCuadriculas();
        GenerarCuadriculas();
        Dropdown selectorHerramienta= GameObject.Find("Herramienta").GetComponent<Dropdown>();
        selectorHerramienta.value = 0;
        Mapa mapa = new Mapa(CrearArchivo.cargarObjetosMapa());
        ObjetoMapa evento=null;
        List<TileMapa> suelos = new List<TileMapa>();
        suelos.AddRange(mapa.TerrenoTraspasable);
        suelos.AddRange(mapa.TerrenoNoTraspasable);
        foreach (TileMapa suelotras in suelos)
        {
            evento = null;
            foreach (JugadorMapa jugador in mapa.Jugador)
            {
                if(jugador.X==suelotras.X && jugador.Y == suelotras.Y && jugador.Z == suelotras.Z)
                {
                    //Debug.Log("Entro aqui siempre ????");
                    evento = jugador;
                    break;
                }
            }
            if (evento == null)
            {
                foreach (EnemigoMapa enemigo in mapa.Enemigo)
                {
                    if (enemigo.X == suelotras.X && enemigo.Y == suelotras.Y && enemigo.Z == suelotras.Z)
                    {
                        evento = enemigo;
                        break;
                    }
                }
            }
            if (evento == null)
            {
                foreach (ArbustoMapa arbusto in mapa.Arbusto)
                {
                    if (arbusto.X == suelotras.X && arbusto.Y == suelotras.Y && arbusto.Z == suelotras.Z)
                    {
                        evento = arbusto;
                        break;
                    }
                }
            }
            if (evento == null)
            {
                foreach (ObstaculosMapa obstaculo in mapa.Obstaculos)
                {
                    if (obstaculo.X == suelotras.X && obstaculo.Y == suelotras.Y && obstaculo.Z == suelotras.Z)
                    {
                        evento = obstaculo;
                        break;
                    }
                }
            }
            //Debug.Log("buscando pos x: " + suelotras.X + " pos y: " + suelotras.Y);
            foreach (GameObject celda in cuadriculas)
            {
                PulsarCuadricula pulsar = celda.GetComponent<PulsarCuadricula>();
                RectTransform rectTrans = celda.GetComponent<RectTransform>();
                //Debug.Log("pos x: " + tilemap.WorldToCell(rectTrans.position).x + " pos y: " + tilemap.WorldToCell(rectTrans.position).y);
                if (tilemap.WorldToCell(rectTrans.position).x == suelotras.X &&
                        tilemap.WorldToCell(rectTrans.position).y == suelotras.Y)
                {
                    //Debug.Log("encontrado");
                    
                    pulsar.pintarSuelo(suelotras.Tile, suelotras.Traspasable);
                    if (evento != null)
                    {
                        //Debug.Log("Clase: " + evento.GetType().Name);
                        if (evento is JugadorMapa)
                        {
                            //Debug.Log("Jugador");
                            if (pulsar.Evento != eventosMapa.jugador)
                            {
                                pulsar.actualizarEvento(eventosMapa.jugador,true);
                            }
                        }
                        else if (evento is EnemigoMapa)
                        {
                            switch (((EnemigoMapa)evento).TipoEnemigo)
                            {
                                case eEnemigo.Moco:
                                    //Debug.Log("Moco");
                                    if (pulsar.Evento != eventosMapa.moco)
                                    {
                                        pulsar.actualizarEvento(eventosMapa.moco, true);
                                    }
                                    break;
                                case eEnemigo.Orco:
                                    //Debug.Log("Orco");
                                    if (pulsar.Evento != eventosMapa.orco)
                                    {
                                        pulsar.actualizarEvento(eventosMapa.orco, true);
                                    }
                                    break;
                                case eEnemigo.Tronquito:
                                    //Debug.Log("Tronquito");
                                    if (pulsar.Evento!=eventosMapa.tronquito)
                                    {
                                        pulsar.actualizarEvento(eventosMapa.tronquito, true);
                                    }
                                    break;
                            }
                        }
                        else if(evento is ArbustoMapa)
                        {
                            //Debug.Log("arbusto");
                            if (pulsar.Evento != eventosMapa.arbusto)
                            {
                                pulsar.actualizarEvento(eventosMapa.arbusto, true);
                            }
                        }
                        else if(evento is ObstaculosMapa)
                        {
                            switch (((ObstaculosMapa)evento).TipoObstaculo)
                            {
                                case eObstaculos.Arbol :
                                    //Debug.Log("arbol");
                                    if (pulsar.Evento != eventosMapa.arbol)
                                    {
                                        pulsar.actualizarEvento(eventosMapa.arbol, true);
                                    }
                                break;
                            }
                        }
                    }
                    break;
                }
            }
        }
    }
    /// <summary>
    /// Función que borra todas las cuadriculas del editor y vacía la lista de  <see cref="cuadriculas"/>
    /// </summary>
    void BorrarCuadriculas()
    {
        cuadriculas.Clear();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
    void Start()
    {
        generadorMapa = GameObject.Find("Grid").GetComponent<GenerarMapa>();
        PanelHerramientas = GameObject.Find("Panel");
        GenerarCuadriculas();
        //rectTrans.localPosition= new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
