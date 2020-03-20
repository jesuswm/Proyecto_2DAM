using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase serializable que contiene las variables comunes de cualquier objeto del mapa (Coordenadas)
/// </summary>
[System.Serializable]
public class ObjetoMapa 
{
    /// <summary>
    /// Cordenadas del objeto del mapa en el eje x
    /// </summary>
    private int x;
    /// <summary>
    /// Cordenadas del objeto del mapa en el eje y
    /// </summary>
    private int y;
    /// <summary>
    /// Cordenadas del objeto del mapa en el eje y
    /// </summary>
    private int z;

    public ObjetoMapa(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    /// <summary>
    /// Establece o devuelve el valor de la variable <see cref="x"/>
    /// </summary>
    public int X { get => x; set => x = value; }
    /// <summary>
    /// Establece o devuelve el valor de la variable <see cref="y"/>
    /// </summary>
    public int Y { get => y; set => y = value; }
    /// <summary>
    /// Establece o devuelve el valor de la variable <see cref="z"/>
    /// </summary>
    public int Z { get => z; set => z = value; }
}
