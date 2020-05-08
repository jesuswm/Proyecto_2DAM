using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
/// <summary>
/// Clase que se encarga de actualizar el mensaje de error si el nombre dado al mapa no es válido o ya existe.
/// </summary>
public class MensajeErrorNombreMapa : MonoBehaviour
{
    /// <summary>
    /// Text en el que se encuentra introducido el nombre con el que se desea guardar el mapa
    /// </summary>
    public Text cuadroTexto;
    /// <summary>
    /// Botón de guardado de la pantalla de guardar mapa
    /// </summary>
    public Button botonGuardar;
    /// <summary>
    /// Text donde se visualizará el error en caso de que exista
    /// </summary>
    private Text textoError;
    /// <summary>
    /// Lista donde se guardaran los nombres de los mapas ya guardados.
    /// </summary>
    private List<string> nombresMapas=new List<string>();
    /// <summary>
    /// Función que consulta los archivos de mapas almacenados para obtener sus nombres.
    /// </summary>
    /// <returns>Lista con los nombres de los nombres de mapas usados</returns>
    public List<string> obtenerNombresMapas()
    {
        List<string> nombres=new List<string>();
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] info = dir.GetFiles("*.map");
        foreach (FileInfo f in info)
        {
            nombres.Add(f.Name.Substring(0, f.Name.Length - 4));
        }
        return nombres;
    }
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase cambia al estado habilitado.
    /// </summary>
    void OnEnable()
    {
        nombresMapas = obtenerNombresMapas();
        cuadroTexto.text = CreadorDeCuadriculas.mapaActualEditor;
    }
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase esté habilitade por primera vez.
    /// </summary>
    void Start()
    {
        nombresMapas = obtenerNombresMapas();
        if (cuadroTexto == null)
        {
            cuadroTexto = GameObject.Find("CuadroDeTexto").GetComponent<Text>();
        }
        if (botonGuardar == null)
        {
            botonGuardar = GameObject.Find("ButtonGuardar").GetComponent<Button>();
        }
        cuadroTexto.text = CreadorDeCuadriculas.mapaActualEditor;
        textoError = GetComponent<Text>();
        textoError.text = null;
    }
    /// <summary>
    /// Función que se llama cada frame mientras que el elemento que posee esta clase esta habilitado.
    /// </summary>
    void Update()
    {
        if (cuadroTexto.text.Trim() == "")
        {
            textoError.color = Color.red;
            //textoError.text = "Nombre no válido";
            textoError.text = Palabras.getPalabra(palabras.GuardadoNombreNoValido);
            botonGuardar.interactable = false;
        }
        else if (nombresMapas.Contains(cuadroTexto.text))
        {
            textoError.color = Color.yellow;
            //textoError.text = "Ya existe un mapa con ese nombre si se guarda ahora se sobrescribirá";
            textoError.text = Palabras.getPalabra(palabras.GuardadoNombreYaExiste);
            botonGuardar.interactable = true;
        }
        else
        {
            textoError.text = null;
            botonGuardar.interactable = true;
        }
    }
}
