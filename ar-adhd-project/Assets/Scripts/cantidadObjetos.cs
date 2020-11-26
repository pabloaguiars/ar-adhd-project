using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cantidadObjetos : MonoBehaviour
{
    public GameObject chocolate, paleta, popsicle, objetivo;
    private Vector3 posicion;
    private int cantidadChocolates, cantidadPaletas, cantidadCono;
    public static int MaxObjetos;
    public static int dificultad;
    private string nombre;
    public int numero;
    string[] figuras = new string[4];

    // Start is called before the first frame update
    void Start()
    {
        //dificultad = Random.Range(1, 4);
        dificultad = 3;
        SeleccionaObjetos();
        IniciarJuego(dificultad);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void IniciarJuego(int dificultad)
    {
        //Condicion para saber la dificultad de los juegos
        if (dificultad == 1)
        {
            //Cantidad máxima de objetos
            MaxObjetos = 5;
            //Posicionamiento de los objeto
            posicion = Random.insideUnitSphere;
            //Aleatorio de cantidad maxima de objetos
            cantidadPaletas = Random.Range(3, MaxObjetos);

            //Instanzación de objetos
            if (objetivo.GetComponent<Text>().text =="Paleta")
            {
                //Destruir objetos
                Destroy(chocolate);
                Destroy(popsicle);
                for (int conta2 = 1; conta2 < cantidadPaletas; conta2++)
                {
                    GameObject Paleta = Instantiate(paleta, posicion, Quaternion.Euler(-89, -43, 43));
                    Paleta.name = "Paleta";
                }
            }
            else if (objetivo.GetComponent<Text>().text == "Popsicle")
            {
                //Destruir objetos
                Destroy(chocolate);
                Destroy(paleta);
                for (int conta2 = 1; conta2 < cantidadPaletas; conta2++)
                {
                    GameObject Popsicle = Instantiate(popsicle, posicion, Quaternion.Euler(-90,0,0));
                    Popsicle.name = "Popsicle";
                }
            }
            else if (objetivo.GetComponent<Text>().text == "Chocolate")
            {
                //Destruir objetos
                Destroy(paleta);
                Destroy(popsicle);
                for (int conta2 = 1; conta2 < cantidadPaletas; conta2++)
                {
                    GameObject Chocolate = Instantiate(chocolate, posicion, Quaternion.identity);
                    Chocolate.name = "Chocolate";
                }
            }
            
        }
        else if (dificultad == 2)
        {
            //Cantidad máxima de objetos
            MaxObjetos = 6;
            //Posicionamiento de los objeto
            posicion = Random.insideUnitSphere;
            //Obtención del nombre del objeto
            nombre = gameObject.name;
            //Aleatorio de cantidad maxima de objetos
            cantidadChocolates = Random.Range(3, MaxObjetos);
            cantidadPaletas = Random.Range(3, MaxObjetos);
            cantidadCono = Random.Range(3, MaxObjetos);
            //Instanzación de objetos
            for (int conta = 1; conta < cantidadChocolates; conta++)
            {
                GameObject Chocolate = Instantiate(chocolate, posicion, Quaternion.identity);
                Chocolate.name = "Chocolate";
            }
            for (int conta2 = 1; conta2 < cantidadPaletas; conta2++)
            {
                GameObject Paleta = Instantiate(paleta, posicion, Quaternion.Euler(-89, -43, 43));
                Paleta.name = "Paleta";
            }
            for (int conta3 = 1; conta3 < cantidadCono; conta3++)
            {
                GameObject Popsicle = Instantiate(popsicle, posicion, Quaternion.Euler(-90, 0, 0));
                Popsicle.name = "Popsicle";
            }
        }
        else if (dificultad == 3)
        {
            //Cantidad máxima de objetos
            MaxObjetos = 7;
            //Aleatorio de cantidad maxima de objetos
            cantidadChocolates = Random.Range(3, MaxObjetos);
            cantidadPaletas = Random.Range(3, MaxObjetos);
            cantidadCono = Random.Range(3, MaxObjetos);
            //Posicionamiento de los objeto
            posicion = Random.insideUnitSphere;
            //Aleatorio de cantidad maxima de objetos
            nombre = gameObject.name;
            //Instanciación de objetos
            for (int conta = 1; conta < cantidadChocolates; conta++)
            {
                GameObject Chocolate = Instantiate(chocolate, posicion, Quaternion.identity);
                Chocolate.name = "Chocolate";
            }
            for (int conta2 = 1; conta2 < cantidadPaletas; conta2++)
            {
                GameObject Paleta = Instantiate(paleta, posicion, Quaternion.Euler(-89, -43, 43));
                Paleta.name = "Paleta";
            }
            for (int conta3 = 1; conta3 < cantidadCono; conta3++)
            {
                GameObject Popsicle = Instantiate(popsicle, posicion, Quaternion.Euler(-90, 0, 0));
                Popsicle.name = "Popsicle";
            }
        }
        else
        {
            //Mensaje de error
            Debug.Log("hay algun error");
        } 
    }

    void SeleccionaObjetos()
    {
        figuras[0] = "Popsicle";
        figuras[1] = "Chocolate";
        figuras[2] = "Paleta";
        //numero = Random.Range(0, 3);
        objetivo.GetComponent<Text>().text = figuras[Random.Range(0, 3)];
    }
}
