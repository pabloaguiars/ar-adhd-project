using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegistroJugador : MonoBehaviour
{
    JugadorDAO JugadorDAO;
    PsicologoDAO PsicologoDAO;
    TutorDAO TutorDAO;
    private static Jugador Jugador;

    [SerializeField] InputField nombre;
    [SerializeField] InputField nombreUsuario;
    [SerializeField] InputField edad;
    [SerializeField] Dropdown genero;
    [SerializeField] InputField contrasena;
    [SerializeField] InputField confirmarContrasena;
    [SerializeField] Button buttonInicioSesion;

    [SerializeField] Dropdown tutor;

    // Start is called before the first frame update
    void Start()
    {
        JugadorDAO = new JugadorDAO();
        PsicologoDAO = new PsicologoDAO();
        TutorDAO = new TutorDAO();


        if (!MotorMicrojuego.DesdeIniciarSesion)
        {
            Destroy(buttonInicioSesion.gameObject);
        }
    }

    public void Nuevo()
    {

        if (contrasena.text == confirmarContrasena.text) 
        { 
            List<Psicologo> Psicologos = PsicologoDAO.Lista();
            Psicologo Psicologo = Psicologos[0];

            List<Tutor> Tutores = TutorDAO.Lista();
            Tutor Tutor = Tutores[0];

            Jugador = new Jugador() { 
                Nombre = nombre.text,
                NombreUsuario = nombreUsuario.text,
                Sexo = genero.options[genero.value].text,
                Edad = int.Parse(edad.text),
                Contrasena = contrasena.text,
                Psicologo = Psicologo,
                Tutor = Tutor
            };

            JugadorDAO.Crear(Jugador);
            MotorMicrojuego.MenuPrincipal();
        }
    }

    public void EliminarTutores()
    {
        if (MotorMicrojuego.DesdeIniciarSesion)
        {
            MotorMicrojuego.AbrirLogin();
        }
        else
        {
            TutorDAO tutorDAO = new TutorDAO();
            List<Tutor> tutores = tutorDAO.Lista();
            foreach (Tutor tutor in tutores)
            {
                tutorDAO.Eliminar(tutor);
            }
            MotorMicrojuego.AbrirRegistroTutor();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
