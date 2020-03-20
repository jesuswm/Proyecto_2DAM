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
    /// Función que genera el mapa de la escena en función de un archivo
    /// </summary>
    public void generarMapa()
    {
        tilemapSuelo = terrenoTraspasable.GetComponent<Tilemap>();
        tilemapMuro = terrenoNoTraspasable.GetComponent<Tilemap>();
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
            break;
        }
        foreach (ObjetoMapa arbusto in map.Arbusto)
        {
            Vector3 vec = tilemapMuro.CellToWorld(new Vector3Int(arbusto.X, arbusto.Y, arbusto.Z));
            vec = new Vector3(vec.x + 0.08f, vec.y + 0.08f, vec.z);
            Instantiate(Resources.Load<GameObject>("Arbusto"), vec, Quaternion.identity);
        }
        foreach (EnemigoMapa enemigos in map.Enemigo)
        {

            Vector3 vec = tilemapMuro.CellToWorld(new Vector3Int(enemigos.X, enemigos.Y, enemigos.Z));
            vec = new Vector3(vec.x + 0.08f, vec.y + 0.16f, vec.z);
            if (Enemigos.obtenerEnemigo(enemigos.TipoEnemigo) != null)
            {
                Instantiate(Enemigos.obtenerEnemigo((eEnemigo)enemigos.TipoEnemigo), vec, Quaternion.identity);
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
    }
    void Start()
    {
        
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
        
    }
}
