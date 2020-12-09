using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitacoraResultados
{
    JugadorDAO JugadorDAO = new JugadorDAO();
    Jugador Jugador = new Jugador();

    PartidaDAO PartidaDAO = new PartidaDAO();
    Partida Partida = new Partida();

    public void Guardar()
    {
        Jugador = JugadorDAO.BuscarJugadorActivo();
        if (Jugador!= null)
        {
            Partida.IdJugador = Jugador.IdJugador;
            Partida.Nivel = MotorInferencia.nivel;
            Partida.Dificultad = MotorInferencia.dificultad;
            Partida.Aciertos = Click.toquesBuenos;
            Partida.Errores = Click.toquesMalos;
            Partida.Intentos = Click.toques;
            Partida.FechaTiempo = System.DateTime.UtcNow;
            Partida.Tiempo = (int)Reloj.tiempoAMostrarEnSegundos * 1000;
            PartidaDAO.Crear(Partida);
        }
    }
}
