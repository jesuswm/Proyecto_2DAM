using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
/// <summary>
/// Enumerado que contiene todas las id de los tiles diponibles en el editor
/// </summary>
[System.Serializable]
public enum eTiles
{
    Cesped, Cesped_CurvaNO, Cesped_CurvaNE, Cesped_CurvaSO, Cesped_CurvaSE, Agua, Agua_BordeO, Agua_BordeN, Agua_BordeE, Agua_BordeS, Agua_EsquinaNO, Agua_EsquinaNE, Agua_EsquinaSO, Agua_EsquinaSE
}
/// <summary>
/// Clase estática que asocia el enumerado <see cref="eTiles"/> con un tile de los recursos del proyecto
/// </summary>
public static class Tiles 
{
    /// <summary>
    /// Función que nos devuelve el tile asociado al parametro
    /// </summary>
    /// <param name="tiles">Tile que queremos obtener</param>
    /// <returns>Devuelve el tile asociado al enumerado pasado como parámetro</returns>
    public static Tile obtenerTile(eTiles tiles) {
        return Resources.Load<Tile>(tiles.ToString());
    }
}
