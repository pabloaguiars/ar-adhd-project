using Mono.Data.Sqlite;
using System.Collections.Generic;
using UnityEngine;

public class Jugador
{
    public int IdJugador, Edad;
    public string Nombre, NombreUsuario, Sexo, Contrasena;
    public bool Sesion;

    public int IdTutor, IdPsicologo;

    public Tutor Tutor 
    {
        get => new TutorDAO().Buscar(IdTutor);
        set => IdTutor = value.IdTutor;
    }
    public Psicologo Psicologo
    {
        get => new PsicologoDAO().Buscar(IdPsicologo);
        set => IdPsicologo = value.IdPsicologo;
    }

    public override string ToString()
    {
        return string.Format("{0} {1} {2} {3} {4} {5} ({6}) ({7})",
            IdJugador, Edad, Nombre, NombreUsuario, Sexo, Contrasena,
            Tutor.ToString(), Psicologo.ToString()
        );
    }
}

public class JugadorDAO
{
    private string CONNECTION_STRING = "URI=file:" + Application.dataPath + "/Plugins/BD.db";

    public void Crear(Jugador Jugador)
    {
        string query = @"
            INSERT INTO Jugadores(
                Edad, 
                Nombre,
                NombreUsuario, 
                Sexo, 
                Contrasena,
                IdTutor,
                IdPsicologo,
                Sesion
            ) 
            VALUES (
                @Edad, 
                @Nombre,
                @NombreUsuario, 
                @Sexo, 
                @Contrasena,
                @IdTutor,
                @IdPsicologo,
                @Sesion
            );
            SELECT last_insert_rowid();
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("Edad", Jugador.Edad);
                command.Parameters.AddWithValue("Nombre", Jugador.Nombre);
                command.Parameters.AddWithValue("NombreUsuario", Jugador.NombreUsuario);
                command.Parameters.AddWithValue("Sexo", Jugador.Sexo);
                command.Parameters.AddWithValue("Contrasena", Jugador.Contrasena);
                command.Parameters.AddWithValue("IdTutor", Jugador.IdTutor);
                command.Parameters.AddWithValue("IdPsicologo", Jugador.IdPsicologo);
                command.Parameters.AddWithValue("Sesion", Jugador.Sesion ? 1 : 0);
                command.Prepare();

                Jugador.IdJugador = System.Convert.ToInt32(command.ExecuteScalar());
            }
        }
    }

    public void Actualizar(Jugador Jugador)
    {
        string query = @"
            UPDATE Jugadores
            SET Edad = @Edad,
                Nombre = @Nombre,
                NombreUsuario = @NombreUsuario, 
                Sexo = @Sexo, 
                Contrasena = @Contrasena,
                IdTutor = @IdTutor,
                IdPsicologo = @IdPsicologo,
                Sesion = @Sesion
            WHERE IdJugador = @IdJugador;
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("IdJugador", Jugador.IdJugador);
                command.Parameters.AddWithValue("Edad", Jugador.Edad);
                command.Parameters.AddWithValue("Nombre", Jugador.Nombre);
                command.Parameters.AddWithValue("NombreUsuario", Jugador.NombreUsuario);
                command.Parameters.AddWithValue("Sexo", Jugador.Sexo);
                command.Parameters.AddWithValue("Contrasena", Jugador.Contrasena);
                command.Parameters.AddWithValue("IdTutor", Jugador.IdTutor);
                command.Parameters.AddWithValue("IdPsicologo", Jugador.IdPsicologo);
                command.Parameters.AddWithValue("Sesion", Jugador.Sesion ? 1 : 0);
                command.Prepare();

                command.ExecuteNonQuery();
            }
        }
    }

    public void Eliminar(Jugador Jugador)
    {
        string query = @"
            DELETE
            FROM Jugadores
            WHERE IdJugador = @IdJugador;
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("IdJugador", Jugador.IdJugador);
                command.Prepare();

                command.ExecuteNonQuery();
            }
        }
    }

    public Jugador Buscar(int IdJugador)
    {
        Jugador jugador = null;
        string query = @"
            SELECT IdJugador,
                   Edad, 
                   Nombre,
                   NombreUsuario, 
                   Sexo, 
                   Contrasena,
                   IdTutor,
                   IdPsicologo,
                   Sesion
            FROM Jugadores
            WHERE IdJugador = @IdJugador;
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("IdJugador", IdJugador);
                command.Prepare();

                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    jugador = LeerJugador(reader);
                }
            }
        }

        return jugador;
    }

    public List<Jugador> Lista()
    {
        List<Jugador> jugadores = new List<Jugador>();
        string query = @"
            SELECT IdJugador,
                   Edad, 
                   Nombre,
                   NombreUsuario, 
                   Sexo, 
                   Contrasena,
                   IdTutor,
                   IdPsicologo,
                   Sesion
            FROM Jugadores;
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    jugadores.Add(LeerJugador(reader));
                }
            }
        }

        return jugadores;
    }


    private Jugador LeerJugador(SqliteDataReader reader)
    {
        ColumnReader columnReader = new ColumnReader(reader);

        return new Jugador
        {
            IdJugador = columnReader.GetInt("IdJugador"),
            Edad = columnReader.GetInt("Edad"),
            Nombre = columnReader.GetString("Nombre"),
            NombreUsuario = columnReader.GetString("NombreUsuario"),
            Sexo = columnReader.GetString("Sexo"),
            Contrasena = columnReader.GetString("Contrasena"),
            IdTutor = columnReader.GetInt("IdTutor"),
            IdPsicologo = columnReader.GetInt("IdPsicologo"),
            Sesion = columnReader.GetInt("Sesion") == 1
        };
    }

    public Jugador BuscarJugadorActivo()
    {
        Jugador jugador = null;
        string query = @"
            SELECT IdJugador,
                   Edad, 
                   Nombre,
                   NombreUsuario, 
                   Sexo, 
                   Contrasena,
                   IdTutor,
                   IdPsicologo,
                   Sesion
            FROM Jugadores
            WHERE Sesion=1;
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    jugador = LeerJugador(reader);
                }
            }
        }

        return jugador;
    }
}