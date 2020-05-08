using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
/// <summary>
/// Clase que asigna la función del botón de guardado del teclado
/// </summary>
public class BotonGuardarTeclado : MonoBehaviour
{
    /// <summary>
    /// Script de CreadorDeCuadriculas usado en el editor
    /// </summary>
    public CreadorDeCuadriculas creadorDeCuadriculas;
    /// <summary>
    /// Text en el que se encuentra introducido el nombre con el que se desea guardar el mapa
    /// </summary>
    public Text cuadroTexto;
    /// <summary>
    /// Boton de guardado
    /// </summary>
    private Button buttonGuardar;
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase esté habilitade por primera vez.
    /// </summary>
    void Start()
    {
        if (creadorDeCuadriculas == null)
        {
            creadorDeCuadriculas = GameObject.Find("Celdas").GetComponent<CreadorDeCuadriculas>();
        }
        if (cuadroTexto == null)
        {
            cuadroTexto = GameObject.Find("CuadroDeTexto").GetComponent<Text>();
        }
        buttonGuardar = GetComponent<Button>();
        buttonGuardar.onClick.AddListener(delegate () { CreadorDeCuadriculas.mapaActualEditor = cuadroTexto.text;
            creadorDeCuadriculas.guardarMapa(cuadroTexto.text);
            creadorDeCuadriculas.ComprobarSiExistenMapasGuardados();
            creadorDeCuadriculas.AbrirCerrarMenuGuardar();
        });
    }
}
