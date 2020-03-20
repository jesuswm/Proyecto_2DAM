using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Enumerado que contiene todas las id de los enemigos diponibles en el editor
/// </summary>
[System.Serializable]
public enum eEnemigo 
{
    Moco, Tronquito, Orco
}
/// <summary>
/// Clase estática que asocia el enumerado <see cref="eEnemigo"/> con el GameObject correspondiente al enemigo.
/// </summary>
public static class Enemigos
{
    /// <summary>
    /// Función que nos devuelve el GameObject asociado al parametro
    /// </summary>
    /// <param name="enemigo">enemigo del que deseamos obtener su gameObject</param>
    /// <returns>El GameObject correspondiente al enemigo</returns>
    public static GameObject obtenerEnemigo(eEnemigo enemigo)
    {
        return Resources.Load<GameObject>(enemigo.ToString());
    }
}
