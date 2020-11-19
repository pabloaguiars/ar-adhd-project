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

    // Start is called before the first frame update
    void Start()
    {
        colores[0] = new UnityEngine.Color(1f,0f,0f);
        colores[1] = new UnityEngine.Color(0f, 1f, 0f);
        colores[2] = new UnityEngine.Color(0f,0f,1f);
        coloresObjetivo = colores[Random.Range(0,3)];
        GetComponent<Renderer>().material.color = coloresObjetivo;
        IndicadorColor.GetComponent<Text>().color = coloresObjetivo;

        //coloresPaletas = GetComponent<Renderer>();
        //coloresPaletas.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        //string color = colorObjetivo.ToString();
        //IndicadorColor = GetComponent<Text>();
        //IndicadorColor.text = "hola";

        //IndicadorColor.GetComponent<Text>().text = "hola";
        //IndicadorColor.GetComponent<Text>().color = colorObjetivo;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
