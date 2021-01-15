using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MotorMicrojuegoAleatoreo : IMotorMicrojuego
{
    private string escenaPrincipal;
    private string escenaResultados;
    private List<string> escenas;
    private Random random;
    private int indice;

    public MotorMicrojuegoAleatoreo(
        string EscenaPrincipal, 
        string EscenaResultados,
        List<string> Escenas)
    {
        escenaPrincipal = EscenaPrincipal;
        escenaResultados = EscenaResultados;
        escenas = Escenas;
        random = new Random();
        indice = random.Next(0, escenas.Count);
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
        indice = random.Next(0, escenas.Count);
        SceneManager.LoadScene(escenas[indice]);
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
