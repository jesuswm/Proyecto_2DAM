using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Clase que gestiona la selección de herramientas
/// </summary>
public class SeleccionDeHerramienta : MonoBehaviour
{
    /// <summary>
    /// Dropdown de la escena con la lista de herramientas
    /// </summary>
    Dropdown selectorHerramienta;
    /// <summary>
    /// Imagen de la herramienta seleccionada
    /// </summary>
    SpriteRenderer spriteRendererHerramienta;
    /// <summary>
    /// GameObject que contiene el Dropdown de pinceles
    /// </summary>
    GameObject pincel;
    /// <summary>
    /// Toggle de la escena con la que se establece si estraspasable o no la celda
    /// </summary>
    GameObject togleTraspasable;
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
    // Start is called before the first frame update
    void Start()
    {
        selectorHerramienta= GameObject.Find("Herramienta").GetComponent<Dropdown>();
        spriteRendererHerramienta = GameObject.Find("ImageHerramienta").GetComponent<SpriteRenderer>();
        pincel = GameObject.Find("Pincel");
        togleTraspasable = GameObject.Find("TogleTraspasable");
        /// u21D0 u21D1 u21D2 u21D3 u2196 u2197 u2198 u2199
        listsSuelos.Add(new Dropdown.OptionData("Cesped", Tiles.obtenerTile(eTiles.Cesped).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2196Cesped Curva", Tiles.obtenerTile(eTiles.Cesped_CurvaNO).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2197Cesped Curva", Tiles.obtenerTile(eTiles.Cesped_CurvaNE).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2199Cesped Curva", Tiles.obtenerTile(eTiles.Cesped_CurvaSO).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2198Cesped Curva", Tiles.obtenerTile(eTiles.Cesped_CurvaSE).sprite));
        listsSuelos.Add(new Dropdown.OptionData("Agua", Tiles.obtenerTile(eTiles.Agua).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2190Agua Borde", Tiles.obtenerTile(eTiles.Agua_BordeO).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2191Agua Borde", Tiles.obtenerTile(eTiles.Agua_BordeN).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2192Agua Borde", Tiles.obtenerTile(eTiles.Agua_BordeE).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2193Agua Borde", Tiles.obtenerTile(eTiles.Agua_BordeS).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2196Agua Esquina", Tiles.obtenerTile(eTiles.Agua_EsquinaNO).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2197Agua Esquina", Tiles.obtenerTile(eTiles.Agua_EsquinaNE).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2199Agua Esquina", Tiles.obtenerTile(eTiles.Agua_EsquinaSO).sprite));
        listsSuelos.Add(new Dropdown.OptionData("\u2198Agua Esquina", Tiles.obtenerTile(eTiles.Agua_EsquinaSE).sprite));
        listsEnemigos.Add(new Dropdown.OptionData("Moco", Resources.Load<Sprite>("MocoImg")));
        listsEnemigos.Add(new Dropdown.OptionData("Tronquito", Resources.Load<Sprite>("TronquitoImg")));
        listsEnemigos.Add(new Dropdown.OptionData("Orco", Resources.Load<Sprite>("OrcoImg")));
        listsObstaculos.Add(new Dropdown.OptionData("Arbol", Resources.Load<Sprite>("ArbolImg")));
        pincel.GetComponent<Dropdown>().value = 0;
        pincel.GetComponent<Dropdown>().options = listsSuelos;
    }

    /// <summary>
    /// Función que actualiza la barra de herramientas en función de la herramienta seleccionada
    /// </summary>
    public void OnCambioHerramienta()
    {
        switch (selectorHerramienta.value)
        {
            case 0:
                spriteRendererHerramienta.sprite = null;
                pincel.SetActive(true);
                togleTraspasable.SetActive(true);
                pincel.GetComponent<Dropdown>().value = 0;
                pincel.GetComponent<Dropdown>().options = listsSuelos;
                //pincel.GetComponent<ActualizarImagenPincel>().ColocarImagenActual();
                break;
            case 1:
                spriteRendererHerramienta.sprite = null;
                togleTraspasable.SetActive(false);
                pincel.SetActive(true);
                pincel.GetComponent<Dropdown>().value = 0;
                pincel.GetComponent<Dropdown>().options = listsEnemigos;
                //pincel.GetComponent<ActualizarImagenPincel>().ColocarImagenActual();
                break;
            case 2:
                spriteRendererHerramienta.sprite = Resources.Load<Sprite>("PersonajeImg");
                pincel.SetActive(false);
                togleTraspasable.SetActive(false);
                break;
            case 3:
                spriteRendererHerramienta.sprite = Resources.Load<Sprite>("ArbustoImg");
                pincel.SetActive(false);
                togleTraspasable.SetActive(false);
                break;
            case 4:
                //spriteRendererHerramienta.sprite = null;
                //pincel.SetActive(false);
                //togleTraspasable.SetActive(false);
                spriteRendererHerramienta.sprite = null;
                togleTraspasable.SetActive(false);
                pincel.SetActive(true);
                pincel.GetComponent<Dropdown>().value = 0;
                pincel.GetComponent<Dropdown>().options = listsObstaculos;
                //pincel.GetComponent<ActualizarImagenPincel>().ColocarImagenActual();
                break;
            case 5:
                spriteRendererHerramienta.sprite = null;
                pincel.SetActive(false);
                togleTraspasable.SetActive(false);
                break;
            case 6:
                spriteRendererHerramienta.sprite = null;
                pincel.SetActive(false);
                togleTraspasable.SetActive(false);
                break;
        }
        
    }
}
