using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Click : MonoBehaviour
{
    public GameObject Objetivo, message;
    private string nombre, mensaje;
    private int toquesBuenos, toquesMalos,toques;
    private bool evaluacion;
    private Vector3 posicion;
    private UnityEngine.Color cObjetivo,colorSeleccionado;
    private int MaxObjetos;
    private int dificultad;

    // Start is called before the first frame update
    void Start()
    {
        dificultad = cantidadObjetos.dificultad;
        //Posicionamiento de objetos
        transform.position = Random.insideUnitSphere * 10;
        nombre = gameObject.name;
        MaxObjetos = cantidadObjetos.MaxObjetos;
    }

    public void OnMouseDown()
    {
        //Obtiene color del objeto seleccionado y objetivo
        colorSeleccionado = gameObject.GetComponent<Renderer>().material.color;
        cObjetivo = Objetivo.GetComponent<Text>().color;
        //Evaluacion de nombre del objeto y color
        if(dificultad==1)
        {
            if (nombre == Objetivo.GetComponent<Text>().text)
            {
                Debug.Log(nombre);
                message.GetComponent<Text>().text = "Bien hecho!";
                Invoke("Mensaje", .3f);
                Destroy(gameObject, .35f);
                //Evalua si la cantidad de aciertos es igual a la cantidad de Objetos objetivos
                if (toquesBuenos == MaxObjetos)
                {
                    Debug.Log("Juego terminado");
                    Invoke("MostrarResultados", .3f);
                }
            }
            else
            {
                message.GetComponent<Text>().text = "Uff cerca!";
                Invoke("Mensaje", .3f);
            }
        }
        else if (dificultad==2)
        {
            if (nombre == Objetivo.GetComponent<Text>().text)
            {
                message.GetComponent<Text>().text = "Bien hecho!";
                Invoke("Mensaje", .3f);
                Destroy(gameObject, .35f);
                //Evalua si la cantidad de aciertos es igual a la cantidad de Objetos objetivos
                if (toquesBuenos == MaxObjetos)
                {
                    Debug.Log("Juego terminado");
                    Invoke("MostrarResultados", .3f);
                }
            }
            else
            {
                message.GetComponent<Text>().text = "Uff cerca!";
                Invoke("Mensaje", .3f);
            }
        }
        else if (dificultad==3)
        {

            if (nombre == Objetivo.GetComponent<Text>().text && colorSeleccionado == cObjetivo)
            {
                message.GetComponent<Text>().text = "Bien hecho!";
                Invoke("Mensaje", .3f);
                Destroy(gameObject, .35f);
                //Evalua si la cantidad de aciertos es igual a la cantidad de Objetos objetivos
                if (toquesBuenos == MaxObjetos)
                {
                    Debug.Log("Juego terminado");
                    Invoke("MostrarResultados", .3f);
                }
            }
            else
            {
                message.GetComponent<Text>().text = "Uff cerca!";
                Invoke("Mensaje", .3f);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Definimos cuando sera un error y un acierto
        if (message.GetComponent<Text>().text == "Bien hecho!")
        {
            evaluacion = true;
        }
        else
        {
            evaluacion = false;
        }
        //Contador de aciertos y errores
        if(Input.GetMouseButtonDown(0))
        {
            if (evaluacion)
            {
                toquesBuenos += 1;
                Debug.Log("Aciertos:" + toquesBuenos);
                if (toquesBuenos >= 3)
                {
                    MostrarResultados();
                }
            }
            else
            {
                toquesMalos += 1;
                Debug.Log("Errores:" + toquesMalos);
            }
            toques += 1;
            Debug.Log("Clics:" + toques);
        }
    }

    void Mensaje()
    {
        message.GetComponent<Text>().text = " ";
    }

    void MostrarResultados()
    {
        MotorMicrojuego.Resultados();
    }
}
