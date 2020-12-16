﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegistroTutor : MonoBehaviour
{
    TutorDAO TutorDAO;

    [SerializeField] InputField nombre;
    [SerializeField] InputField correo;
    [SerializeField] InputField telefono;

    // Start is called before the first frame update
    void Start()
    {
        TutorDAO = new TutorDAO();
    }

    public void Crear()
    {
        TutorDAO.Crear(new Tutor
        {
            Nombre = nombre.text,
            Correo = correo.text,
            Telefono = telefono.text,
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}