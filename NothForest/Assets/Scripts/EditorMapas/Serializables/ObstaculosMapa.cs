using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase serializable que contiene las variables que le corresponden obstáculo del mapa
/// </summary>
[System.Serializable]
public class ObstaculosMapa : ObjetoMapa
{
    /// <summary>
    /// Especifica el tipo de obstaculo
    /// </summary>
    eObstaculos tipoObstaculo;
    public ObstaculosMapa(int x, int y, int z,eObstaculos tipoObstaculo) : base(x, y, z)
    {
        TipoObstaculo = tipoObstaculo;
    }
    /// <summary>
    /// Establece o devuelve el valor de la variable <see cref="tipoObstaculo"/>
    /// </summary>
    public eObstaculos TipoObstaculo { get => tipoObstaculo; set => tipoObstaculo = value; }
}
