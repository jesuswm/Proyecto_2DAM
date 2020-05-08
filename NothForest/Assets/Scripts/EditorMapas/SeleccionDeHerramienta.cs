using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Enumerado de las herramientas que usa el editor
/// </summary>
public enum eHerramientas
{
    suelo,enemigo,jugador,arbusto,obstaculo,borrar,mover
}
/// <summary>
/// Clase que gestiona la selección de herramientas
/// </summary>
public class SeleccionDeHerramienta : MonoBehaviour
{
    /// <summary>
    /// Herramienta que se encuentra activa en el editor
    /// </summary>
    public eHerramientas herramientaActual=eHerramientas.suelo;
    /// <summary>
    /// Array en el que se almacena el pincel actual de cada herramienta
    /// </summary>
    int[] pincelActual;
    /// <summary>
    /// Toggle asociado a la herramienta suelo
    /// </summary>
    public Toggle tsuelo;
    /// <summary>
    /// Toggle asociado a la herramienta enemigo
    /// </summary>
    public Toggle tenemigo;
    /// <summary>
    /// Toggle asociado a la herramienta jugador
    /// </summary>
    public Toggle tjugador;
    /// <summary>
    /// Toggle asociado a la herramienta arbusto
    /// </summary>
    public Toggle tarbusto;
    /// <summary>
    /// Toggle asociado a la herramienta obstaculo
    /// </summary>
    public Toggle tobstaculo;
    /// <summary>
    /// Toggle asociado a la herramienta borrar
    /// </summary>
    public Toggle tborrar;
    /// <summary>
    /// Toggle asociado a la herramienta mover
    /// </summary>
    public Toggle tmover;
    /// <summary>
    /// GameObject que contiene el Dropdown de pinceles
    /// </summary>
    GameObject pincel;
    /// <summary>
    /// Lista de pinceles de la herramienta suelos
    /// </summary>
    List<Dropdown.OptionData> listsSuelos=new List<Dropdown.OptionData>();
    /// <summary>
    /// Lista de pinceles de la herramienta enemigos
    /// </summary>
    List<Dropdown.OptionData> listsEnemigos= new List<Dropdown.OptionData>();
    /// <summary>
    /// Lista de pinceles de la herramienta obstaculos
    /// </summary>
    List<Dropdown.OptionData> listsObstaculos = new List<Dropdown.OptionData>();
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase esté habilitade por primera vez.
    /// </summary>
    void Start()
    {
        pincelActual = new int[Enum.GetNames(typeof(eHerramientas)).Length];
        for(int i=0;i< pincelActual.Length; i++)
        {
            pincelActual[i] = 0;
        }
        tsuelo.onValueChanged.AddListener(delegate {
            OnCambioHerramienta(eHerramientas.suelo,tsuelo);
        });
        tenemigo.onValueChanged.AddListener(delegate {
            OnCambioHerramienta(eHerramientas.enemigo, tenemigo);
        });
        tjugador.onValueChanged.AddListener(delegate {
            OnCambioHerramienta(eHerramientas.jugador, tjugador);
        });
        tarbusto.onValueChanged.AddListener(delegate {
            OnCambioHerramienta(eHerramientas.arbusto, tarbusto);
        });
        tobstaculo.onValueChanged.AddListener(delegate {
            OnCambioHerramienta(eHerramientas.obstaculo, tobstaculo);
        });
        tborrar.onValueChanged.AddListener(delegate {
            OnCambioHerramienta(eHerramientas.borrar, tborrar);
        });
        tmover.onValueChanged.AddListener(delegate {
            OnCambioHerramienta(eHerramientas.mover, tmover);
        });
        pincel = GameObject.Find("Pincel");
        /// u21D0 u21D1 u21D2 u21D3 u2196 u2197 u2198 u2199
        listsSuelos.Add(new Dropdown.OptionData(Palabras.getPalabra(palabras.Cesped), Tiles.obtenerTile(eTiles.Cesped).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2196" + Palabras.getPalabra(palabras.CespedCurva), Tiles.obtenerTile(eTiles.Cesped_CurvaNO).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2197" + Palabras.getPalabra(palabras.CespedCurva), Tiles.obtenerTile(eTiles.Cesped_CurvaNE).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2199" + Palabras.getPalabra(palabras.CespedCurva), Tiles.obtenerTile(eTiles.Cesped_CurvaSO).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2198" + Palabras.getPalabra(palabras.CespedCurva), Tiles.obtenerTile(eTiles.Cesped_CurvaSE).sprite));
        listsSuelos.Add(new Dropdown.OptionData(Palabras.getPalabra(palabras.Agua), Tiles.obtenerTile(eTiles.Agua).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2190" + Palabras.getPalabra(palabras.AguaBorde), Tiles.obtenerTile(eTiles.Agua_BordeO).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2191" + Palabras.getPalabra(palabras.AguaBorde), Tiles.obtenerTile(eTiles.Agua_BordeN).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2192" + Palabras.getPalabra(palabras.AguaBorde), Tiles.obtenerTile(eTiles.Agua_BordeE).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2193" + Palabras.getPalabra(palabras.AguaBorde), Tiles.obtenerTile(eTiles.Agua_BordeS).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2196" + Palabras.getPalabra(palabras.AguaEsquina), Tiles.obtenerTile(eTiles.Agua_EsquinaNO).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2197" + Palabras.getPalabra(palabras.AguaEsquina), Tiles.obtenerTile(eTiles.Agua_EsquinaNE).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2199" + Palabras.getPalabra(palabras.AguaEsquina), Tiles.obtenerTile(eTiles.Agua_EsquinaSO).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2198" + Palabras.getPalabra(palabras.AguaEsquina), Tiles.obtenerTile(eTiles.Agua_EsquinaSE).sprite));
        listsEnemigos.Add(new Dropdown.OptionData("Moco", Resources.Load<Sprite>("MocoImg")));
        listsEnemigos.Add(new Dropdown.OptionData("Tronquito", Resources.Load<Sprite>("TronquitoImg")));
        listsEnemigos.Add(new Dropdown.OptionData("Orco", Resources.Load<Sprite>("OrcoImg")));
        listsObstaculos.Add(new Dropdown.OptionData(Palabras.getPalabra(palabras.Arbol), Resources.Load<Sprite>("ArbolImg")));
        listsObstaculos.Add(new Dropdown.OptionData(Palabras.getPalabra(palabras.Tocon), Resources.Load<Sprite>("ToconImg")));
        listsObstaculos.Add(new Dropdown.OptionData(Palabras.getPalabra(palabras.RocaGrande), Resources.Load<Sprite>("RocaGrandeImg")));
        pincel.GetComponent<Dropdown>().value = 0;
        pincel.GetComponent<Dropdown>().options = listsSuelos;
    }

