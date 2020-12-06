using Mono.Data.Sqlite;
using System.Collections.Generic;
using UnityEngine;

public class Tutor
{
    public int IdTutor;
    public string Nombre, Telefono, Correo;

    public override string ToString()
    {
        return string.Format("{0} {1} {2} {3}", IdTutor, Nombre, Telefono, Correo);
    }
}

public class TutorDAO
{
    private string CONNECTION_STRING = "URI=file:" + Application.dataPath + "/Plugins/BD.db";

    public void Crear(Tutor Tutor)
    {
        string query = @"
            INSERT INTO Tutores(
                Nombre,
                Telefono, 
                Correo
            ) 
            VALUES (
                @Nombre,
                @Telefono, 
                @Correo
            );
            SELECT last_insert_rowid();
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("Nombre", Tutor.Nombre);
                command.Parameters.AddWithValue("Telefono", Tutor.Telefono);
                command.Parameters.AddWithValue("Correo", Tutor.Correo);
                command.Prepare();

                Tutor.IdTutor = System.Convert.ToInt32(command.ExecuteScalar());
            }
        }
    }

    public void Actualizar(Tutor Tutor)
    {
        string query = @"
            UPDATE Tutores
            SET Nombre = @Nombre,
                Telefono = @Telefono,  
                Correo = @Correo
            WHERE IdTutor = @IdTutor;
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("IdTutor", Tutor.IdTutor);
                command.Parameters.AddWithValue("Nombre", Tutor.Nombre);
                command.Parameters.AddWithValue("Telefono", Tutor.Telefono);
                command.Parameters.AddWithValue("Correo", Tutor.Correo);
                command.Prepare();

                command.ExecuteNonQuery();
            }
        }
    }

    public void Eliminar(Tutor Tutor)
    {
        string query = @"
            DELETE
            FROM Tutores
            WHERE IdTutor = @IdTutor;
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("IdTutor", Tutor.IdTutor);
                command.Prepare();

                command.ExecuteNonQuery();
            }
        }
    }

    public Tutor Buscar(int IdTutor)
    {
        Tutor tutor = null;
        string query = @"
            SELECT IdTutor,
                   Nombre,
                   Telefono, 
                   Correo
            FROM Tutores
            WHERE IdTutor = @IdTutor;
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("IdTutor", IdTutor);
                command.Prepare();

                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    tutor = LeerTutor(reader);
                }
            }
        }

        return tutor;
    }

    public List<Tutor> Lista()
    {
        List<Tutor> tutores = new List<Tutor>();
        string query = @"
            SELECT IdTutor,
                   Nombre,
                   Telefono, 
                   Correo
            FROM Tutores;
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    tutores.Add(LeerTutor(reader));
                }
            }
        }

        return tutores;
    }


    private Tutor LeerTutor(SqliteDataReader reader)
    {
        ColumnReader columnReader = new ColumnReader(reader);
        return new Tutor
        {
            IdTutor = columnReader.GetInt("IdTutor"),
            Nombre = columnReader.GetString("Nombre"),
            Telefono = columnReader.GetString("Telefono"),
            Correo = columnReader.GetString("Correo")
        };
    }
}