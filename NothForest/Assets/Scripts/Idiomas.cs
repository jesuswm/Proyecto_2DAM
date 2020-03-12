using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using UnityEngine;
/// <summary>
/// Enumerado que contiene todos los idiomas disponibles en el juego
/// </summary>
public enum Idiomas 
{
    /// <summary>
    /// Idioma español
    /// </summary>
    Español,
    /// <summary>
    /// Idioma ingles
    /// </summary>
    Ingles
}
/// <summary>
/// Enumerado que contiene todas las id de los string del juego
/// </summary>
public enum palabras
{
    id, Menu,Jugar, Opciones, Salir, Vibracion, Sonido, VolverMenu, Continuar, CambiarIdioma, ReiniciarConf, Pausa, HasMuerto, Reintentar, Creditos, Estadisticas, Ayuda, EstadisticasAtaques, EstadisticasEnemigos, EstadisticasMaxTiempo, HasGanado, VolverJugar, Volver, ObjetivoTitulo, ObjetivoDescripcion, ControlesTitulo, ControlesMovimiento, ControlesAtaque, ControlesPausa, ControlesIndicadorVida, MenuAyudaTitulo, Graficos, Seleccionidioma, ingles, español
}
/// <summary>
/// Clase estática que se encarga de generar un diccionario para cada idioma en función al contenido de Idiomas.xml que se encuenta en los recursos del proyecto
/// </summary>
public static class Palabras
{
    /// <summary>
    /// Índice que se usa de referencia para identificar de que idioma se obtienen las palabras
    /// </summary>
    static int idiomaActual=0;
    /// <summary>
    /// Función que devuelve el valor del idioma actual
    /// </summary>
    public static int IdiomaActual
    {
        get
        {
            return idiomaActual;
        }
    }
    /// <summary>
    /// Lista en la que se almacenan los Dictionary de cada idioma
    /// </summary>
    private static List<Dictionary<string, string>> listaIdiomas = leerXml();
    /// <summary>
    /// Función que lee el archivo Idiomas.xml de los recursos del proyecto
    /// </summary>
    /// <returns>Devuelve una lista con un dictionary para cada uno de los idiomas definidos en el xml</returns>
    public static List<Dictionary<string, string>> leerXml()
    {
        List<Dictionary<string, string>> listaIdiomas = new List<Dictionary<string, string>>();
        XmlDocument xmlDoc = new XmlDocument();
        TextAsset text = Resources.Load<TextAsset>("Idiomas");
        xmlDoc.LoadXml(text.text);
        XmlNodeList idiomas = xmlDoc.GetElementsByTagName("Idioma");
        foreach (XmlNode palabrasIdioma in idiomas)
        {
            XmlNodeList palabra = palabrasIdioma.ChildNodes;
            Dictionary<string, string> listaPalabras = new Dictionary<string, string>();
            foreach (XmlNode valor in palabra)
            {
                if (listaPalabras.ContainsKey(valor.Name) == false)
                {
                    listaPalabras.Add(valor.Name, valor.InnerText);
                }
            }
            listaIdiomas.Add(listaPalabras);
        }
        return listaIdiomas;
    }
    /// <summary>
    /// Función que establece el valor del idioma actual en función de un enumerado en caso del que el enumerado no tenga una equivalencia el idioma actual se establecerá en -1
    /// </summary>
    /// <param name="idioma">Enumerado del idioma al que deseamos cambiar</param>
    public static void setIdioma(Idiomas idioma)
    {
        idiomaActual = (int)idioma;
    }
    /// <summary>
    /// Función que establece el valor del idioma actual en función de su índice en caso del que el índice no exista el valor de idioma actual se establecerá en -1
    /// </summary>
    /// <param name="indice">Indice del idioma al que deseamos cambiar</param>
    public static void setIdioma(int indice)
    {
        if (indice >= 0 && indice < listaIdiomas.Count)
        {
            idiomaActual = indice;
        }
        else
        {
            idiomaActual = -1;
            //Debug.Log("El indice introduciducido no es valido");
        }
    }
    /// <summary>
    /// Función que obtiene la palabra asociada a un id pasado como parámetro en función al idioma actual. 
    /// </summary>
    /// <param name="nombre">Id de la palabra que buscamos</param>
    /// <returns>palabra asociada al id pasado como parámetro en el idioma actual si no existe devuelve null</returns>
    public static string getPalabra(string nombre)
    {
        string palabra;
        if (idiomaActual != -1)
        {
            listaIdiomas[idiomaActual].TryGetValue(nombre, out palabra);
        }
        else
        {
            listaIdiomas[0].TryGetValue(nombre, out palabra);
        }
        //Debug.Log(palabra);
        return palabra;
    }
    /// <summary>
    /// Función que obtiene la palabra asociada a un enumerado pasado como parámetro en función al idioma actual. 
    /// </summary>
    /// <param name="nombre">Enumerado asociado con la palabra que buscamos</param>
    /// <returns>palabra asociada al enumerado pasado como parámetro en el idioma actual si no existe devuelve null</returns>
    public static string getPalabra(palabras nombre)
    {
        string busqueda = nombre.ToString();
        //string palabra;
        //if (idiomaActual != -1)
        //{
        //    listaIdiomas[idiomaActual].TryGetValue(nombre.ToString(), out palabra);
        //}
        //else
        //{
        //    listaIdiomas[0].TryGetValue(nombre.ToString(), out palabra);
        //}
        //Debug.Log(palabra);
        return getPalabra(busqueda);
    }
    /// <summary>
    /// Función que nos devuelve el índice de un idioma pasado como parámetro
    /// </summary>
    /// <param name="idioma">nombre del idioma del que deseamos obtener su índice</param>
    /// <returns>Devuelve el índice del idioma en caso de que no exista devuelve -1</returns>
    public static int getIndiceDeUnIdioma(string idioma)
    {
        int indice = -1;
        for (int i=0;i<listaIdiomas.Count;i++)
        {
            if (listaIdiomas[i]["id"].ToLower() == idioma.ToLower())
            {
                indice = i;
                break;
            }
        }
        //Debug.Log(indice);
        return indice;
    }
    
}

