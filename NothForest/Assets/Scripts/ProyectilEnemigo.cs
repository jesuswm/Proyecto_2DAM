using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase que gestiona los proyectiles que lanzan los enemigos
/// </summary>
public class ProyectilEnemigo : MonoBehaviour
{
    // Start is called before the first frame update
    /// <summary>
    /// Daño que realiza el proyectil al jugador
    /// </summary>
    float daño = 1f;
    /// <summary>
    /// Dirección hacia la que se desplaza el proyectil
    /// </summary>
    public Direccion dir = Direccion.undefine;
    /// <summary>
    /// El objeto que hizo aparecer a este objeto
    /// </summary>
    GameObject invocador =null;
    /// <summary>
    /// Devuelve el valor de la variable <see cref="daño"/>
    /// </summary>
    public float Daño
    {
        get
        {
            return daño;
        }
    }
    /// <summary>
    /// Función que desplaza al objeto en una dirección en función del parametro <see cref="Direccion"/>
    /// </summary>
    void mover()
    {
        switch (dir)
        {
            case Direccion.derecha:
                transform.Translate(Vector2.right * Time.deltaTime);
                break;
            case Direccion.izquierda:
                transform.Translate(Vector2.left * Time.deltaTime);
                break;
            case Direccion.arriba:
                transform.Translate(Vector2.up * Time.deltaTime);
                break;
            case Direccion.abajo:
                transform.Translate(Vector2.down  * Time.deltaTime);
                break;
        }
    }
    /// <summary>
    /// Función que se llama cada cierto número de frames mientras que el elemento que posee esta clase esté habilitada. 
    /// </summary>
    void FixedUpdate()
    {
        mover();
    }
    /// <summary>
    /// Función que permite estableces todos los parámetros del objeto
    /// </summary>
    /// <param name="daño">Establece el valor de daño del objeto</param>
    /// <param name="dir">Establece el valor de dir del objeto</param>
    /// <param name="invocador">Establece el valor del invocador del objeto</param>
    public void establecerValores(float daño, Direccion dir,GameObject invocador)
    {
        this.daño = daño;
        this.dir = dir;
        this.invocador = invocador;
    }
    /// <summary>
    /// Función que se lanza cuando el objeto entra colisión
    /// El objeto se destruye si la colisión se produce con algo que no sea su propio invocador
    /// </summary>
    /// <param name="collision">Objeto generado de por la colisión</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != invocador)
        {
            Destroy(this.gameObject);
        }
    }
}
