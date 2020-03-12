using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Mapa 
{
    List<ObjetoMapa> terrenoTraspasable=new List<ObjetoMapa>();
    List<ObjetoMapa> terrenoNoTraspasable = new List<ObjetoMapa>();
    List<ObjetoMapa> enemigo = new List<ObjetoMapa>();
    List<ObjetoMapa> jugador = new List<ObjetoMapa>();
    List<ObjetoMapa> arbusto = new List<ObjetoMapa>();
    public Mapa(List<ObjetoMapa> objetos)
    {
        foreach (ObjetoMapa objeto in objetos)
        {
            switch (objeto.Tipo)
            {
                case eTipo.TerrenoTras:
                    TerrenoTraspasable.Add(objeto);
                    break;
                case eTipo.TerrenoNoTras:
                    TerrenoNoTraspasable.Add(objeto);
                    break;
                case eTipo.Jugador:
                    Jugador.Add(objeto);
                    break;
                case eTipo.Enemigo:
                    Enemigo.Add(objeto);
                    break;
                case eTipo.Arbusto:
                    Arbusto.Add(objeto);
                    break;
            }
        }
    }

    public List<ObjetoMapa> TerrenoTraspasable { get => terrenoTraspasable; set => terrenoTraspasable = value; }
    public List<ObjetoMapa> TerrenoNoTraspasable { get => terrenoNoTraspasable; set => terrenoNoTraspasable = value; }
    public List<ObjetoMapa> Enemigo { get => enemigo; set => enemigo = value; }
    public List<ObjetoMapa> Jugador { get => jugador; set => jugador = value; }
    public List<ObjetoMapa> Arbusto { get => arbusto; set => arbusto = value; }
}
