using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum eEnemigo 
{
    Moco, Tronquito, Orco
}
public static class Enemigos
{
    public static GameObject obtenerEnemigo(eEnemigo enemigo)
    {
        return Resources.Load<GameObject>(enemigo.ToString());
    }
}
