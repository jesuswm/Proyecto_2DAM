using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
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
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
