using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase serializable que contiene las variables que le corresponden al punto de un arbusto en el mapa
/// </summary>
[System.Serializable]
public class ArbustoMapa : ObjetoMapa
{
    public ArbustoMapa(int x, int y, int z) : base(x, y, z)
    {
    }
}
