using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UnityEngine;

public class Psicologo
{
    public int IdPsicologo;
    public string Nombre, Telefono, Correo, Cedula, Contrasena;

    public override string ToString()
    {
        return string.Format("{0} {1} {2} {3} {4} {5}", IdPsicologo, Nombre, Telefono, Correo, Cedula, Contrasena);
    }
}

public class PsicologoDAO
{
    private string CONNECTION_STRING = "URI=file:" + Application.dataPath + "/Plugins/BD.db";

    public void Crear(Psicologo Psicologo)
    {
        string query = @"
            INSERT INTO Psicologos(
                Nombre,
                Telefono, 
                Correo, 
                Cedula, 
                Contrasena
            ) 
            VALUES (
                @Nombre,
                @Telefono, 
                @Correo, 
                @Cedula, 
                @Contrasena
            );
            SELECT last_insert_rowid();
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("Nombre", Psicologo.Nombre);
                command.Parameters.AddWithValue("Telefono", Psicologo.Telefono);
                command.Parameters.AddWithValue("Correo", Psicologo.Correo);
                command.Parameters.AddWithValue("Cedula", Psicologo.Cedula);
                command.Parameters.AddWithValue("Contrasena", Psicologo.Contrasena);
                command.Prepare();

                Psicologo.IdPsicologo = System.Convert.ToInt32(command.ExecuteScalar());
            }
        }
    }
    public void Actualizar(Psicologo Psicologo)
    {
        string query = @"
            UPDATE Psicologos
            SET Nombre = @Nombre,
                Telefono = @Telefono,  
                Correo = @Correo, 
                Cedula =  @Cedula, 
                Contrasena = @Contrasena
            WHERE IdPsicologo = @IdPsicologo;
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("IdPsicologo", Psicologo.IdPsicologo);
                command.Parameters.AddWithValue("Nombre", Psicologo.Nombre);
                command.Parameters.AddWithValue("Telefono", Psicologo.Telefono);
                command.Parameters.AddWithValue("Correo", Psicologo.Correo);
                command.Parameters.AddWithValue("Cedula", Psicologo.Cedula);
                command.Parameters.AddWithValue("Contrasena", Psicologo.Contrasena);
                command.Prepare();

                command.ExecuteNonQuery();
            }
        }
    }

    public void Eliminar(Psicologo Psicologo)
    {
        string query = @"
            DELETE
            FROM Psicologos
            WHERE IdPsicologo = @IdPsicologo;
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("IdPsicologo", Psicologo.IdPsicologo);
                command.Prepare();

                command.ExecuteNonQuery();
            }
        }
    }

    public Psicologo Buscar(int IdPsicologo)
    {
        Psicologo psicologo = null;
        string query = @"
            SELECT IdPsicologo,
                   Nombre,
                   Telefono, 
                   Correo, 
                   Cedula, 
                   Contrasena
            FROM Psicologos
            WHERE IdPsicologo = @IdPsicologo;
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("IdPsicologo", IdPsicologo);
                command.Prepare();

                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    psicologo = LeerPsicologo(reader);
                }
            }
        }

        return psicologo;
    }

    public List<Psicologo> Lista()
    {
        List<Psicologo> psicologos = new List<Psicologo>();
        string query = @"
            SELECT IdPsicologo,
                   Nombre,
                   Telefono, 
                   Correo, 
                   Cedula, 
                   Contrasena
            FROM Psicologos;
         ";

        using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
        {
            connection.Open();
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    psicologos.Add(LeerPsicologo(reader));
                }
            }
        }

        return psicologos;
    }


    private Psicologo LeerPsicologo(SqliteDataReader reader)
    {
        ColumnReader columnReader = new ColumnReader(reader);
        return new Psicologo
        {
            IdPsicologo = columnReader.GetInt("IdPsicologo"),
            Nombre = columnReader.GetString("Nombre"),
            Telefono = columnReader.GetString("Telefono"),
            Correo = columnReader.GetString("Correo"),
            Cedula = columnReader.GetString("Cedula"),
            Contrasena = columnReader.GetString("Contrasena")
        };
    }



}