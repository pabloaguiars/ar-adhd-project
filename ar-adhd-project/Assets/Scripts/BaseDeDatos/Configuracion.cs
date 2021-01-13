using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Configuracion
{
    public int IdConfiguracion, Sfx, Volumen;
    public int IdJugador;

    public Jugador Jugador
    {
        get => new JugadorDAO().Buscar(IdJugador);
        set => IdJugador = value.IdJugador;
    }

    public override string ToString()
    {
        return string.Format("{0} {1} {2} ({3})", 
            IdConfiguracion, Sfx, Volumen, Jugador.ToString());
    }
}

public class ConfiguracionDAO
{
    private string CONNECTION_STRING = ServicioBaseDatos.getDatabasePath();

    public void Crear(Configuracion Configuracion)
    {
        string query = @"
            INSERT INTO Configuraciones(
                Sfx, 
                Volumen,
                IdJugador
            ) 
            VALUES (
                @Sfx,
                @Volumen, 
                @IdJugador
            );
            SELECT last_insert_rowid();
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("Sfx", Configuracion.Sfx);
                command.Parameters.AddWithValue("Volumen", Configuracion.Volumen);
                command.Parameters.AddWithValue("IdJugador", Configuracion.IdJugador);
                command.Prepare();

                Configuracion.IdConfiguracion = System.Convert.ToInt32(command.ExecuteScalar());
            }
        }
    }

    public void Actualizar(Configuracion Configuracion)
    {
        string query = @"
            UPDATE Configuraciones
            SET Sfx = @Sfx, 
                Volumen = @Volumen,
                IdJugador = @IdJugador
            WHERE IdConfiguracion = @IdConfiguracion;
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("IdConfiguracion", Configuracion.IdConfiguracion);
                command.Parameters.AddWithValue("Sfx", Configuracion.Sfx);
                command.Parameters.AddWithValue("Volumen", Configuracion.Volumen);
                command.Parameters.AddWithValue("IdJugador", Configuracion.IdJugador);
                command.Prepare();

                command.ExecuteNonQuery();
            }
        }
    }

    public void Eliminar(Configuracion Configuracion)
    {
        string query = @"
            DELETE
            FROM Configuraciones
            WHERE IdConfiguracion = @IdConfiguracion;
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("IdConfiguracion", Configuracion.IdConfiguracion);
                command.Prepare();

                command.ExecuteNonQuery();
            }
        }
    }

    public Configuracion Buscar(int IdConfiguracion)
    {
        Configuracion configuracion = null;
        string query = @"
            SELECT IdConfiguracion,
                   Sfx, 
                   Volumen,
                   IdJugador
            FROM Configuraciones
            WHERE IdConfiguracion = @IdConfiguracion;
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("IdConfiguracion", IdConfiguracion);
                command.Prepare();

                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    configuracion = LeerConfiguracion(reader);
                }
            }
        }

        return configuracion;
    }

    public List<Configuracion> Lista()
    {
        List<Configuracion> configuraciones = new List<Configuracion>();
        string query = @"
            SELECT IdConfiguracion,
                   Sfx, 
                   Volumen,
                   IdJugador
            FROM Configuraciones;
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    configuraciones.Add(LeerConfiguracion(reader));
                }
            }
        }

        return configuraciones;
    }


    private Configuracion LeerConfiguracion(SqliteDataReader reader)
    {
        ColumnReader columnReader = new ColumnReader(reader);
        return new Configuracion
        {
            IdConfiguracion = columnReader.GetInt("IdConfiguracion"),
            Sfx = columnReader.GetInt("Sfx"),
            Volumen = columnReader.GetInt("Volumen"),
            IdJugador = columnReader.GetInt("IdJugador")
        };
    }
}