using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Objeto
{
    Paleta,
    PaletaDeHielo,
    Chocolate
}

public enum ColorObjeto
{
    Azul,
    Rosa,
    Blanco
}

public class FabricaDeObjetos : MonoBehaviour
{
    private static readonly Quaternion PALETA = Quaternion.Euler(-89, -43, 43);
    private static readonly Quaternion PALETA_DE_HIELO = Quaternion.Euler(-90, 0, 0);
    private static readonly Quaternion CHOCOLATE = Quaternion.identity;
    private static readonly Vector3 posicion = Random.insideUnitSphere;

    private GameObject[] objetos;


    public FabricaDeObjetos(params GameObject[] Objetos)
    {
        objetos = Objetos;
    }

    public GameObject crear(Objeto Objeto)
    {
        int[] cantidades = MotorInferencia.CantidadObjetos();
        GameObject ObjetoDeJuego;

        switch (Objeto)
        {
            case Objeto.Paleta:
                ObjetoDeJuego = Instantiate(objetos[(int)Objeto.Paleta], posicion, PALETA);
                ObjetoDeJuego.name = Objeto.Paleta.ToString();
                return ObjetoDeJuego;

            case Objeto.PaletaDeHielo:
                ObjetoDeJuego = Instantiate(objetos[(int)Objeto.PaletaDeHielo], posicion, PALETA_DE_HIELO);
                ObjetoDeJuego.name = Objeto.PaletaDeHielo.ToString();
                return ObjetoDeJuego;

            default:
                ObjetoDeJuego = Instantiate(objetos[(int)Objeto.Chocolate], posicion, CHOCOLATE);
                ObjetoDeJuego.name = Objeto.Chocolate.ToString();
                return ObjetoDeJuego;
        }
    }
}

public class DecodificadorDeColor
{
    private static readonly UnityEngine.Color AZUL = new UnityEngine.Color(0.0549019607843137F, 0.7058823529411765F, 0.8901960784313725F);
    private static readonly UnityEngine.Color ROSA = new UnityEngine.Color(1F, 0.6470588235294118F, 0.6549019607843137F);
    private static readonly UnityEngine.Color BLANCO = new UnityEngine.Color(1F, 1F, 1F);

    public static UnityEngine.Color decodificar(ColorObjeto ColorObjeto)
    {
        switch (ColorObjeto)
        {
            case ColorObjeto.Azul: return AZUL;
            case ColorObjeto.Rosa: return ROSA;
            default: return BLANCO;
        }
    }
}

public class MotorInferencia
{
    // ESPACIO DE MEMORIA
    private static int dificultad = 1;
    private static Objeto objetivo = Objeto.Paleta;
    private static ColorObjeto colorObjeto = ColorObjeto.Blanco;
    private static int[] cantidadObjetos = new int[] { 0, 0, 0 };

    private static readonly int[] CANTIDAD_OBJETOS = new int[] { 5, 6, 7 };
    private static readonly bool[] TIENE_COLORES = new bool[] { false, false, true };
    private static readonly bool[] TIENE_OBSTACULOS = new bool[] { false, true, true };

    public static void DeterminarObjetivo()
    {
        objetivo = (Objeto)Random.Range(0, 3);
        colorObjeto = (ColorObjeto)Random.Range(0, 3);

        for (int i = 0; i < cantidadObjetos.Length; i++) { 
            cantidadObjetos[i] = Random.Range(3, CANTIDAD_OBJETOS[dificultad]);
        }
    }

    public static Objeto Objetivo()
    {
        return objetivo;
    }

    public static ColorObjeto Color()
    {
        return colorObjeto;
    }

    public static int[] CantidadObjetos()
    {
        return cantidadObjetos;
    }

    public static bool TieneColores()
    {
        return TIENE_COLORES[dificultad];
    }

    public static bool TieneObstaculos()
    {
        return TIENE_OBSTACULOS[dificultad];
    }
}
