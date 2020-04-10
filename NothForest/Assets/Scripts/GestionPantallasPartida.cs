using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Clase que se encarga de la gestión de las pantallas del juego (Pausa, Pantalla de muerte y Pantalla de victoria) 
/// </summary>
/// <remarks>
/// Para el funcionamiento de esta clase es necesario que en la escena existan los elementos con los siguientes nombres:
/// <list type="bullet">
/// <item>Controles</item>
/// <item>MenuPausa</item>
/// <item>MenuMuerte</item>
/// <item>MenuVictoria</item>
/// <item>Jugador</item>
/// </list>
/// </remarks>
public class GestionPantallasPartida : MonoBehaviour
{
    /// <summary>
    /// Objeto que contenga los controles
    /// </summary>
    GameObject controles;
    /// <summary>
    /// Objeto que contiene el menú de pausa
    /// </summary>
    GameObject menuPausa;
    /// <summary>
    /// Objeto que contiene el menú de muerte
    /// </summary>
    GameObject menuMuerte;
    /// <summary>
    /// Objeto que contenga la clase <see cref="Jugador"/> en la escena
    /// </summary>
    GameObject objjugador;
    /// <summary>
    /// Objeto que contiene el menú de victoria
    /// </summary>
    GameObject menuVictoria;
    /// <summary>
    /// Booleana que indica si el juego se ha terminado 
    /// </summary>
    bool fin = false;
    /// <summary>
    /// Devuelve o establece el valor de la variable fin
    /// </summary>
    public bool Fin
    {
        set
        {
            fin = value;
        }
        get
        {
            return fin;
        }
    }
    /// <summary>
    /// Script <see cref="Jugador"/> contenido en <see cref="objjugador"/>
    /// </summary>
    Jugador jugador;
    /// <summary>
    /// Booleana que indica si el juego se encuentra en pausa
    /// </summary>
    bool pausa;
    /// <summary>
    /// Booleana que indica si es necesario la realización de las comprobaciones en el update
    /// </summary>
    bool realizarUpdate =true;
    /// <summary>
    /// Devuelve el valor de la variable pausa
    /// </summary>
    public bool Pausa
    {
        get
        {
            return pausa;
        }
    }
    /// <summary>
    ///Función que se llama en cuanto el elemento que posee esta clase está habilitado por primera vez antes de update
    ///En ella se inicializan las variables.
    /// </summary>
    void Start()
    {
        controles=GameObject.Find("Controles");
        menuPausa=GameObject.Find("MenuPausa");
        menuMuerte = GameObject.Find("MenuMuerte");
        menuVictoria = GameObject.Find("MenuVictoria");
        objjugador = GameObject.Find("Jugador");
        try { 
            jugador = objjugador.GetComponent<Jugador>();
        }
        catch (NullReferenceException)
        {
            Debug.Log("Script GestionPantallasPartida: No se encontro el componente \"Jugador\" de objjugador");
        }
        menuPausa.gameObject.SetActive(false);
        menuMuerte.gameObject.SetActive(false);
        menuVictoria.gameObject.SetActive(false);
        pausa = false;
    }

    // Update is called once per frame
    /// <summary>
    /// Función que se llama cada frame mientras que el elemento que posee esta clase esté habilitado. 
    /// </summary>
    void Update()
    {
        if (realizarUpdate)
        {
            try
            {
                if (fin && !menuVictoria.activeSelf)
                {
                    abrirMenuVictoria();
                }
                else
                {
                    if (jugador.Vida <= 0 && !menuMuerte.activeSelf)
                    {
                        abrirMenuMuerte();
                    }
                    if (Input.GetKeyDown(KeyCode.Escape) && !menuMuerte.activeSelf)
                    {
                        if (!pausa)
                        {
                            pausar();
                        }
                        else
                        {
                            reiniciar();
                        }
                    }
                }
            }
            catch (NullReferenceException)
            {
                realizarUpdate = false;
                Debug.Log("Script GestionPantallasPartida: NullReferenceException en el update se procede a desactivar el update");
            }
        }
    }
    /// <summary>
    /// Función que para el juego activa el menú de pausa y desactiva los controles de juego
    /// </summary>
    public void pausar()
    {
        Time.timeScale = 0f;
        menuPausa.SetActive(true);
        controles.SetActive(false);
        pausa = true;
        
    }
    /// <summary>
    /// Función que restaura el juego en el estado anterior a la pausa y activa los controles de juego
    /// </summary>
    public void reiniciar()
    {
        try
        {
            menuPausa.SetActive(false);
            controles.SetActive(true);
        }
        catch (NullReferenceException)
        {

        }
        Time.timeScale = 1f;
        pausa = false;
    }
    /// <summary>
    /// Función que restaura el juego y carga el menú
    /// </summary>
    public void menuPrincipal()
    {
        reiniciar();
        SceneManager.LoadScene(0);
    }
    /// <summary>
    /// Función que para el juego activa el menú de muerte y desactiva los controles de juego
    /// </summary>
    public void abrirMenuMuerte()
    {
        Time.timeScale = 0f;
        menuPausa.SetActive(false);
        controles.SetActive(false);
        menuMuerte.SetActive(true);
    }
    /// <summary>
    /// Función que para el juego activa el menú de victoria y desactiva los controles de juego
    /// </summary>
    public void abrirMenuVictoria()
    {
        Time.timeScale = 0f;
        menuPausa.SetActive(false);
        controles.SetActive(false);
        menuMuerte.SetActive(false);
        menuVictoria.SetActive(true);
    }
    /// <summary>
    /// Función que restaura el juego y recarga la escena actual
    /// </summary>
    public void reintentar()
    {
        Time.timeScale = 1f;
        //menuMuerte.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
