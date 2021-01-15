using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicializador : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PsicologoDAO psicologoDAO = new PsicologoDAO();
        List<Psicologo> psicologos = psicologoDAO.Lista();

        if (psicologos.Count > 0)
        {
            TutorDAO tutorDAO = new TutorDAO();
            List<Tutor> tutores = tutorDAO.Lista();

            if (tutores.Count > 0)
            {
                JugadorDAO jugadorDAO = new JugadorDAO();
                Jugador jugador = jugadorDAO.BuscarJugadorActivo();
                if (jugador == null)
                {
                    MotorMicrojuego.AbrirLogin();
                }
            } 
            else
            {
                MotorMicrojuego.AbrirRegistroTutor();
            }
        }
        else
        {
            MotorMicrojuego.AbrirRegistroPsicologo();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
