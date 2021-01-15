using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{

    [SerializeField] InputField NombreUsuario;
    [SerializeField] InputField Contrasena;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Autenticar()
    {
        JugadorDAO jugadorDAO = new JugadorDAO();
        List<Jugador> jugadores = jugadorDAO.Lista();
        foreach (Jugador jugador in jugadores)
        {
            if (jugador.NombreUsuario == NombreUsuario.text &&
                jugador.Contrasena == Contrasena.text)
            {
                jugador.Sesion = true;
                jugadorDAO.Actualizar(jugador);
                MotorMicrojuego.MenuPrincipal();
            }
        }

    }
}
