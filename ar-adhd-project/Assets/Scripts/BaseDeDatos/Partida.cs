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

    public string ToCsv()
    {
        return string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
               Jugador.Nombre, Nivel, Dificultad, Intentos, Aciertos, Errores,
               Tiempo, FechaTiempo
        );
    }
    public string ToTsv()
    {
        return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}",
               Jugador.Nombre, Nivel, Dificultad, Intentos, Aciertos, Errores,
               Tiempo, FechaTiempo
        );
    }
    public static string ToCsvColumnas()
    {
        return "Nombre Jugador,Nivel,Dificultad,Intentos,Aciertos,Errores,Tiempo,Fecha y Hora";
    }
    public static string ToTsvColumnas()
    {
        return "Nombre Jugador\tNivel\tDificultad\tIntentos\tAciertos\tErrores\tTiempo\tFecha y Hora";
    }
}

public class PartidaDAO
{
    private string CONNECTION_STRING = ServicioBaseDatos.getDatabasePath();

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

    public List<Partida> ListaPartidasJugadorActivo()
    {
        List<Partida> partidas = new List<Partida>();
        string query = @"
            SELECT 
                IdPartida,
                Nivel,
                Dificultad,
                Intentos,
                Aciertos,
                Errores,
                Tiempo,
                FechaTiempo,
                P.IdJugador
            FROM Partidas AS P 
			JOIN Jugadores AS J 
			on P.IdJugador = J.IdJugador
			WHERE J.Sesion = 1
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

    public void CalcularMetricas(float[] Cuartiles)
    {
        Cuartiles[0] = 0;
        Cuartiles[1] = 0;
        Cuartiles[2] = 0;

        List<Partida> partidas = new List<Partida>();
        string query = @"
            SELECT 
				IFNULL((
					SELECT CAST(Aciertos + 1 AS REAL)/CAST(Intentos + 1 AS REAL) AS Q1
					FROM Partidas
					ORDER BY Q1
					LIMIT 1 OFFSET (SELECT 1 * COUNT(*) / 4 FROM Partidas)
				), 0.0) AS DIST_Q1,
				IFNULL((
					SELECT CAST(Aciertos + 1 AS REAL)/CAST(Intentos + 1 AS REAL) AS Q2
					FROM Partidas
					ORDER BY Q2
					LIMIT 1 OFFSET (SELECT 2 * COUNT(*) / 4 FROM Partidas)
				), 0.0) AS DIST_Q2,
				IFNULL((
					SELECT CAST(Aciertos + 1 AS REAL)/CAST(Intentos + 1 AS REAL) AS Q3
					FROM Partidas
					ORDER BY Q3
					LIMIT 1 OFFSET (SELECT 3 * COUNT(*) / 4 FROM Partidas)
				), 0.0) AS DIST_Q3;
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Cuartiles[0] = reader.GetFloat(0);
                    Cuartiles[1] = reader.GetFloat(1);
                    Cuartiles[2] = reader.GetFloat(2);
                }
            }
        }
    }

    public float CalcularMetricaJugador()
    {
        float Promedio = 0;

        List<Partida> partidas = new List<Partida>();
        string query = @"
            SELECT 
	            IFNULL(AVG(CAST(Aciertos + 1 AS REAL)/CAST(Intentos + 1 AS REAL)), 0.0) AS DIST_AVG
            FROM (
	            SELECT Aciertos, Intentos
	            FROM Partidas
	            JOIN Jugadores 
	            ON Jugadores.IdJugador = Partidas.IdJugador
	            WHERE Sesion = 1
	            ORDER BY FechaTiempo DESC
	            LIMIT 3
            );
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Promedio = reader.GetFloat(0);
                }
            }
        }
        return Promedio;
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