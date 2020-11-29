using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cantidadObjetos : MonoBehaviour
{
    public GameObject chocolate, paleta, popsicle, objetivo;

    FabricaDeObjetos fabrica;
    Objeto objetoObjetivo;
    bool tieneObstaculos;

    // Start is called before the first frame update
    void Start()
    {
        SeleccionaObjetos();

        fabrica = new FabricaDeObjetos(paleta, popsicle, chocolate);
        objetoObjetivo = MotorInferencia.Objetivo();
        tieneObstaculos = MotorInferencia.TieneObstaculos();

        IniciarJuego();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void IniciarJuego()
    {
        //Condicion para saber la dificultad de los juegos
        if (tieneObstaculos)
        {
            Instanciar(Objeto.Paleta);
            Instanciar(Objeto.PaletaDeHielo);
            Instanciar(Objeto.Chocolate);
        } 
        else
        {
            switch (objetoObjetivo)
            {
                case Objeto.Paleta:
                    Destroy(chocolate);
                    Destroy(popsicle);
                    Instanciar(Objeto.Paleta);
                    break;

                case Objeto.PaletaDeHielo:
                    Destroy(chocolate);
                    Destroy(paleta);
                    Instanciar(Objeto.PaletaDeHielo);
                    break;

                default:
                    Destroy(paleta);
                    Destroy(popsicle);
                    Instanciar(Objeto.Chocolate);
                    break;
            }
        }
    }

    void Instanciar(Objeto objeto)
    {
        int[] cantidades = MotorInferencia.CantidadObjetos();

        for (int i = 0; i < cantidades[(int) objeto]; i++)
        {
            fabrica.crear(objeto);
        }
    }

    void SeleccionaObjetos()
    {
        MotorInferencia.DeterminarObjetivo();
        objetivo.GetComponent<Text>().text = MotorInferencia.Objetivo().ToString();
    }
}
