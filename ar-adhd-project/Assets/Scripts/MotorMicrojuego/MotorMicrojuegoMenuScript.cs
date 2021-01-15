using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorMicrojuegoMenuScript : MonoBehaviour
{
    public void Repetir()
    {
        MotorMicrojuego.Repetir();
    }

    public void Siguiente()
    {
        MotorMicrojuego.Siguiente();
    }

    public void MenuPrincipal()
    {
        MotorMicrojuego.MenuPrincipal();
    }

    public void Resultados()
    {
        MotorMicrojuego.MenuPrincipal();
    }

    public void AbrirRegistroPsicologo()
    {
        MotorMicrojuego.AbrirRegistroPsicologo();
    }

    public void AbrirRegistroScan()
    {
        MotorMicrojuego.AbrirPsicologoScan();
    }

    public void AbrirPsicologoQr()
    {
        MotorMicrojuego.AbrirPsicologoQr();
    }

    public void AbrirRegistroJugador()
    {
        MotorMicrojuego.AbrirRegistroJugador();
    }

    public void AbrirRegistroJugadorInicioSesion()
    {
        MotorMicrojuego.AbrirRegistroJugadorIncioSesion();
    }

    public void AbrirLogin()
    {
        MotorMicrojuego.AbrirLogin();
    }

    public void AbrirRegistroTutor()
    {
        MotorMicrojuego.AbrirRegistroTutor();
    }

    public void CerrarSesion()
    {
        MotorMicrojuego.CerrarSesion();
    }
}
