using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Color : MonoBehaviour
{
    
    public GameObject IndicadorColor;
    public UnityEngine.Color coloresObjetivo;
    private Renderer coloresPaletas;
    private UnityEngine.Color[] colores = new UnityEngine.Color[3];
    private int dificultad;

    // Start is called before the first frame update
    void Start()
    {
        //Obtiene dificultad
        dificultad = cantidadObjetos.dificultad;
        //Asignación de colores al arreglo
        colores[0] = new UnityEngine.Color(1f,0f,0f);
        colores[1] = new UnityEngine.Color(0f, 1f, 0f);
        colores[2] = new UnityEngine.Color(0f,0f,1f);
        //Pintamos el objeto de acuerdo al nivel
        if (dificultad==1)
        {
            coloresObjetivo = colores[0];
            GetComponent<Renderer>().material.color = coloresObjetivo;
            IndicadorColor.GetComponent<Text>().color = coloresObjetivo;
        }
        else if(dificultad>=2)
        {
            coloresObjetivo = colores[Random.Range(0, 3)];
            GetComponent<Renderer>().material.color = coloresObjetivo;
            IndicadorColor.GetComponent<Text>().color = coloresObjetivo;
        }
        else
        {
            Debug.Log("Hay algun error");
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
