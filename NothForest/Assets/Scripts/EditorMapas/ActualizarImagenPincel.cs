using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Clase que se encarga del cambio de la imagen (en la barra de herramientas) según el pincel seleccionado
/// </summary>
public class ActualizarImagenPincel : MonoBehaviour
{
    /// <summary>
    /// Spriterender que gestiona la imagen
    /// </summary>
    SpriteRenderer spriteRenderer;
    /// <summary>
    /// Dropdown con la lista de pinceles
    /// </summary>
    Dropdown pincel;
    /// <summary>
    /// Dropdown con la lista de herramientas
    /// </summary>
    Dropdown selectorHerramienta;
    void Start()
    {
        spriteRenderer = GameObject.Find("ImageTile").GetComponent<SpriteRenderer>();
        pincel = GameObject.Find("Pincel").GetComponent<Dropdown>();
        selectorHerramienta = GameObject.Find("Herramienta").GetComponent<Dropdown>();
    }

    
    void Update()
    {
        
    }
    /// <summary>
    /// Función que actualiza la imagen mostrada en función de la herramienta y el pincel actual
    /// </summary>
    public void ColocarImagenActual()
    {
        if (selectorHerramienta.value == 0)
        {
            spriteRenderer.sprite = Tiles.obtenerTile((eTiles)pincel.value).sprite;
        }
        else if(selectorHerramienta.value == 1)
        {
            switch (pincel.value)
            {
                case 0:
                    spriteRenderer.sprite= Resources.Load<Sprite>("MocoImg");
                    break;
                case 1:
                    spriteRenderer.sprite= Resources.Load<Sprite>("TronquitoImg");
                    break;
                case 2:
                    spriteRenderer.sprite= Resources.Load<Sprite>("OrcoImg");
                    break;
            }
        }
        else if (selectorHerramienta.value == 4)
        {
            switch (pincel.value)
            {
                case 0:
                    spriteRenderer.sprite = Resources.Load<Sprite>("ArbolImg");
                    break;
            }
        }
        else
        {
            spriteRenderer.sprite = null;
        }
    }
}
