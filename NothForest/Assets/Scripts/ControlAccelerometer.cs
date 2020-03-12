using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase que controla el movimiento horizontal mediante el acelerómetro del dispositivo
/// </summary>
/// <remarks>
/// Para el funcionamiento de esta clase es necesario que el elemento también tenga asociado:
/// <list type="bullet">
/// <item><see cref="Rigidbody2D"/></item>
/// <item><see cref="BoxCollider2D"/></item>
/// </list>
/// </remarks>
public class ControlAccelerometer : MonoBehaviour
{
    // Start is called before the first frame update
    /// <summary>
    /// Objeto que deseamos que se desplace con el acelerómetro 
    /// </summary>
    public GameObject obj;
    /// <summary>
    /// Velocidad a la que se desplaza el objeto
    /// </summary>
    float velocidad=100f;
    /// <summary>
    /// Vector donde se establece el movimiento
    /// </summary>
    Vector2 vector;
    /// <summary>
    /// Rigidbody2D asociado al objeto(<see cref="obj"/>) que deseamos desplazar con el Accelerometer 
    /// </summary>
    Rigidbody2D rbd;
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase esta habilitado por primera vez antes de update.
    /// En ella se inicializan la variables.
    /// </summary>
    void Start()
    {
        rbd = obj.GetComponent<Rigidbody2D>();
        obj.GetComponent<BoxCollider2D>().size= new Vector2(obj.GetComponent<RectTransform>().rect.width, obj.GetComponent<RectTransform>().rect.height); 
    }

    /// <summary>
    /// Función que se llama cada frame mientras que el elemento que posee esta clase esta habilitado. 
    /// </summary>
    void Update()
    {
        //Debug.Log(Input.acceleration.x);
        if (Mathf.Abs(Input.acceleration.x) > 0.2)
        {
            vector.x = Input.acceleration.x*velocidad;
            vector.y = 0;
        }
        else
        {
            vector.x = 0;
            vector.y = 0;
        }
        //Debug.Log("acceleration x:"+Input.acceleration.x);
    }
    /// <summary>
    /// Función que se llama cada cierto numero de frames mientras que el elemento que posee esta clase esta habilitado. 
    /// </summary>
    void FixedUpdate()
    {
            rbd.MovePosition(rbd.position + vector * velocidad * Time.fixedDeltaTime);
    }
}
