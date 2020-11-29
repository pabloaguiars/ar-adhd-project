//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Click : MonoBehaviour
{
    public AudioSource correcto, incorrecto;
    public GameObject Objetivo, message;
    private string mensaje;
    private int toquesBuenos, toquesMalos,toques;
    private bool evaluacion;
    private UnityEngine.Color cObjetivo,colorSeleccionado;
    private int CantidadObjetos;
    private bool TieneColores;
    Objeto objeto, objetivo;

    private double traslado_x = 0.0f;

    //private int dificultad;

    // Start is called before the first frame update
    void Start()
    {   
        //Posicionamiento de objetos
        transform.position = Random.insideUnitSphere * 10;
        System.Enum.TryParse<Objeto>(gameObject.name, out objeto);

        objetivo = MotorInferencia.Objetivo();
        CantidadObjetos = MotorInferencia.CantidadObjetos()[(int) objetivo];
        TieneColores = MotorInferencia.TieneColores();
    }

    private void Correcto()
    {
        correcto.Play();
        Debug.Log(objeto);
        message.GetComponent<Text>().text = "Bien hecho!";
        Invoke("Mensaje", .3f);
        Destroy(gameObject, .35f);
        //Evalua si la cantidad de aciertos es igual a la cantidad de Objetos objetivos
        if (toquesBuenos == CantidadObjetos)
        {
            Debug.Log("Juego terminado");
            Invoke("MostrarResultados", .3f);
        }
    }

    private void Incorrecto()
    {
        incorrecto.Play();
        message.GetComponent<Text>().text = "Uff cerca!";
        Invoke("Mensaje", .3f);
    }

    public void OnMouseDown()
    {
        //Obtiene color del objeto seleccionado y objetivo
        colorSeleccionado = gameObject.GetComponent<Renderer>().material.color;
        cObjetivo = Objetivo.GetComponent<Text>().color;

        //Evaluacion de nombre del objeto y color
        if (TieneColores)
        {
            if (objeto == objetivo && colorSeleccionado == cObjetivo)
            {
                Correcto();
            }
            else
            {
                Incorrecto();
            }
        } 
        else
        {
            if (objeto == objetivo)
            {
                Correcto();
            }
            else
            {
                Incorrecto();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        traslado_x += 0.01;

        gameObject.transform.Translate( (float) System.Math.Cos(traslado_x) / 20.0f, 0, 0);



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
            }
            else
            {
                toquesMalos += 1;
                Debug.Log("Errores:" + toquesMalos);
                if (toquesMalos >= 3)
                {
                    MostrarResultados();
                }
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
