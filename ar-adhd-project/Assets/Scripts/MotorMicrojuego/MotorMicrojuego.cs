using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MotorMicrojuego
{
    private const string ESCENA_PRINCIPAL = "Principal";
    private const string ESCENA_RESULTADOS = "Resultados";
    private const string ESCENA_MANUAL = "Manual";

    private const string ESCENA_REGISTRO_PSICOLOGO = "RegistroPsicologo";
    private const string ESCENA_PSICOLOGO_SCAN = "PsicologoScan";
    private const string ESCENA_PSICOLOGO_QR = "PsicologoQR";
    private const string ESCENA_REGISTRO_JUGADOR = "RegistroJugador";
    private const string ESCENA_REGISTRO_TUTOR = "RegistroTutor";
    private const string ESCENA_LOGIN = "Login";

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

    public static void AbrirRegistroPsicologo()
    {
        SceneManager.LoadScene(ESCENA_REGISTRO_PSICOLOGO);
    }

    public static bool DesdeIniciarSesion = true;
    public static void AbrirRegistroJugador()
    {
        DesdeIniciarSesion = false;
        SceneManager.LoadScene(ESCENA_REGISTRO_JUGADOR);
    }
    public static void AbrirRegistroJugadorIncioSesion()
    {
        DesdeIniciarSesion = true;
        SceneManager.LoadScene(ESCENA_REGISTRO_JUGADOR);
    }

    public static void AbrirRegistroTutor()
    {
        SceneManager.LoadScene(ESCENA_REGISTRO_TUTOR);
    }

    public static void AbrirLogin()
    {
        SceneManager.LoadScene(ESCENA_LOGIN);
    }

    public static void AbrirPsicologoScan()
    {
        SceneManager.LoadScene(ESCENA_PSICOLOGO_SCAN);
    }

    public static void AbrirPsicologoQr()
    {
        SceneManager.LoadScene(ESCENA_PSICOLOGO_QR);
    }

    public static void CerrarSesion()
    {
        JugadorDAO jugadorDAO = new JugadorDAO();
        Jugador jugador = jugadorDAO.BuscarJugadorActivo();
        if (jugador != null)
        {
            jugador.Sesion = false;
            jugadorDAO.Actualizar(jugador);
            MotorMicrojuego.MenuPrincipal();
        }
    }

}