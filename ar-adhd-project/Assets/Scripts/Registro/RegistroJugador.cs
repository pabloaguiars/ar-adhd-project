using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegistroJugador : MonoBehaviour
{
    JugadorDAO JugadorDAO;
    PsicologoDAO PsicologoDAO;
    private static Jugador Jugador;

    [SerializeField] InputField nombre;
    [SerializeField] InputField nombreUsuario;
    [SerializeField] InputField edad;
    [SerializeField] Dropdown genero;
    [SerializeField] InputField contrasena;
    [SerializeField] InputField confirmarContrasena;

    [SerializeField] Dropdown tutor;

    // Start is called before the first frame update
    void Start()
    {
        JugadorDAO = new JugadorDAO();
        PsicologoDAO = new PsicologoDAO();
    }

    public void Nuevo()
    {
        List<Psicologo> Psicologos = PsicologoDAO.Lista();
        Psicologo Psicologo = Psicologos[0];

        Jugador = new Jugador() { 
            Nombre = nombre.text,
            NombreUsuario = nombreUsuario.text,
            Sexo = genero.options[genero.value].text,
            Edad = int.Parse(edad.text),
            Contrasena = contrasena.text,
            Psicologo = Psicologo
        };
    }

    public void Crear()
    {
        if (Jugador != null)
        {
            Jugador.IdTutor = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
