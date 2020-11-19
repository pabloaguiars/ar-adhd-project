using System.Collections;
using System.Collections.Generic;

public class MotorMicrojuego
{
    private const string ESCENA_PRINCIPAL = "Principal";
    private const string ESCENA_RESULTADOS = "Resultados";
    private const string ESCENA_MANUAL = "Manual";
    private static List<string> ESCENAS = new List<string>() {
        "SampleScene"
    };
    private static IMotorMicrojuego motor;

    public static void SeleccionarAleatoreo()
    {
        motor = new MotorMicrojuegoAleatoreo(ESCENA_PRINCIPAL, ESCENA_RESULTADOS, ESCENAS);
    }

    public static void SeleccionarSecuencial()
    {
        motor = new MotorMicrojuegoSecuencial(ESCENA_PRINCIPAL, ESCENA_RESULTADOS, ESCENAS);
    }

    public static void SeleccionarManual()
    {
        motor = new MotorMicrojuegoManual(ESCENA_PRINCIPAL, ESCENA_RESULTADOS, ESCENA_MANUAL, ESCENAS);
    }

    public static void Repetir()
    {
        if (motor == null)
        {
            SeleccionarSecuencial();
        }
        motor.Repetir();
    }

    public static void Resultados()
    {
        if (motor == null)
        {
            SeleccionarSecuencial();
        }
        motor.Resultados();
    }

    public static void Siguiente()
    {
        if (motor == null)
        {
            SeleccionarSecuencial();
        }
        motor.Siguiente();
    }

    public static void Seleccionar(string escena)
    {
        if (motor == null)
        {
            SeleccionarSecuencial();
        }
        motor.Seleccionar(escena);
    }

    public static void MenuPrincipal()
    {
        if (motor == null)
        {
            SeleccionarSecuencial();
        }
        motor.MenuPrincipal();
    }
}