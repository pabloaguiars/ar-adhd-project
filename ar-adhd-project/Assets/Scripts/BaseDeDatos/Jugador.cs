using System.Collections.Generic;

public class Jugador
{
    public int IdJugador, Edad;
    public string Nombre, NombreUsuario, Sexo, Contrasena;
    public Tutor Tutor;
    public Psicologo Psicologo;
}

public class JugadorDAO
{
    public void Guardar(Jugador Jugador)
    {

    }

    public void Eliminar(Jugador Jugador)
    {

    }

    public Jugador Buscar(int IdJugador)
    {
        return null;
    }

    public List<Jugador> Lista()
    {
        return null;
    }
}