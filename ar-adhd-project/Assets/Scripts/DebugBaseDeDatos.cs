using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBaseDeDatos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TutorDAO tutorDAO = new TutorDAO();
        PsicologoDAO psicologoDAO = new PsicologoDAO();
        JugadorDAO jugadorDAO = new JugadorDAO();
        if (psicologoDAO.Lista().Count<1)
        {
            Psicologo psicologo = new Psicologo()
            {
                Nombre = "Psicologo",
                Correo = "sweetvictory.soporte@gmail.com",
                Telefono = "0123456789",
                Cedula = "cedula",
                Contrasena = "contrasena"
            };

            psicologoDAO.Crear(psicologo);
        }

        if (tutorDAO.Lista().Count<1)
        {
            Tutor tutor = new Tutor()
            {
                Nombre = "Tutor",
                Correo = "tutor@correo.com",
                Telefono = "0123456789"
            };
            tutorDAO.Crear(tutor);
        }

        if (jugadorDAO.Lista().Count<1)
        {
            Jugador jugador = new Jugador()
            {
                Nombre = "Jugador",
                NombreUsuario = "Jugador1",
                Contrasena = "contrasena",
                Edad = 12,
                Sexo = "M",
                IdTutor = 1,
                IdPsicologo = 1,
                Sesion=true
            };
            jugadorDAO.Crear(jugador);
        }

        //ConfiguracionDAO dao = new ConfiguracionDAO();
        //Configuracion configuracion = new Configuracion
        //{
        //    Sfx = 1,
        //    Volumen = 2,
        //    IdJugador = 5,
        //};
        //dao.Crear(configuracion);

        //configuracion = dao.Buscar(configuracion.IdConfiguracion);
        //Debug.Log(configuracion);

        //configuracion.Sfx = 300;
        //dao.Actualizar(configuracion);

        //configuracion = dao.Buscar(configuracion.IdConfiguracion);
        //Debug.Log(configuracion);


        //List<Configuracion> configuraciones;

        //Debug.Log("Antes: ");
        //configuraciones = dao.Lista();
        //foreach (Configuracion configures2 in configuraciones)
        //{
        //    Debug.Log(configures2.ToString());
        //    dao.Eliminar(configures2);
        //}

        //Debug.Log("Despues: ");
        //configuraciones = dao.Lista();
        //foreach (Configuracion configures2 in configuraciones)
        //{
        //    Debug.Log(configures2.ToString());
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
