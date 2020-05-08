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
    /// Variable estática que especifica el nombre del mapa cargado actualmente en el editor
    /// </summary>
    public static string mapaActualEditor=null;
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
    /// <summary>
    /// GameObject que contiene la pantalla de guardado del editor.
    /// </summary>
    GameObject pantallaGuardar;
    /// <summary>
    /// GameObject que contiene la pantalla de cargar mapa del editor.
    /// </summary>
    GameObject pantallaCargar;
    /// <summary>
    /// GameObject que contiene la pantalla de borrado del editor.
    /// </summary>
    GameObject pantallaBorrado;
    /// <summary>
    /// GameObject que contiene la barra de herramientas del editor.
    /// </summary>
    GameObject PanelHerramientas;
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
                case eventosMapa.tocon:
                    objetos.Add(new ObstaculosMapa(tilemap.WorldToCell(rectTrans.position).x,
                        tilemap.WorldToCell(rectTrans.position).y, 0, eObstaculos.Tocon));
                    break;
                case eventosMapa.rocaGrande:
                    objetos.Add(new ObstaculosMapa(tilemap.WorldToCell(rectTrans.position).x,
                        tilemap.WorldToCell(rectTrans.position).y, 0, eObstaculos.RocaGrande));
                    break;
            }
        }
        formatter.Serialize(stream, objetos);
        stream.Close();
        Debug.Log("MapaGuardado");
    }
    /// <summary>
    /// Función que guarda el mapa actual del editor en un archivo con el nombre pasado como parametro
    /// <param name = "nombre" > Nombre con el que se guardara el mapa</param>
    /// </summary>
    public void guardarMapa(string nombre)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string patch = Application.persistentDataPath + "/"+nombre+".map";
        FileStream stream = new FileStream(patch, FileMode.Create);
        List<ObjetoMapa> objetos = new List<ObjetoMapa>();
        foreach (GameObject gameObject in cuadriculas)
        {
            RectTransform rectTrans = gameObject.GetComponent<RectTransform>();
            if (gameObject.GetComponent<PulsarCuadricula>().Activo)
            {
                rectTrans = gameObject.GetComponent<RectTransform>();
                objetos.Add(new TileMapa(tilemap.WorldToCell(rectTrans.position).x,
                    tilemap.WorldToCell(rectTrans.position).y, 0, gameObject.GetComponent<PulsarCuadricula>().Tile, gameObject.GetComponent<PulsarCuadricula>().Traspasable));
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
                        tilemap.WorldToCell(rectTrans.position).y, 0, eEnemigo.Moco));
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
                case eventosMapa.tocon:
                    objetos.Add(new ObstaculosMapa(tilemap.WorldToCell(rectTrans.position).x,
                        tilemap.WorldToCell(rectTrans.position).y, 0, eObstaculos.Tocon));
                    break;
                case eventosMapa.rocaGrande:
                    objetos.Add(new ObstaculosMapa(tilemap.WorldToCell(rectTrans.position).x,
                        tilemap.WorldToCell(rectTrans.position).y, 0, eObstaculos.RocaGrande));
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
        GenerarMapa.mapaActualPartida = CreadorDeCuadriculas.mapaActualEditor;
        CreadorDeCuadriculas.mapaActualEditor = null;
        SceneManager.LoadScene(3);
        //guardarMapa();
        //PanelHerramientas.SetActive(false);
        //generadorMapa.generarMapa();
        //Destroy(this.transform.gameObject);
    }
    /// <summary>
    /// Función que carga la scena de menu de incio y establece a null el valor de <see cref="mapaActualEditor"/> y el <see cref="GenerarMapa.mapaActualPartida"/>.
    /// </summary>
    public void VolverMenuPrincipal()
    {
        GenerarMapa.mapaActualPartida = null;
        CreadorDeCuadriculas.mapaActualEditor = null;
        SceneManager.LoadScene(0);
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
                            && (gameO.GetComponent<PulsarCuadricula>().Evento==eventosMapa.arbol || gameO.GetComponent<PulsarCuadricula>().Evento == eventosMapa.tocon || gameO.GetComponent<PulsarCuadricula>().Evento == eventosMapa.rocaGrande))
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
    /// Función que busca el <see cref="mapaActualEditor"/> y lo carga en el editor
    /// </summary>
    public void CargarMapaAlEditor()
    {
        BorrarCuadriculas();
        GenerarCuadriculas();
        Toggle tsuelo= GameObject.Find("Hsuelo").GetComponent<Toggle>();
        tsuelo.isOn = true;
        //Mapa mapa = new Mapa(CrearArchivo.cargarObjetosMapa());
        Mapa mapa = new Mapa(CrearArchivo.cargarObjetosMapa(mapaActualEditor));
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
                    pulsar.pintarSuelo(suelotras.Tile, suelotras.Traspasable);
                    if (evento != null)
                    {
                        if (evento is JugadorMapa)
                        {
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
                                    if (pulsar.Evento != eventosMapa.moco)
                                    {
                                        pulsar.actualizarEvento(eventosMapa.moco, true);
                                    }
                                    break;
                                case eEnemigo.Orco:
                                    if (pulsar.Evento != eventosMapa.orco)
                                    {
                                        pulsar.actualizarEvento(eventosMapa.orco, true);
                                    }
                                    break;
                                case eEnemigo.Tronquito:
                                    if (pulsar.Evento!=eventosMapa.tronquito)
                                    {
                                        pulsar.actualizarEvento(eventosMapa.tronquito, true);
                                    }
                                    break;
                            }
                        }
                        else if(evento is ArbustoMapa)
                        {
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
                                    if (pulsar.Evento != eventosMapa.arbol)
                                    {
                                        pulsar.actualizarEvento(eventosMapa.arbol, true);
                                    }
                                break;
                                case eObstaculos.Tocon:
                                    if (pulsar.Evento != eventosMapa.tocon)
                                    {
                                        pulsar.actualizarEvento(eventosMapa.tocon, true);
                                    }
                                break;
                                case eObstaculos.RocaGrande:
                                    if (pulsar.Evento != eventosMapa.rocaGrande)
                                    {
                                        pulsar.actualizarEvento(eventosMapa.rocaGrande, true);
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
    /// <summary>
    /// Función que abre o cierra el menú de guardar mapa
    /// </summary>
    public void AbrirCerrarMenuGuardar()
    {
        pantallaGuardar.SetActive(!pantallaGuardar.activeSelf);
    }
    /// <summary>
    /// Función que abre o cierra el menú de cargar mapa
    /// </summary>
    public void AbrirCerrarMenuCargar()
    {
        pantallaCargar.SetActive(!pantallaCargar.activeSelf);
    }
    /// <summary>
    /// Función que abre o cierra el menú de borrar mapa
    /// </summary>
    public void AbrirCerrarMenuBorrado()
    {
        pantallaBorrado.SetActive(!pantallaBorrado.activeSelf);
    }
    /// <summary>
    /// Función que comprueba si existen mapas guardados en caso de que no exista desabilita los botones de cargar y borrar el editor 
    /// </summary>
    public void ComprobarSiExistenMapasGuardados()
    {
        List<string> nombres = new List<string>();
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] info = dir.GetFiles("*.map");
        if (info.Length > 0)
        {
            GameObject.Find("Cargar").GetComponent<Button>().interactable = true;
            GameObject.Find("BorrarMapas").GetComponent<Button>().interactable = true;
        }
        else
        {
            GameObject.Find("Cargar").GetComponent<Button>().interactable = false;
            GameObject.Find("BorrarMapas").GetComponent<Button>().interactable = false;
        }
    }
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase esté habilitade por primera vez.
    /// </summary>
    void Start()
    {
        GenerarMapa.mapaActualPartida = null;
        PanelHerramientas = GameObject.Find("Panel");
        pantallaGuardar = GameObject.Find("PantallaGuardar");
        pantallaCargar = GameObject.Find("PantallaCargar");
        pantallaBorrado = GameObject.Find("PantallaBorrar");
        pantallaGuardar.SetActive(false);
        pantallaCargar.SetActive(false);
        pantallaBorrado.SetActive(false);
        ComprobarSiExistenMapasGuardados();
        if (mapaActualEditor == null)
        {
            GenerarCuadriculas();
        }
        else
        {
            CargarMapaAlEditor();
        }
        //rectTrans.localPosition= new Vector3(0, 0, 0);
    }
}
