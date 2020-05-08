using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Clase que asigna la función del botón de ataque en la scena del mapa personalizado.
/// </summary>
public class BotonAtacarAutoGenerado : MonoBehaviour
{
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase esté habilitade por primera vez.
    /// </summary>
    void Start()
    {
        Button atacar = gameObject.GetComponent<Button>();
        try
        {
            Jugador jugador = GameObject.Find("Jugador").GetComponent<Jugador>();
            atacar.onClick.AddListener(delegate () { jugador.ataque(); });
        }
        catch (NullReferenceException)
        {
            Debug.Log("Script BotonAtacarAutoGenerado: No se encontro el componente \"Jugador\" deL GameObject Jugador");
        }
    }
}
