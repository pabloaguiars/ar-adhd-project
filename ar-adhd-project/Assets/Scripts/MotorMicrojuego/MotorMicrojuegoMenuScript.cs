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
}
