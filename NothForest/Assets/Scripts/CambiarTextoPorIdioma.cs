using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Clase que se encarga de establecer los textos del elemento en función del idioma actual del juego
/// </summary>
///  <remarks>
/// Para el funcionamiento de esta clase es necesario que el elemento también tenga asociado
/// <list type="bullet">
/// <item><see cref="Text"/></item>
/// </list>
/// </remarks>
public class CambiarTextoPorIdioma : MonoBehaviour
{
    /// <summary>
    /// Elemento donde se va a establecer el texto
    /// </summary>
    Text texto;
    /// <summary>
    /// Índice del idioma actual de juego
    /// </summary>
    int idioma;
    /// <summary>
    /// Valor de enumerado asociado a la palabra que buscamos
    /// </summary>
    public palabras palabra;
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase está habilitado por primera vez antes de la función update 
    /// en ella se obtiene el Text que posee el elemento y se le asigna a la variable palabra y establece el valor del texto al correspondiente a la configuración de idioma del juego. 
    /// </summary>
    void Start()
    {
        texto = transform.gameObject.GetComponent<Text>();
        idioma = Palabras.IdiomaActual;
        texto.text = Palabras.getPalabra(palabra);
    }

    /// <summary>
    /// Función que se llama cada frame mientras el elemento que posee esta clase esté habilitado
    /// en ella se comprueba si se ha cambiado el idioma de la aplicación en caso de que ocurriera cambiaria el texto por el correspondiente. 
    /// </summary>
    void Update()
    {
        if (idioma != Palabras.IdiomaActual)
        {
            texto.text = Palabras.getPalabra(palabra);
            idioma = Palabras.IdiomaActual;
        }
    }
}
