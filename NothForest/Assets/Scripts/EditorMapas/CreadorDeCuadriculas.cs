using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
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
    public void CargarMapa()
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
                gameObject.GetComponent<PulsarCuadricula>().actualizarEvento(eventosMapa.ninguno);
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
                            gameO.GetComponent<PulsarCuadricula>().actualizarEvento(eventosMapa.ninguno);
                        }
                    }
                }
                gameObject.GetComponent<PulsarCuadricula>().actualizarEvento(eventosMapa.ninguno);
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
