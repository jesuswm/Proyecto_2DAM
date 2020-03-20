using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase serializable que contiene las variables que le corresponden al punto de aparición de un enemigo en el mapa
/// </summary>
[System.Serializable]
public class EnemigoMapa : ObjetoMapa
{
    /// <summary>
    /// Especifica el tipo de enemigo que aparecerá en ese punto del mapa
    /// </summary>
    eEnemigo tipoEnemigo;

    public EnemigoMapa(int x, int y, int z, eEnemigo tipoEnemigo) : base(x, y, z)
    {
        TipoEnemigo = tipoEnemigo;
    }
    /// <summary>
    /// Establece o devuelve el valor de la variable <see cref="tipoEnemigo"/>
    /// </summary>
    public eEnemigo TipoEnemigo { get => tipoEnemigo; set => tipoEnemigo = value; }
}
