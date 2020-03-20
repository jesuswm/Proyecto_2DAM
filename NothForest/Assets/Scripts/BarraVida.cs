using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase que se encarga de gestionar la barra de vida de la interfaz
/// </summary>
public class BarraVida : MonoBehaviour
{
    /// <summary>
    /// Objeto que contenga la clase <see cref="Jugador"/> en la escena
    /// </summary>
    public GameObject jugador;
    /// <summary>
    /// Vida máxima del jugador
    /// </summary>
    float maxVida;
    /// <summary>
    /// Vida actual del jugador
    /// </summary>
    float vida;

    void Start()
    {
        if (jugador == null)
        {
            jugador = GameObject.Find("Jugador");
        }
    }

    /// <summary>
    /// Función que se llama cada frame mientras que el elemento que posee esta clase esté habilitado
    /// en ella se hacen las comprobaciones de la vida actual del jugador y se le asigna a la barra de vida la animación correspondiente
    /// a su estado. 
    /// </summary>
    void Update()
    {
        float maxVida = jugador.GetComponent<Jugador>().MaxVida;
        vida = jugador.GetComponent<Jugador>().Vida;
        GetComponent<Animator>().SetFloat("Vida", vida/maxVida);
    }
}
