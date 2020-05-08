using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
/// <summary>
/// Clase que se encarga de gestionar el funcionamiento de la pantalla de carga.
/// </summary>
public class PantallaCargar : MonoBehaviour
{
    /// <summary>
    /// Script de CreadorDeCuadriculas usado en el editor
    /// </summary>
    public CreadorDeCuadriculas creadorDeCuadriculas;
    /// <summary>
    /// Botón de cargar
    /// </summary>
    public Button buttonCargar;
    /// <summary>
    /// dropdownMapas donde se mostraran los mapas existentes
    /// </summary>
    public Dropdown dropdownMapas;
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase cambia al estado habilitado.
    /// </summary>
    void OnEnable()
    {
        List<Dropdown.OptionData> listsMapa = new List<Dropdown.OptionData>();
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] info = dir.GetFiles("*.map");
        foreach (FileInfo fileInfo in info)
        {
            listsMapa.Add(new Dropdown.OptionData(fileInfo.Name.Substring(0, fileInfo.Name.Length-4)));
        }
        dropdownMapas.options = listsMapa;
        dropdownMapas.value = 0;
    }
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase esté habilitade por primera vez.
    /// </summary>
    void Start()
    {
        if (creadorDeCuadriculas == null)
        {
            creadorDeCuadriculas = GameObject.Find("Celdas").GetComponent<CreadorDeCuadriculas>();
        }
        if (buttonCargar == null)
        {
            buttonCargar = GameObject.Find("ButtonCargar").GetComponent<Button>();
        }
        if(dropdownMapas == null)
        {
            dropdownMapas= GameObject.Find("DropdownMapas").GetComponent<Dropdown>(); 
        }
        buttonCargar.onClick.AddListener(pulsar);
    }
    /// <summary>
    /// Función que gestiona la pulsación del botón cargar de la pantalla de carga.
    /// </summary>
    public void pulsar()
    {
        //Debug.Log(dropdownMapas.options[dropdownMapas.value].text);
        CreadorDeCuadriculas.mapaActualEditor = dropdownMapas.options[dropdownMapas.value].text;
        creadorDeCuadriculas.CargarMapaAlEditor();
        creadorDeCuadriculas.AbrirCerrarMenuCargar();
    }
}
