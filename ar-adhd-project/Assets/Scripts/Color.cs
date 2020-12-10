using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Color : MonoBehaviour
{
    private bool TieneColores = false;

    // Start is called before the first frame update
    void Start()
    {
        //ColorObjeto colorObjeto = (ColorObjeto) Random.Range(0, 3);

        Objeto objeto;
        //Debug.Log(gameObject.name);
        System.Enum.TryParse<Objeto>(gameObject.name, out objeto);

        ColorObjeto colorObjeto = MotorInferencia.IteradorDeColores.Siguiente(objeto);

        TieneColores = MotorInferencia.TieneColores();

        if(objeto==Objeto.PaletaDeHielo)
        {
            GetComponent<Renderer>().materials[1].color = DecodificadorDeColor.decodificar(colorObjeto);
        }
        else
        {
            GetComponent<Renderer>().material.color = DecodificadorDeColor.decodificar(colorObjeto);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
