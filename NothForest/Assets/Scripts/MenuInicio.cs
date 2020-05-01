using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// Clase que se encarga de la gestión del menú principal y establecer los valores de la pestaña de estadísticas
/// </summary>
/// <remarks>
/// Para el funcionamiento de esta clase es necesario que en la escena existan los elementos con los siguientes nombres
/// <list type="bullet">
/// <item>MenuPrincipal</item>
/// <item>MenuOpciones</item>
/// <item>MenuIdiomas</item>
/// <item>Estadisticas</item>
/// <item>Creditos</item>
/// <item>MenuAyuda</item>
/// <item>AyudaObjetivo</item>
/// <item>AyudaControles</item>
/// </list>
/// </remarks>
public class MenuInicio : MonoBehaviour
{
    /// <summary>
    /// Objeto que contenga el menú de inicio
    /// </summary>
    GameObject menuInicio;
    /// <summary>
    /// Objeto que contenga el menú de opciones
    /// </summary>
    GameObject menuOpciones;
    /// <summary>
    /// Objeto que contenga el menú de idiomas
    /// </summary>
    GameObject menuIdiomas;
    /// <summary>
    /// Objeto que contenga las estadisticas
    /// </summary>
    GameObject menuEstadisticas;
    /// <summary>
    /// Objeto en el que se establece el texto de la estadistica de ataques realizados
    /// </summary>
    GameObject estadisticasAtaque;
    /// <summary>
    /// Objeto en el que se establece el texto de la estadistica de enemigos derrotados
    /// </summary>
    GameObject estadisticasEnemigo;
    /// <summary>
    /// Objeto en el que se establece el texto de la estadistica de maxima duración de una partida
    /// </summary>
    GameObject estadisticasTiempo;
    /// <summary>
    /// Objeto que contenga los creditos
    /// </summary>
    GameObject menuCreditos;
    /// <summary>
    /// Objeto que contenga la esplicacion de objetivo del juego
    /// </summary>
    GameObject menuAyudaObjetivo;
    /// <summary>
    /// Objeto que contenga la ayuda de los controles
    /// </summary>
    GameObject menuAyudaControles;
    /// <summary>
    /// Objeto que contenga el menú de ayuda
    /// </summary>
    GameObject menuAyuda;
    /// <summary>
    /// Objeto que contenga el menú de selección de mapa.
    /// </summary>
    GameObject menuSeleccionarMapa;
    /// <summary>
    /// Estadísticas guardadas actualmente
    /// </summary>
    RegistroEstadisticas estadisticas;
    /// <summary>
    ///Función que se llama en cuanto el elemento que posee esta clase esta habilitado por primera vez antes de update.
    ///En ella se inicializan la variables en función a los elementos en la escena 
    /// </summary>
    void Start()
    {
        //GuardarCargarConf.BorrarEstadisticas();
        actualizarEstadisticas();
        menuInicio = GameObject.Find("MenuPrincipal");
        menuOpciones = GameObject.Find("MenuOpciones");
        menuIdiomas = GameObject.Find("MenuIdiomas");
        menuEstadisticas = GameObject.Find("Estadisticas");
        menuCreditos = GameObject.Find("Creditos");
        menuAyuda = GameObject.Find("MenuAyuda");
        menuAyudaObjetivo = GameObject.Find("AyudaObjetivo");
        menuAyudaControles = GameObject.Find("AyudaControles");
        menuSeleccionarMapa = GameObject.Find("MenuSeleccionMapa");
        List<Dropdown.OptionData> listsMapa = new List<Dropdown.OptionData>();
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] info = dir.GetFiles("*.map");
        if (info.Length <= 0)
        {
            GameObject.Find("BtJugarCustom").GetComponent<Button>().interactable = false;
        }
        else
        {
            foreach (FileInfo fileInfo in info)
            {
                listsMapa.Add(new Dropdown.OptionData(fileInfo.Name.Substring(0, fileInfo.Name.Length - 4)));
            }
            GameObject.Find("DropdownMapas").GetComponent<Dropdown>().options = listsMapa;
            GameObject.Find("DropdownMapas").GetComponent<Dropdown>().value = 0;
        }
        menuSeleccionarMapa.SetActive(false);
        menuAyuda.SetActive(false);
        menuAyudaControles.SetActive(false);
        menuAyudaObjetivo.SetActive(false);
        menuEstadisticas.SetActive(false);
        menuOpciones.SetActive(false);
        menuIdiomas.SetActive(false);
        menuCreditos.SetActive(false);
        
    }

    /// <summary>
    /// Función que se llama cada frame mientras que el elemento que posee esta clase esta habilitado. 
    /// </summary>
    void Update()
    {
        if(Palabras.IdiomaActual==-1 && !menuIdiomas.activeSelf)
        {
            abrirIdiomas();
        }
    }
    /// <summary>
    /// Función que carga la scena del juego.
    /// </summary>
    public void iniciarJuego()
    {
        SceneManager.LoadScene(1);
    }
    /// <summary>
    /// Función que muestra el menú de opciones y oculta los demas.
    /// </summary>
    public void abrirOpciones()
    {
        menuAyuda.SetActive(false);
        menuAyudaControles.SetActive(false);
        menuAyudaObjetivo.SetActive(false);
        menuInicio.SetActive(false);
        menuIdiomas.SetActive(false);
        menuEstadisticas.SetActive(false);
        menuCreditos.SetActive(false);
        menuOpciones.SetActive(true);
    }
    /// <summary>
    /// Función que muestra el menú de pricipal y oculta los demas.
    /// </summary>
    public void VolverMenuPrincipal()
    {
        menuSeleccionarMapa.SetActive(false);
        menuAyuda.SetActive(false);
        menuAyudaControles.SetActive(false);
        menuAyudaObjetivo.SetActive(false);
        menuEstadisticas.SetActive(false);
        menuOpciones.SetActive(false);
        menuIdiomas.SetActive(false);
        menuCreditos.SetActive(false);
        menuInicio.SetActive(true);
    }
    /// <summary>
    /// Función que cierra la aplicación.
    /// </summary>
    public void salir()
    {
        Application.Quit();
    }
    /// <summary>
    /// Función que muestra el menú de idiomas y oculta los demas.
    /// </summary>
    public void abrirIdiomas()
    {
        menuAyuda.SetActive(false);
        menuAyudaControles.SetActive(false);
        menuIdiomas.SetActive(true);
        menuAyudaObjetivo.SetActive(false);
        menuInicio.SetActive(false);
        menuOpciones.SetActive(false);
        menuCreditos.SetActive(false);
        menuEstadisticas.SetActive(false);
    }
    /// <summary>
    /// Función que muestra el menú de estadísticas y oculta los demas.
    /// </summary>
    public void abrirEstadisticas()
    {
        menuAyudaControles.SetActive(false);
        menuIdiomas.SetActive(false);
        menuInicio.SetActive(false);
        menuOpciones.SetActive(false);
        menuCreditos.SetActive(false);
        menuEstadisticas.SetActive(true);
    }
    /// <summary>
    /// Función que muestra el menú de creditos y oculta los demas.
    /// </summary>
    public void abrirCreditos()
    {
        menuAyuda.SetActive(false);
        menuAyudaControles.SetActive(false);
        menuAyudaObjetivo.SetActive(false);
        menuIdiomas.SetActive(false);
        menuInicio.SetActive(false);
        menuOpciones.SetActive(false);
        menuEstadisticas.SetActive(false);
        menuCreditos.SetActive(true);
    }
    /// <summary>
    /// Función que muestra el menú de ayuda y oculta los demas.
    /// </summary>
    public void abrirAyuda()
    {
        menuAyudaControles.SetActive(false);
        menuIdiomas.SetActive(false);
        menuInicio.SetActive(false);
        menuOpciones.SetActive(false);
        menuEstadisticas.SetActive(false);
        menuCreditos.SetActive(false);
        menuAyudaObjetivo.SetActive(false);
        menuAyuda.SetActive(true);
    }
    /// <summary>
    /// Función que muestra el menú de ayudaObjetivo y oculta los demas.
    /// </summary>
    public void abrirAyudaObjetivo()
    {
        menuAyuda.SetActive(false);
        menuAyudaControles.SetActive(false);
        menuIdiomas.SetActive(false);
        menuInicio.SetActive(false);
        menuOpciones.SetActive(false);
        menuEstadisticas.SetActive(false);
        menuCreditos.SetActive(false);
        menuAyudaObjetivo.SetActive(true);
    }
    /// <summary>
    /// Función que muestra el menú de ayudaControles y oculta los demas.
    /// </summary>
    public void abrirAyudaControles()
    {
        menuAyuda.SetActive(false);
        menuIdiomas.SetActive(false);
        menuInicio.SetActive(false);
        menuOpciones.SetActive(false);
        menuEstadisticas.SetActive(false);
        menuCreditos.SetActive(false);
        menuAyudaObjetivo.SetActive(false);
        menuAyudaControles.SetActive(true);
    }
    /// <summary>
    /// Función que muestra el menú de selección de mapa.
    /// </summary>
    public void abrirSeleccionMapa()
    {
        menuSeleccionarMapa.SetActive(true);
        menuAyuda.SetActive(false);
        menuAyudaControles.SetActive(false);
        menuAyudaObjetivo.SetActive(false);
        menuEstadisticas.SetActive(false);
        menuOpciones.SetActive(false);
        menuIdiomas.SetActive(false);
        menuCreditos.SetActive(false);
        menuInicio.SetActive(false);
    }
    /// <summary>
    /// Función que muestra el menú de estadísticas y oculta los demas.
    /// </summary>
    public void actualizarEstadisticas()
    {
        estadisticas = GuardarCargarConf.cargarEstadiscas();
        if (estadisticas == null)
        {
            estadisticas = new RegistroEstadisticas();
        }
        estadisticasAtaque = GameObject.Find("nAtaques");
        estadisticasAtaque.GetComponent<Text>().text = ""+estadisticas.AtaquesRealizados;
        estadisticasEnemigo = GameObject.Find("nEnemigos");
        estadisticasEnemigo.GetComponent<Text>().text = "" + estadisticas.EnemigosDerrotados;
        estadisticasTiempo = GameObject.Find("nTiempo");
        estadisticasTiempo.GetComponent<Text>().text =estadisticas.tiempoFormateado();
        //Debug.Log("Ataques" + estadisticas.AtaquesRealizados);
        //Debug.Log("Enemigos" + estadisticas.EnemigosDerrotados);
        //Debug.Log("Tiempo: " + estadisticas.tiempoFormateado());
    }
    /// <summary>
    /// Función que carga la escena del mapa personalizado con el mapa seleccionado.
    /// </summary>
    public void iniciarJuegoCustom()
    {
        GenerarMapa.mapaActualPartida = GameObject.Find("DropdownMapas").GetComponent<Dropdown>().options[GameObject.Find("DropdownMapas").GetComponent<Dropdown>().value].text;
        SceneManager.LoadScene(3);
    }
    /// <summary>
    /// Función que carga la scena del editor.
    /// </summary>
    public void abrirEditor()
    {
        SceneManager.LoadScene(2);
    }
}
