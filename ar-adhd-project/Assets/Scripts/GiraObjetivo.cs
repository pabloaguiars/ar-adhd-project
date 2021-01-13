using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiraObjetivo : MonoBehaviour
{
    Objeto objeto;

    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    void Start()
    {
        if (gameObject.name == "Paleta de Hielo Objetivo")
        {
            objeto = Objeto.PaletaDeHielo;
        }
        else
        {
            objeto = Objeto.Chocolate;
        }

        gradient = new Gradient();

        colorKey = new GradientColorKey[3]; 
        colorKey[0].color = DecodificadorDeColor.decodificar(ColorObjeto.Azul);
        colorKey[0].time = 0.0f;

        colorKey[1].color = DecodificadorDeColor.decodificar(ColorObjeto.Blanco);
        colorKey[1].time = 0.5f;

        colorKey[2].color = DecodificadorDeColor.decodificar(ColorObjeto.Rosa);
        colorKey[2].time = 1.0f;

        alphaKey = new GradientAlphaKey[3];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 0.5f;

        alphaKey[2].alpha = 1.0f;
        alphaKey[2].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
    }

    public Vector3 direccion;
    public float velocidad = 30;

    public void Update()
    {
        transform.Rotate(direccion * velocidad * Time.deltaTime);

        if (!MotorInferencia.TieneColores()) {
            if (objeto == Objeto.PaletaDeHielo)
            {
                GetComponent<Renderer>().materials[1].color = gradient.Evaluate(Mathf.PingPong(Time.time, 2.0f) / 2.0f);
            }
            else
            {
                GetComponent<Renderer>().material.color = gradient.Evaluate(Mathf.PingPong(Time.time, 2.0f) / 2.0f);
            }
        }
    }
}
