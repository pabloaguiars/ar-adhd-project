using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class cantidadObjetos : MonoBehaviour
{
    public GameObject objetivoPaleta, objetivoChocolate, objetivoPaletaDeHielo;
    public GameObject chocolate, paleta, paletaDeHielo;
    public GameObject nivelLabel;

    FabricaDeObjetos fabrica;
    Objeto objetoObjetivo;
    ColorObjeto colorObjetivo;
    bool tieneObstaculos, tieneColores;
    int nivel, dificultad;

    // Start is called before the first frame update
    void Start()
    {

        fabrica = new FabricaDeObjetos(paleta, paletaDeHielo, chocolate);

        SeleccionaObjetos();
       
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
                    Instanciar(Objeto.Paleta);
                    break;

                case Objeto.PaletaDeHielo:
                    Instanciar(Objeto.PaletaDeHielo);
                    break;

                default:
                    Instanciar(Objeto.Chocolate);
                    break;
            }
        }
        Destroy(paletaDeHielo);
        Destroy(chocolate);
        Destroy(paleta);
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

        objetoObjetivo = MotorInferencia.Objetivo();
        colorObjetivo = MotorInferencia.Color();
        tieneColores = MotorInferencia.TieneColores();
        nivel = MotorInferencia.nivel;
        dificultad = MotorInferencia.dificultad;

        nivelLabel.GetComponent<Text>().text = string.Format("N: {0}, D: {1}", nivel + 1, dificultad + 1);

        switch (objetoObjetivo)
        {
            case Objeto.Paleta:
                if (tieneColores) 
                { 
                    objetivoPaleta.GetComponent<Renderer>().material.color = DecodificadorDeColor.decodificar(colorObjetivo);
                }
                Destroy(objetivoPaletaDeHielo);
                Destroy(objetivoChocolate);
                break;

            case Objeto.PaletaDeHielo:

                if (tieneColores)
                {
                    objetivoPaletaDeHielo.GetComponent<Renderer>().material.color = DecodificadorDeColor.decodificar(colorObjetivo);
                }
                Destroy(objetivoPaleta);
                Destroy(objetivoChocolate);
                break;

            default:
                if (tieneColores)
                { 
                    objetivoChocolate.GetComponent<Renderer>().material.color = DecodificadorDeColor.decodificar(colorObjetivo);
                }
                Destroy(objetivoPaleta);
                Destroy(objetivoPaletaDeHielo);
                break;
        }

    }
}
