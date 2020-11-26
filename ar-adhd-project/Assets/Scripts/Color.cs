using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Color : MonoBehaviour
{
    
    public GameObject IndicadorColor;
    public UnityEngine.Color coloresObjetivo;
    private UnityEngine.Color[] colores = new UnityEngine.Color[3];
    private int dificultad;

    // Start is called before the first frame update
    void Start()
    {
        //Obtiene dificultad
        dificultad = cantidadObjetos.dificultad;
        //Asignación de colores al arreglo
        colores[0] = new UnityEngine.Color(1f,1f,1f);
        colores[1] = new UnityEngine.Color(1, 0.6470588235294118F, 0.6549019607843137F);
        colores[2] = new UnityEngine.Color(0.0549019607843137F, 0.7058823529411765F, 0.8901960784313725F);
        //Pintamos el objeto de acuerdo al nivel
        if (dificultad<=2)
        {
            coloresObjetivo = colores[Random.Range(0, 3)];
            GetComponent<Renderer>().material.color = coloresObjetivo;
            IndicadorColor.GetComponent<Text>().color = new UnityEngine.Color(1f, 0f, 0f);
        }
        else if(dificultad==3)
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
