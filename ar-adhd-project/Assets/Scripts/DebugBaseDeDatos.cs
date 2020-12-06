using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBaseDeDatos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PsicologoDAO dao = new PsicologoDAO();
        List<Psicologo> psicologos;

        Debug.Log("Antes: ");
        psicologos= dao.Lista();
        foreach (Psicologo psicologo in psicologos)
        {
            Debug.Log(psicologo.ToString());
            dao.Eliminar(psicologo);
        }

        Debug.Log("Despues: ");
        psicologos = dao.Lista();
        foreach (Psicologo psicologo in psicologos)
        {
            Debug.Log(psicologo.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
