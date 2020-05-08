using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Clase que gestiona el desplazamiento de la cámara en el editor
/// </summary>
public class MoverCamara : MonoBehaviour
{
    /// <summary>
    /// GameObject que contiene la camara
    /// </summary>
    public GameObject camera_GameObject;
    /// <summary>
    /// Primer punto donde pulsamos
    /// </summary>
    Vector2 puntoInicio;
    /// <summary>
    /// Posición segundo dedo
    /// </summary>
    Vector2 inicioArrastre;
    /// <summary>
    /// Posicion en la que termina el arrastre el segundo dedo
    /// </summary>
    Vector2 finArrastre;
    /// <summary>
    /// Posición del primer dedo
    /// </summary>
    Vector2 posicionDedo0;
    /// <summary>
    /// Distancia entre el dedo cero y uno para calcular el zoom
    /// </summary>
    float DistanciaEntreDedos;
    /// <summary>
    /// Booleana que indica si se está haciendo zoom
    /// </summary>
    bool zoom =false;
    /// <summary>
    /// Booleana que indica si se las funciones de zoom y desplazamiento pueden ser usadas
    /// </summary>
    public bool activo = true;
    /// <summary>
    /// GameObject que contiene el panel de herramientas del editor
    /// </summary>
    GameObject PanelHerramientas;
    /// <summary>
    /// Lista de las dicersas pantallas que se usan en el editor
    /// </summary>
    List <GameObject> menus=new List<GameObject>();
    /// <summary>
    /// El máximo zoom que se puede realizar la cámara
    /// </summary>
    float maxZom = 3.5f;
    /// <summary>
    /// El mínimo zoom que se puede realizar la cámara
    /// </summary>
    float minZom = 0.5f;
    /// <summary>
    /// Posición inicial
    /// </summary>
    Vector3 posicionInicio;
    /// <summary>
    /// Posición de origen
    /// </summary>
    Vector3 Origen;
    /// <summary>
    /// Velocidad de desplazamiento
    /// </summary>
    float Velocidad=1f;
    void Start()
    {
        PanelHerramientas = GameObject.Find("Panel");
        menus.Add(GameObject.Find("PantallaGuardar"));
        menus.Add(GameObject.Find("PantallaCargar"));
        menus.Add(GameObject.Find("PantallaBorrar"));
    }
    /// <summary>
    /// Función que se llama cada frame mientras que el elemento que posee esta clase esta habilitado.
    /// </summary>
    void Update()
    {
        foreach(GameObject menu in menus)
        {
            if (menu.activeSelf)
            {
                activo = false;
            }
        }
        if (activo)
        {
            if (Application.platform != RuntimePlatform.Android)
            {
                if (!PanelHerramientas.activeSelf || GameObject.Find("TogleHerramientas").GetComponent<SeleccionDeHerramienta>().herramientaActual == eHerramientas.mover)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        posicionInicio = transform.position;
                        Origen = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                    }

                    if (Input.GetMouseButton(0))
                    {
                        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - Origen;
                        camera_GameObject.transform.position = posicionInicio + -pos * Velocidad;
                    }
                    if (Input.GetAxis("Mouse ScrollWheel") > 0f && camera_GameObject.GetComponent<Camera>().orthographicSize > minZom)
                    {
                        camera_GameObject.GetComponent<Camera>().orthographicSize = camera_GameObject.GetComponent<Camera>().orthographicSize - 0.1f;
                    }
                    else if (Input.GetAxis("Mouse ScrollWheel") < 0f && camera_GameObject.GetComponent<Camera>().orthographicSize < maxZom)
                    {
                        camera_GameObject.GetComponent<Camera>().orthographicSize = camera_GameObject.GetComponent<Camera>().orthographicSize + 0.1f;
                    }
                }
            }
            else
            {
                if (!PanelHerramientas.activeSelf || GameObject.Find("TogleHerramientas").GetComponent<SeleccionDeHerramienta>().herramientaActual == eHerramientas.mover)
                {
                    if (Input.touchCount == 0 && zoom)
                    {
                        zoom = false;
                    }

                    if (Input.touchCount == 1)
                    {
                        if (!zoom)
                        {
                            if (Input.GetTouch(0).phase == TouchPhase.Moved)
                            {
                                Vector2 nuevaPosicion = GetWorldPosition();
                                Vector2 PositionDifference = nuevaPosicion - puntoInicio;
                                camera_GameObject.transform.Translate(-PositionDifference);
                            }
                            puntoInicio = GetWorldPosition();
                        }
                    }
                    else if (Input.touchCount == 2)
                    {
                        if (Input.GetTouch(1).phase == TouchPhase.Moved)
                        {
                            zoom = true;

                            finArrastre = GetWorldPositionDedo(1);
                            Vector2 diferenciaEntrePosiciones = finArrastre - inicioArrastre;

                            if (Vector2.Distance(finArrastre, posicionDedo0) < DistanciaEntreDedos)
                            {
                                if (camera_GameObject.GetComponent<Camera>().orthographicSize + diferenciaEntrePosiciones.magnitude <= maxZom)
                                {
                                    camera_GameObject.GetComponent<Camera>().orthographicSize += (diferenciaEntrePosiciones.magnitude);
                                }
                            }

                            if (Vector2.Distance(finArrastre, posicionDedo0) >= DistanciaEntreDedos)
                            {
                                camera_GameObject.GetComponent<Camera>().orthographicSize -= (diferenciaEntrePosiciones.magnitude);
                            }

                            DistanciaEntreDedos = Vector2.Distance(finArrastre, posicionDedo0);
                        }
                        inicioArrastre = GetWorldPositionDedo(1);
                        posicionDedo0 = GetWorldPositionDedo(0);
                    }
                }
                else
                {
                    zoom = false;
                }
            }
        }
        else
        {
            activo = true;
        }
    }
    /// <summary>
    /// Función que devuelve la posición de la cámara en relación al mundo
    /// </summary>
    /// <returns>La posición de la cámara en relación al mundo</returns>
    Vector2 GetWorldPosition()
    {
        return camera_GameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
    }
    /// <summary>
    /// Función que devuelve la posicion de un dedo en relación al mundo
    /// </summary>
    /// <param name="dedo">Indice del dedo del que queremos saber la posición</param>
    /// <returns>La posición del dedo en relación al mundo</returns>
    Vector2 GetWorldPositionDedo(int dedo)
    {
        return camera_GameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.GetTouch(dedo).position);
    }
}
