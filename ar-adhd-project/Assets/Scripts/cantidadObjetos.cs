using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cantidadObjetos : MonoBehaviour
{
    public GameObject esferas, paleta, cubo;
    private Vector3 posicion;
    private float posicionX, posicionY;
    private float[] posicionZ = new float[6];
    private int cantidadEsferas, cantidadPaletas, cantidadCubos,MaxObjetos;
    // Start is called before the first frame update
    void Start()
    {
        posicionX = Random.Range(3f, 6f);
        posicionY = Random.Range(0f, 3f);
        posicion = new Vector3(Random.Range(-posicionX, posicionX), Random.Range(-posicionY, posicionY), posicionZ[Random.Range(0, 5)]);
        posicionZ[0] = 5;
        posicionZ[1] = 6;
        posicionZ[2] = 7;
        posicionZ[3] = -5;
        posicionZ[4] = -6;
        posicionZ[5] = -7;

        MaxObjetos = 2;
        cantidadEsferas = Random.Range(0, MaxObjetos);
        cantidadPaletas = Random.Range(0, MaxObjetos);
        cantidadCubos = Random.Range(0, MaxObjetos);

        for (int conta = 0; conta <= cantidadEsferas; conta++)
        {
            GameObject Esferas = Instantiate(esferas, posicion, Quaternion.identity);
            Esferas.name = "Esfera";
        }
        for (int conta2 = 0; conta2 <= cantidadPaletas; conta2++)
        {
            GameObject Paleta = Instantiate(paleta, posicion, Quaternion.Euler(-89,-43,43));
            Paleta.name = "Paleta";
        }
        for (int conta3 = 0; conta3 <= cantidadCubos; conta3++)
        {
            GameObject Cubo = Instantiate(cubo, posicion, Quaternion.identity);
            Cubo.name = "Cubo";
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
