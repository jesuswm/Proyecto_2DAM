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
        listsSuelos.Add(new Dropdown.OptionData("Cesped"));
        listsSuelos.Add(new Dropdown.OptionData("Agua"));
        listsEnemigos.Add(new Dropdown.OptionData("Moco"));
        listsEnemigos.Add(new Dropdown.OptionData("Tronquito"));
        listsEnemigos.Add(new Dropdown.OptionData("Orco"));
        listsObstaculos.Add(new Dropdown.OptionData("Arbol"));
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
                pincel.GetComponent<ActualizarImagenPincel>().ColocarImagenActual();
                break;
            case 1:
                spriteRendererHerramienta.sprite = null;
                togleTraspasable.SetActive(false);
                pincel.SetActive(true);
                pincel.GetComponent<Dropdown>().value = 0;
                pincel.GetComponent<Dropdown>().options = listsEnemigos;
                pincel.GetComponent<ActualizarImagenPincel>().ColocarImagenActual();
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
                pincel.GetComponent<ActualizarImagenPincel>().ColocarImagenActual();
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
