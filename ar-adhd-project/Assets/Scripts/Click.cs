//using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Click : MonoBehaviour
{
    public AudioSource correcto, incorrecto;
    public GameObject message;
    private string mensaje;
    private static int toquesBuenos, toquesMalos,toques;
    private bool evaluacion;
    private UnityEngine.Color colorObjetivo,colorSeleccionado;
    private int CantidadObjetos;
    private bool TieneColores, SeMueven;
    private static bool clic = false, clicAnterior = false;

    Objeto objeto, objetivo;

    private double traslado_x = 0.0f;

    //private int dificultad;

    // Start is called before the first frame update
    void Start()
    {
        toquesBuenos = 0;
        toquesMalos = 0;
        toques = 0;

        clic = false;
        clicAnterior = false;

        //Posicionamiento de objetos
        transform.position = Random.insideUnitSphere * 5;
        System.Enum.TryParse<Objeto>(gameObject.name, out objeto);

        objetivo = MotorInferencia.Objetivo();
        colorObjetivo = DecodificadorDeColor.decodificar(MotorInferencia.Color());
        CantidadObjetos = MotorInferencia.CantidadObjetivos();
        TieneColores = MotorInferencia.TieneColores();
        SeMueven = MotorInferencia.SeMueven();
    }

    private void Correcto()
    {
        toquesBuenos += 1;
        correcto.Play();
        Debug.Log(objeto);
        message.GetComponent<Text>().text = "Bien hecho!";
        Invoke("Mensaje", .3f);
        Destroy(gameObject, .35f);
        //Evalua si la cantidad de aciertos es igual a la cantidad de Objetos objetivos
        if (toquesBuenos >= CantidadObjetos)
        {
            Debug.Log("Juego terminado");
            Invoke("MostrarResultados", .3f);
        }
    }

    private void Incorrecto()
    {
        toquesMalos += 1;
        incorrecto.Play();
        message.GetComponent<Text>().text = "Uff cerca!";
        Invoke("Mensaje", .3f);

        if (toquesMalos >= 3)
        {
            MostrarResultados();
        }
    }

    public void OnMouseDown()
    {
        //Obtiene color del objeto seleccionado y objetivo
        colorSeleccionado = gameObject.GetComponent<Renderer>().material.color;

        //Evaluacion de nombre del objeto y color
        if (TieneColores)
        {
            if (objeto == objetivo && colorSeleccionado == colorObjetivo)
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

        Debug.Log("Clics:" + toques);
        Debug.Log("Aciertos:" + toquesBuenos);
        Debug.Log("Errores:" + toquesMalos);
    }


    // Update is called once per frame
    void Update()
    {

        if (SeMueven) 
        { 
            traslado_x += 0.01;

            gameObject.transform.Translate( (float) System.Math.Cos(traslado_x) / 30.0f, 0, 0);
        }

        clicAnterior = clic;
        clic = Input.GetMouseButtonDown(0);
        if (!clicAnterior && clic)
        {
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
