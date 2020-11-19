using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Click : MonoBehaviour
{
    public GameObject Objetivo, message;
    private string nombre, mensaje;
    private float posicionX, posicionY;
    private float[] posicionZ = new float[6];
    private int toquesBuenos, toquesMalos,toques;
    private bool evaluacion;
    private Vector3 posicion;

    // Start is called before the first frame update
    void Start()
    {
        //Posicion de los objetos originales
        //posicionX = Random.Range(3f, 6f);
        //posicionY = Random.Range(0f, 2f);
        //posicionZ[0] = 5;
        //posicionZ[1] = 6;
        //posicionZ[2] = 7;
        //posicionZ[3] = -5;
        //posicionZ[4] = -6;
        //posicionZ[5] = -7;

        //transform.position = new Vector3(Random.Range(-posicionX, posicionX), Random.Range(-posicionY, posicionY), posicionZ[Random.Range(0, 5)]);
        transform.position = Random.insideUnitSphere * 10;
        nombre = gameObject.name;
        
    }

    public void OnMouseDown()
    {
        if (nombre == Objetivo.GetComponent<Text>().text)
        {
            message.GetComponent<Text>().text = "Bien hecho!";
            Invoke("Mensaje", .3f);
            Destroy(gameObject,.35f);
        }
        else
        {
            message.GetComponent<Text>().text = "Uff cerca!";
            Invoke("Mensaje", .3f);
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
