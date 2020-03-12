using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Clase que se encarga de almacenar las estadísticas de la partida
/// </summary>
public class Estadisticas : MonoBehaviour
{
    /// <summary>
    /// Número de enemigos derrotados
    /// </summary>
    public int enemigosDerrotados=0;
    /// <summary>
    /// Número de ataques realizados
    /// </summary>
    public int ataquesRealizados=0;
    /// <summary>
    /// Tiempo en el que se inicio la partida actual
    /// </summary>
    public float inicioDePartida=0;
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase esta habilitado por primera vez antes de update.
    /// </summary>
    void Start()
    {
         inicioDePartida = Time.time;
    }

    /// <summary>
    /// Función que se llama cada frame mientras que el elemento que posee esta clase esta habilitado
    /// </summary>
    void Update()
    {
        //TimeSpan ts = TimeSpan.FromSeconds(Time.time-inicioDePartida);
        //Debug.Log(string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds));
    }
    /// <summary>
    /// Función que intenta guardar las estadísticas actuales y se guardan en caso de que sean mejores que las actuales (<see cref="GuardarCargarConf.GuardarEstadisticas(Estadisticas)"/>)
    /// </summary>
    public void actualizarEstadisticas()
    {
        GuardarCargarConf.GuardarEstadisticas(this);
    }
    /// <summary>
    /// Función que se llama cuando el objeto se va ha destruir
    /// </summary>
    public void OnDestroy()
    {
        //Debug.Log("Destrullendo");
        actualizarEstadisticas();
    }
}