    /// <summary>
    /// Función que actualiza la barra de herramientas en función de la herramienta seleccionada
    /// </summary>
    
    public void OnCambioHerramienta(eHerramientas herramienta,Toggle actual)
    {
        if (actual.isOn)
        {
            pincelActual[(int)herramientaActual] = pincel.GetComponent<Dropdown>().value;
            switch (herramienta)
            {
                case eHerramientas.suelo:
                    pincel.SetActive(true);
                    pincel.GetComponent<Dropdown>().value = 0;
                    pincel.GetComponent<Dropdown>().options = listsSuelos;
                    pincel.GetComponent<Dropdown>().value = pincelActual[(int)herramienta];
                    break;
                case eHerramientas.enemigo:
                    pincel.SetActive(true);
                    pincel.GetComponent<Dropdown>().value = 0;
                    pincel.GetComponent<Dropdown>().options = listsEnemigos;
                    pincel.GetComponent<Dropdown>().value = pincelActual[(int)herramienta];
                    break;
                case eHerramientas.obstaculo:
                    pincel.SetActive(true);
                    pincel.GetComponent<Dropdown>().value = 0;
                    pincel.GetComponent<Dropdown>().options = listsObstaculos;
                    pincel.GetComponent<Dropdown>().value = pincelActual[(int)herramienta];
                    break;
                default:
                    pincel.SetActive(false);
                    break;
            }
            herramientaActual = herramienta;
            PulsarCuadricula.pulsado = false;
        }

    }
}
