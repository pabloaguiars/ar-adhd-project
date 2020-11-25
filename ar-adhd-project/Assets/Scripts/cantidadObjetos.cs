using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cantidadObjetos : MonoBehaviour
{
    public GameObject chocolate, paleta, candy;
    private Vector3 posicion;
    private int cantidadChocolates, cantidadPaletas, cantidadCandys;
    public static int MaxObjetos;
    public static int dificultad;
    private string nombre;
  
    // Start is called before the first frame update
    void Start()
    {
        dificultad = Random.Range(1, 4);
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
            //Obtención del nombre del objeto
            nombre = gameObject.name;
            //Aleatorio de cantidad maxima de objetos
            cantidadPaletas = Random.Range(3, MaxObjetos);
            //Destruir objetos
            Destroy(chocolate);
            Destroy(candy);
            //Instanzación de objetos
            for (int conta2 = 1; conta2 < cantidadPaletas; conta2++)
            {
                GameObject Paleta = Instantiate(paleta, posicion, Quaternion.Euler(-89, -43, 43));
                Paleta.name = "Paleta";
            }
        }
        else if (dificultad == 2)
        {
            //Cantidad máxima de objetos
            MaxObjetos = 10;
            //Posicionamiento de los objeto
            posicion = Random.insideUnitSphere;
            //Obtención del nombre del objeto
            nombre = gameObject.name;
            //Aleatorio de cantidad maxima de objetos
            cantidadPaletas = Random.Range(3, MaxObjetos);
            //Destruir objetos
            Destroy(chocolate);
            Destroy(candy);
            //Instanzación de objetos
            for (int conta2 = 1; conta2 < cantidadPaletas; conta2++)
            {
                GameObject Paleta = Instantiate(paleta, posicion, Quaternion.Euler(-89, -43, 43));
                Paleta.name = "Paleta";
            }
        }
        else if (dificultad == 3)
        {
            //Cantidad máxima de objetos
            MaxObjetos = 15;
            //Aleatorio de cantidad maxima de objetos
            cantidadChocolates = Random.Range(3, MaxObjetos);
            cantidadPaletas = Random.Range(3, MaxObjetos);
            cantidadCandys = Random.Range(3, MaxObjetos);
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
            for (int conta3 = 1; conta3 < cantidadCandys; conta3++)
            {
                GameObject Candy = Instantiate(candy, posicion, Quaternion.identity);
                Candy.name = "Candy";
            }
        }
        else
        {
            //Mensaje de error
            Debug.Log("hay algun error");
        } 
    }
}
