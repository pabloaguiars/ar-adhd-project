using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

class MotorMicrojuegoSecuencial : IMotorMicrojuego
{
    private string escenaPrincipal;
    private string escenaResultados;
    private List<string> escenas;
    private int indice;

    public MotorMicrojuegoSecuencial(string EscenaPrincipal, string EscenaResultados, List<string> Escenas)
    {
        escenaPrincipal = EscenaPrincipal;
        escenaResultados = EscenaResultados;
        escenas = Escenas;
        indice = 0;
    }

    public void Resultados()
    {
        SceneManager.LoadScene(escenaResultados);
    }

    public void Repetir()
    {
        SceneManager.LoadScene(escenas[indice]);
    }

    public void Siguiente()
    {
        SceneManager.LoadScene(escenas[indice++]);
        if (indice >= escenas.Count)
        {
            indice = 0;
        }
    }

    public void Seleccionar(string Escena)
    {
        int busqueda = escenas.IndexOf(Escena);
        if (busqueda > 0)
        {
            indice = busqueda;
        }
        else
        {
            throw new Exception(String.Format("La escena {0} no existe", Escena));
        }
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene(escenaPrincipal);
    }
}