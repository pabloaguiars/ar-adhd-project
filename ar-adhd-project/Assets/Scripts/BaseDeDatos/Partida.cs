using System;
using System.Collections.Generic;

public class Partida
{
    public int IdPartida, Nivel, Dificultad, Intentos, Aciertos, Errores, Tiempo;
    public DateTime fechaHora;
    public Jugador Jugador;
}

public class PartidaDAO
{
    public void Guardar(Partida Partida)
    {

    }

    public void Eliminar(Partida Partida)
    {

    }

    public Partida Buscar(int IdPartida)
    {
        return null;
    }

    public List<Partida> Lista()
    {
        return null;
    }
}