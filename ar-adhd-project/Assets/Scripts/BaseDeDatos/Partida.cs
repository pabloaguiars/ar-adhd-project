using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Partida
{
    public int IdPartida, Nivel, Dificultad, Intentos, Aciertos, Errores, Tiempo;
    public DateTime FechaTiempo;
    public int IdJugador;

    public Jugador Jugador
    {
        get => new JugadorDAO().Buscar(IdJugador);
        set => IdJugador = value.IdJugador;
    }

    public override string ToString()
    {
        return string.Format("{0} {1} {2} {3} {4} {5} {6} {7} ({8})",
               IdPartida, Nivel, Dificultad, Intentos, Aciertos, Errores,
               Tiempo, FechaTiempo, Jugador.ToString()
        );
    }

}

public class PartidaDAO
{
    private string CONNECTION_STRING = "URI=file:" + Application.dataPath + "/Plugins/BD.db";

    public void Crear(Partida Partida)
    {
        string query = @"
            INSERT INTO Partidas(
                Nivel,
                Dificultad, 
                Intentos, 
                Aciertos,
                Errores,
                Tiempo,
                FechaTiempo,
                IdJugador
            ) 
            VALUES (
                @Nivel,
                @Dificultad, 
                @Intentos, 
                @Aciertos,
                @Errores,
                @Tiempo,
                @FechaTiempo,
                @IdJugador
            );
            SELECT last_insert_rowid();
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("Nivel", Partida.Nivel);
                command.Parameters.AddWithValue("Dificultad", Partida.Dificultad);
                command.Parameters.AddWithValue("Intentos", Partida.Intentos);
                command.Parameters.AddWithValue("Aciertos", Partida.Aciertos);
                command.Parameters.AddWithValue("Errores", Partida.Errores);
                command.Parameters.AddWithValue("Tiempo", Partida.Tiempo);
                command.Parameters.AddWithValue("FechaTiempo", Partida.FechaTiempo.ToString("o"));
                command.Parameters.AddWithValue("IdJugador", Partida.IdJugador);
                command.Prepare();

                Partida.IdPartida = System.Convert.ToInt32(command.ExecuteScalar());
            }
        }
    }
    public void Actualizar(Partida Partida)
    {
        string query = @"
            UPDATE Partidas
            SET Nivel = @Nivel,
                Dificultad = @Dificultad, 
                Intentos = @Intentos, 
                Aciertos = @Aciertos,
                Errores = @Errores,
                Tiempo = @Tiempo,
                FechaTiempo = @FechaTiempo,
                IdJugador = @IdJugador
            WHERE IdPartida = @IdPartida;
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("IdPartida", Partida.IdPartida);
                command.Parameters.AddWithValue("Nivel", Partida.Nivel);
                command.Parameters.AddWithValue("Dificultad", Partida.Dificultad);
                command.Parameters.AddWithValue("Intentos", Partida.Intentos);
                command.Parameters.AddWithValue("Aciertos", Partida.Aciertos);
                command.Parameters.AddWithValue("Errores", Partida.Errores);
                command.Parameters.AddWithValue("Tiempo", Partida.Tiempo);
                command.Parameters.AddWithValue("FechaTiempo", Partida.FechaTiempo.ToString("o"));
                command.Parameters.AddWithValue("IdJugador", Partida.IdJugador);
                command.Prepare();

                command.ExecuteNonQuery();
            }
        }
    }

    public void Eliminar(Partida Partida)
    {
        string query = @"
            DELETE
            FROM Partidas
            WHERE IdPartida = @IdPartida;
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("IdPartida", Partida.IdPartida);
                command.Prepare();

                command.ExecuteNonQuery();
            }
        }
    }

    public Partida Buscar(int IdPartida)
    {
        Partida partida = null;
        string query = @"
            SELECT IdPartida,       
                   Nivel,
                   Dificultad, 
                   Intentos, 
                   Aciertos,
                   Errores,
                   Tiempo,
                   FechaTiempo,
                   IdJugador
            FROM Partidas
            WHERE IdPartida = @IdPartida;
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("IdPartida", IdPartida);
                command.Prepare();

                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    partida = LeerPartida(reader);
                }
            }
        }

        return partida;
    }

    public List<Partida> Lista()
    {
        List<Partida> partidas = new List<Partida>();
        string query = @"
            SELECT IdPartida,      
                   Nivel,
                   Dificultad, 
                   Intentos, 
                   Aciertos,
                   Errores,
                   Tiempo,
                   FechaTiempo,
                   IdJugador
            FROM Partidas
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    partidas.Add(LeerPartida(reader));
                }
            }
        }

        return partidas;
    }


    private Partida LeerPartida(SqliteDataReader reader)
    {
        ColumnReader columnReader = new ColumnReader(reader);

        return new Partida
        {
            IdPartida = columnReader.GetInt("IdPartida"),
            Nivel = columnReader.GetInt("Nivel"),
            Dificultad = columnReader.GetInt("Dificultad"),
            Intentos = columnReader.GetInt("Intentos"),
            Aciertos = columnReader.GetInt("Aciertos"),
            Errores = columnReader.GetInt("Errores"),
            Tiempo = columnReader.GetInt("Tiempo"),
            FechaTiempo = DateTime.Parse(columnReader.GetString("FechaTiempo")),
            IdJugador = columnReader.GetInt("IdJugador")
        };
    }
}