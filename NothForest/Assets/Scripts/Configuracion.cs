using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// Clase que contiene todas las funcionas que gestionan la configuración del juego (idioma, sonido, vibración)
/// </summary>
/// <remarks>
/// Para el funcionamiento de esta clase es necesario que en la escena existan los elementos con los siguientes nombres:
/// <list type="bullet">
/// <item>Main Camera con un <see cref="AudioSource"/></item>
/// <item>ToggleSonido con un <see cref="Toggle"/></item>
/// <item>ToggleVibracion con un <see cref="Toggle"/></item>
/// </list>
/// </remarks>
public class Configuracion : MonoBehaviour
{
    /// <summary>
    /// Booleana que indica si la opción de vibración esta activa en el juego
    /// </summary>
    public bool vibracion;
    /// <summary>
    /// Booleana que indica si la opción de sonido esta activa en el juego
    /// </summary>
    public bool sonido;
    /// <summary>
    /// Booleana que indica si el elemento ya se ha inicializado
    /// </summary>
    bool inicializado = false;
    /// <summary>
    /// Valor de la id del idioma actual del juego
    /// </summary>
    public int idioma=0;
    /// <summary>
    /// Elemento en la escena con el checkBox de sonido
    /// </summary>
    GameObject checkSonido;
    /// <summary>
    /// Elemento en la escena con el checkBox de vibración
    /// </summary>
    GameObject checkVibracion;
    /// <summary>
    /// Audio que se reproduce en el menú principal
    /// </summary>
    AudioSource musica;
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase esta habilitado por primera vez antes de update.
    /// En ella se inicializan las variables y se carga la configuración guardada.  
    /// </summary>
    void Start()
    {
        musica = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        RegistroConfiguracion conf = GuardarCargarConf.cargarConfiguracion();
        if (conf != null)
        {
            vibracion = conf.vibracion;
            sonido = conf.sonido;
            idioma = conf.idioma;
        }
        else
        {
            vibracion = true;
            sonido = true;
            idioma = -1;
        }
        Palabras.setIdioma(idioma);
        checkSonido = GameObject.Find("ToggleSonido");
        checkSonido.GetComponent<Toggle>().isOn=sonido;
        checkVibracion = GameObject.Find("ToggleVibracion");
        checkVibracion.GetComponent<Toggle>().isOn = vibracion;
        inicializado = true;
    }

    /// <summary>
    /// Función que se llama cada frame mientras que el elemento que posee esta clase esté habilitado. 
    /// </summary>
    void Update()
    {
        if (musica.enabled && !sonido)
        {
            musica.enabled = false;
        }
        else
        {
            if (!musica.enabled && sonido)
            {
                musica.enabled = true;
            }
        }
    }
    /// <summary>
    /// Función que  desactivando el parámetro sonido de la configuración del juego
    /// </summary>
    public void pulsarSonido()
    {
        if (inicializado)
        {
            Debug.Log("Pulsaste sonido");
            sonido = !sonido;
            GuardarCargarConf.GuardarConfiguracion(this);
        }
    }
    /// <summary>
    /// Función que  desactivando el parámetro vibración de la configuración del juego
    /// </summary>
    public void pulsarVibracion()
    {
        if (inicializado)
        {
            Debug.Log("Pulsaste vibracion");
            vibracion = !vibracion;
            GuardarCargarConf.GuardarConfiguracion(this);
        }
    }
    /// <summary>
    /// Función que cambia el idioma del juego a inglés
    /// </summary>
    public void idiomaIngles()
    {
        Palabras.setIdioma(Palabras.getIndiceDeUnIdioma("Ingles"));
        idioma = Palabras.IdiomaActual;
        GuardarCargarConf.GuardarConfiguracion(this);
    }
    /// <summary>
    /// Función que cambia el idioma del juego a español
    /// </summary>
    public void idiomaEspañol()
    {
        Palabras.setIdioma(Palabras.getIndiceDeUnIdioma("Español"));
        idioma = Palabras.IdiomaActual;
        GuardarCargarConf.GuardarConfiguracion(this);
    }
    /// <summary>
    /// Función que borra la configuración actual del juego y reinicia la escena
    /// </summary>
    public void reiniciarConfig()
    {
        GuardarCargarConf.BorrarConfiguracion();
        GuardarCargarConf.BorrarEstadisticas();
        SceneManager.LoadScene(0);
    }
}
