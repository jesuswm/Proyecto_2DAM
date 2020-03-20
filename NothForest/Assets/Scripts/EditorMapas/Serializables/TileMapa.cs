using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase serializable que contiene las variables que le corresponden a un tile del mapa
/// </summary>
[System.Serializable]
public class TileMapa : ObjetoMapa
{
    /// <summary>
    /// Booleana que establece si el tile es traspaseble o no por el jugador
    /// </summary>
    bool traspasable;
    /// <summary>
    /// Especifica el tile
    /// </summary>
    eTiles tile;

    public TileMapa(int x, int y, int z,eTiles tile,bool traspasable) : base(x, y, z)
    {
        this.Traspasable = traspasable;
        this.Tile = tile;
    }
    /// <summary>
    /// Establece o devuelve el valor de la variable <see cref="traspasable"/>
    /// </summary>
    public bool Traspasable { get => traspasable; set => traspasable = value; }
    /// <summary>
    /// Establece o devuelve el valor de la variable <see cref="tile"/>
    /// </summary>
    public eTiles Tile { get => tile; set => tile = value; }
}
