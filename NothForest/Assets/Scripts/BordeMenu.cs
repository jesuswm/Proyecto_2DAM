using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase que se encarga de ajustar el tamaño del BoxCollider2D asociado al objeto al alto de la pantalla actual
/// </summary>
/// <remarks>
/// Para el funcionamiento de esta clase es necesario que en la escena existan los elementos con los siguientes nombres
/// <list type="bullet">
/// <item>Canvas</item>
/// </list>
/// Además para el funcionamiento de esta clase es necesario que el elemento también  tenga asociado un <see cref="BoxCollider2D"/>
/// </remarks>
public class BordeMenu : MonoBehaviour
{
    // Start is called before the first frame update
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase esté habilitado por primera vez antes de update.
    /// En ella se inicializan la variables en función a los elementos en la escena y se ajusta el tamaño del boxColaider al tamaño del menú
    /// </summary>
    void Start()
    {
        GameObject canvas= GameObject.Find("Canvas");
        GetComponent<BoxCollider2D>().size = new Vector2(2f, canvas.GetComponent<RectTransform>().rect.height);
    }
}
