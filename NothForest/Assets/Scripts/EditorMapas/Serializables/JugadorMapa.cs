using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase serializable que contiene las variables que le corresponden al punto de inicio del jugador en el mapa
/// </summary>
[System.Serializable]
public class JugadorMapa : ObjetoMapa
{
    public JugadorMapa(int x, int y, int z) : base(x, y, z)
    {
    }
}
