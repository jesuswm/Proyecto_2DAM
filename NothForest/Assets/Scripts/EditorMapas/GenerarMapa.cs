using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Clase que genera un mapa en la escena en función de un archivo
/// </summary>
public class GenerarMapa : MonoBehaviour
{
    /// <summary>
    /// GameObject con el tilemap sin colaider de la escena
    /// </summary>
    public GameObject terrenoTraspasable;
    /// <summary>
    /// GameObject con el tilemap con colaider de la escena
    /// </summary>
    public GameObject terrenoNoTraspasable;
    /// <summary>
    /// Tilemap de <see cref="terrenoTraspasable"/>
    /// </summary>
    private Tilemap tilemapSuelo;
    /// <summary>
    /// Tilemap de <see cref="terrenoNoTraspasable"/>
    /// </summary>
    private Tilemap tilemapMuro;
    /// <summary>
    /// Booleana que indica si el mapa se genera en el momento en el que se carga la escena
    /// </summary>
    public bool auto=false;
    /// <summary>
    /// Tamaño del mapa en cuadriculas del eje x
    /// </summary>
    public int anchoMapa=50;
    /// <summary>
    /// Tamaño del mapa en cuadriculas del eje y
    /// </summary>
    public int altoMapa=50;
    /// <summary>
    /// Tile que se usara para generar el limite máximo del mapa
    /// </summary>
    public Tile tileLimete;
    /// <summary>
    /// Booleana que indica si el mapa es jugable
    /// </summary>
    bool jugable = false;
    /// <summary>
    /// GameObject con toda la interfaz de controles
    /// </summary>
    public GameObject controles;
    /// <summary>
    /// GameObject con la pestaña de error
    /// </summary>
    public GameObject pantallaError;
    /// <summary>
    /// GameObject en el que se añaden todos los enemigos del mapa
    /// </summary>
    public GameObject ObjetosEnemigos;
    /// <summary>
    /// Función que genera los límites máximos del mapa
    /// </summary>
    public void generarLimitesMapa()
    {
        if (tileLimete != null)
        {
            for (int i = 0; i < anchoMapa; i++)
            {
                tilemapMuro.SetTile(new Vector3Int(i, -1, 0), tileLimete);
            }
            for (int i = 0; i < anchoMapa; i++)
            {
                tilemapMuro.SetTile(new Vector3Int(i, altoMapa, 0), tileLimete);
            }
            for (int i = 0; i < altoMapa; i++)
            {
                tilemapMuro.SetTile(new Vector3Int(-1, i, 0), tileLimete);
            }
            for (int i = 0; i < altoMapa; i++)
            {
                tilemapMuro.SetTile(new Vector3Int(anchoMapa, i, 0), tileLimete);
            }
        }
    }
    /// <summary>
    /// Función que genera el mapa de la escena en función de un archivo
    /// </summary>
    public void generarMapa()
    {
        tilemapSuelo = terrenoTraspasable.GetComponent<Tilemap>();
        tilemapMuro = terrenoNoTraspasable.GetComponent<Tilemap>();
        generarLimitesMapa();
        List<ObjetoMapa> objetos = CrearArchivo.cargarObjetosMapa();
        Mapa map = new Mapa(objetos);
        foreach (TileMapa terreno in map.TerrenoTraspasable)
        {
            if (terreno.Traspasable)
            {
                tilemapSuelo.SetTile(new Vector3Int(terreno.X, terreno.Y, 0), Tiles.obtenerTile(terreno.Tile));
            }
            else
            {
                tilemapMuro.SetTile(new Vector3Int(terreno.X, terreno.Y, 0), Tiles.obtenerTile((eTiles)terreno.Tile));
            }
        }
        foreach (JugadorMapa jugador in map.Jugador)
        {
            Vector3 vec = tilemapMuro.CellToWorld(new Vector3Int(jugador.X, jugador.Y, jugador.Z));
            vec = new Vector3(vec.x + 0.08f, vec.y + 0.16f, vec.z);
            Instantiate(Resources.Load<GameObject>("Jugador"), vec, Quaternion.identity).name="Jugador";
            jugable = true;
            break;
        }
        foreach (ObjetoMapa arbusto in map.Arbusto)
        {
            Vector3 vec = tilemapMuro.CellToWorld(new Vector3Int(arbusto.X, arbusto.Y, arbusto.Z));
            vec = new Vector3(vec.x + 0.08f, vec.y + 0.08f, vec.z);
            Instantiate(Resources.Load<GameObject>("Arbusto"), vec, Quaternion.identity);
        }
        if (jugable)
        {
            foreach (EnemigoMapa enemigos in map.Enemigo)
            {

                Vector3 vec = tilemapMuro.CellToWorld(new Vector3Int(enemigos.X, enemigos.Y, enemigos.Z));
                vec = new Vector3(vec.x + 0.08f, vec.y + 0.16f, vec.z);
                if (Enemigos.obtenerEnemigo(enemigos.TipoEnemigo) != null)
                {
                    GameObject enemi=Instantiate(Enemigos.obtenerEnemigo((eEnemigo)enemigos.TipoEnemigo), vec, Quaternion.identity);
                    enemi.transform.parent = ObjetosEnemigos.transform;
                }
            }
        }
        foreach (ObstaculosMapa obstaculo in map.Obstaculos)
        {

            Vector3 vec = tilemapMuro.CellToWorld(new Vector3Int(obstaculo.X, obstaculo.Y, obstaculo.Z));
            vec = new Vector3(vec.x + 0.16f, vec.y + 0.16f, vec.z);
            if (Obstaculos.obtenerObstaculo(obstaculo.TipoObstaculo) != null)
            {
                Instantiate(Obstaculos.obtenerObstaculo(obstaculo.TipoObstaculo), vec, Quaternion.identity);
            }
        }
        Debug.Log("childCount: "+ObjetosEnemigos.transform.childCount);
        if (ObjetosEnemigos.transform.childCount == 0)
        {
            jugable = false;
        }
    }
    void Start()
    {
        if (!jugable)
        {
            //controles.SetActive(false);
            for (int i = 0; i < transform.childCount; ++i)
            {
                controles.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else
        {
            pantallaError.SetActive(false);
        }
    }
    void Awake()
    {
        if (auto)
        {
            generarMapa();
        }
    }

    void Update()
    {
        if (jugable)
        {
            if (ObjetosEnemigos.transform.childCount == 0)
            {
                controles.GetComponent<GestionPantallasPartida>().Fin = true;
            }
        }
    }
}
