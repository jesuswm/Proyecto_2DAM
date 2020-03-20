using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase que se genera a partir de una Lista de <see cref="ObjetoMapa"/> y los clasifica en funcion de la subclase de objeto
/// </summary>
[System.Serializable]
public class Mapa 
{
    /// <summary>
    /// Lista de objetos <see cref="TileMapa"/> con la propiedad <see cref="TileMapa.traspasable"/ true>
    /// </summary>
    List<TileMapa> terrenoTraspasable=new List<TileMapa>();
    /// <summary>
    /// Lista de objetos <see cref="TileMapa"/> con la propiedad <see cref="TileMapa.traspasable"/ false>
    /// </summary>
    List<TileMapa> terrenoNoTraspasable = new List<TileMapa>();
    /// <summary>
    /// Lista de objetos <see cref="EnemigoMapa"/> 
    /// </summary>
    List<EnemigoMapa> enemigo = new List<EnemigoMapa>();
    /// <summary>
    /// Lista de objetos <see cref="JugadorMapa"/> 
    /// </summary>
    List<JugadorMapa> jugador = new List<JugadorMapa>();
    /// <summary>
    /// Lista de objetos <see cref="ArbustoMapa"/> 
    /// </summary>
    List<ArbustoMapa> arbusto = new List<ArbustoMapa>();
    /// <summary>
    /// Lista de objetos <see cref="ObstaculosMapa"/> 
    /// </summary>
    List<ObstaculosMapa> obstaculos = new List<ObstaculosMapa>();
    public Mapa(List<ObjetoMapa> objetos)
    {
        foreach (ObjetoMapa objeto in objetos)
        {
            if (objeto is TileMapa) {
                TerrenoTraspasable.Add((TileMapa)objeto);
                TerrenoNoTraspasable.Add((TileMapa)objeto);
            }
            if (objeto is JugadorMapa)
            {
                Jugador.Add((JugadorMapa)objeto);
            }
            if (objeto is EnemigoMapa)
            {
                Enemigo.Add((EnemigoMapa)objeto);
            }
            if (objeto is ArbustoMapa)
            {
                Arbusto.Add((ArbustoMapa)objeto);
            }
            if (objeto is ObstaculosMapa)
            {
                Obstaculos.Add((ObstaculosMapa)objeto);
            }

        }
    }
    /// <summary>
    /// Establece o devuelve el valor de la variable <see cref="terrenoTraspasable"/>
    /// </summary>
    public List<TileMapa> TerrenoTraspasable { get => terrenoTraspasable; set => terrenoTraspasable = value; }
    /// <summary>
    /// Establece o devuelve el valor de la variable <see cref="terrenoNoTraspasable"/>
    /// </summary>
    public List<TileMapa> TerrenoNoTraspasable { get => terrenoNoTraspasable; set => terrenoNoTraspasable = value; }
    /// <summary>
    /// Establece o devuelve el valor de la variable <see cref="enemigo"/>
    /// </summary>
    public List<EnemigoMapa> Enemigo { get => enemigo; set => enemigo = value; }
    /// <summary>
    /// Establece o devuelve el valor de la variable <see cref="jugador"/>
    /// </summary>
    public List<JugadorMapa> Jugador { get => jugador; set => jugador = value; }
    /// <summary>
    /// Establece o devuelve el valor de la variable <see cref="arbusto"/>
    /// </summary>
    public List<ArbustoMapa> Arbusto { get => arbusto; set => arbusto = value; }
    /// <summary>
    /// Establece o devuelve el valor de la variable <see cref="obstaculos"/>
    /// </summary>
    public List<ObstaculosMapa> Obstaculos { get => obstaculos; set => obstaculos = value; }
}
