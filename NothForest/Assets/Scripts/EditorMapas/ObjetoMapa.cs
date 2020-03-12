using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum eTipo
{
    TerrenoTras,TerrenoNoTras,Enemigo,Jugador,Arbusto
}
[System.Serializable]
public class ObjetoMapa 
{
    private eTipo tipo;
    private int id;
    private int x;
    private int y;
    private int z;

    public ObjetoMapa(int id, eTipo tipo, int x, int y, int z)
    {
        Id = id;
        Tipo = tipo;
        X = x;
        Y = y;
        Z = z;
    }

    public int Id { get => id; set => id = value; }
    public eTipo Tipo { get => tipo; set => tipo = value; }
    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    public int Z { get => z; set => z = value; }
}
