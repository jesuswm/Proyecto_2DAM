using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public enum eTiles
{
    Cesped, Agua
}
public static class Tiles 
{
    public static Tile obtenerTile(eTiles tiles) {
        return Resources.Load<Tile>(tiles.ToString());
    }
}
