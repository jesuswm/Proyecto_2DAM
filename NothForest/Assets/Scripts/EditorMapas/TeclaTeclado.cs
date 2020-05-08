using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Clase que se encarga de gestionar el funcionamiento de las teclas del teclado que se muestra para poner nombre al mapa.
/// </summary>
public class TeclaTeclado : MonoBehaviour
{
    /// <summary>
    /// Cuadro de texto que se modificara la pulsar la tecla
    /// </summary>
    private Text cuadroTexto;
    /// <summary>
    /// Texto de la tecla
    /// </summary>
    private Text miTexto;
    /// <summary>
    /// Booleana que indica si la tecla borra o escribe
    /// </summary>
    public bool borrar=false;
    /// <summary>
    /// Máximo número de caracteres que puede tener el cuadro de texto.
    /// </summary>
    private int maxCaracteres=12;
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase esté habilitade por primera vez.
    /// </summary>
    void Start()
    {
        miTexto = transform.GetChild(0).gameObject.GetComponent<Text>();
        cuadroTexto = GameObject.Find("CuadroDeTexto").GetComponent<Text>();
        GetComponent<Button>().onClick.AddListener(pulsar);
    }
    /// <summary>
    /// Función que gestiona la pulsación de la tecla
    /// </summary>
    public void pulsar()
    {
        if (!borrar)
        {
            if (cuadroTexto.text.Length < maxCaracteres)
            {
                cuadroTexto.text = cuadroTexto.text + miTexto.text;
            }
        }
        else
        {
            if (cuadroTexto.text.Length > 0)
            {
                cuadroTexto.text=cuadroTexto.text.Substring(0, cuadroTexto.text.Length - 1);
            }
        }
    }
}
