using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Enumerado que contiene todas las id de los enemigos diponibles en el editor
/// </summary>
[System.Serializable]
public enum eObstaculos
{
    Arbol
}
/// <summary>
/// Clase estática que asocia el enumerado <see cref="eObstaculos"/> con el GameObject correspondiente al Obstáculo.
/// </summary>
public static class Obstaculos 
{
    /// <summary>
    /// Función que nos devuelve el GameObject asociado al parametro
    /// </summary>
    /// <param name="obstaculo">Obstáculo del que deseamos obtener su gameObject</param>
    /// <returns></returns>
    public static GameObject obtenerObstaculo(eObstaculos obstaculo)
    {
        return Resources.Load<GameObject>(obstaculo.ToString());
    }
}
