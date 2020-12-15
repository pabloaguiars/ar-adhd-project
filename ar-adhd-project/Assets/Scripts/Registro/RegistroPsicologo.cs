using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegistroPsicologo : MonoBehaviour
{
    PsicologoDAO PsicologoDAO;

    [SerializeField] InputField nombre;
    [SerializeField] InputField cedula;
    [SerializeField] InputField correo;
    [SerializeField] InputField telefono;

    // Start is called before the first frame update
    void Start()
    {
        PsicologoDAO = new PsicologoDAO();
    }

    public void Crear()
    {
        PsicologoDAO.Crear(new Psicologo { 
            Nombre = nombre.text,
            Cedula = cedula.text,
            Correo = correo.text,
            Telefono = telefono.text,
            Contrasena = "SweetVictory",
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
