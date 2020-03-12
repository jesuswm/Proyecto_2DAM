using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GenerarMapa : MonoBehaviour
{
    public GameObject terrenoTraspasable;
    public GameObject terrenoNoTraspasable;
    private Tilemap tilemapSuelo;
    private Tilemap tilemapMuro;
    int ancho;
    int alto;
    // Start is called before the first frame update
    void Start()
    {
        tilemapSuelo = terrenoTraspasable.GetComponent<Tilemap>();
        tilemapMuro = terrenoNoTraspasable.GetComponent<Tilemap>();

        //List<ObjetoMapa> objetos=new List<ObjetoMapa>();
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped,eTipo.TerrenoTras,0,0,0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 1, 0, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 2, 0, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 3, 0, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 4, 0, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 5, 0, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 6, 0, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 7, 0, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 8, 0, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 0, 1, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 1, 1, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 2, 1, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 3, 1, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 4, 1, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 5, 1, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 6, 1, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 7, 1, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 8, 1, 0));
        //objetos.Add(new ObjetoMapa((int)eEnemigo.Tronquito, eTipo.Enemigo, 4, 1, 0));
        CrearArchivo.GuardarObjetosMapa();
        List<ObjetoMapa> objetos = CrearArchivo.cargarObjetosMapa();
        Mapa map = new Mapa(objetos);
        foreach (ObjetoMapa terreno in map.TerrenoTraspasable)
        {
            Debug.Log("Terreno traspasable");
            tilemapSuelo.SetTile(new Vector3Int(terreno.X, terreno.Y, 0), Tiles.obtenerTile((eTiles)terreno.Id));
        }
        foreach (ObjetoMapa terreno in map.TerrenoNoTraspasable)
        {
            Debug.Log("Terreno No traspasable");
            tilemapMuro.SetTile(new Vector3Int(terreno.X, terreno.Y, 0), Tiles.obtenerTile((eTiles)terreno.Id));
        }
        foreach (ObjetoMapa terreno in map.TerrenoNoTraspasable)
        {
            Debug.Log("Terreno No traspasable");
            tilemapMuro.SetTile(new Vector3Int(terreno.X, terreno.Y, 0), Tiles.obtenerTile((eTiles)terreno.Id));
        }
        foreach (ObjetoMapa enemigos in map.Enemigo)
        {
            Debug.Log("Terreno No traspasable");
            Vector3 vec = tilemapMuro.CellToWorld(new Vector3Int(enemigos.X, enemigos.Y, enemigos.Z));
            vec = new Vector3(vec.x + 0.08f, vec.y + 0.08f, vec.z);
            if (Enemigos.obtenerEnemigo((eEnemigo)enemigos.Id ) != null){
                Instantiate(Enemigos.obtenerEnemigo((eEnemigo)enemigos.Id), vec, Quaternion.identity);
            }
        }

        //int x = 0;
        //int y = 0;
        //ancho = 10;
        //alto = 10;
        //for (int j = 0; j < alto; j++)
        //{
        //    for (int i = 0; i < ancho; i++)
        //    {
        //        if (y != 0 && y != alto - 1)
        //        {
        //            if (x != 0 && x != ancho - 1)
        //            {
        //                tilemapSuelo.SetTile(new Vector3Int(x, y, 0), Tiles.obtenerTile(eTiles.Cesped));
        //            }
        //            else
        //            {
        //                tilemapMuro.SetTile(new Vector3Int(x, y, 0), Tiles.obtenerTile(eTiles.Agua));
        //            }
        //        }
        //        else
        //        {
        //            tilemapMuro.SetTile(new Vector3Int(x, y, 0), Tiles.obtenerTile(eTiles.Agua));
        //        }
        //        x++;
        //    }
        //    x = 0;
        //    y++;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
