using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBaseDeDatos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ConfiguracionDAO dao = new ConfiguracionDAO();
        Configuracion configuracion = new Configuracion
        {
            Sfx = 1,
            Volumen = 2,
            IdJugador = 5,
        };
        dao.Crear(configuracion);

        configuracion = dao.Buscar(configuracion.IdConfiguracion);
        Debug.Log(configuracion);

        configuracion.Sfx = 300;
        dao.Actualizar(configuracion);

        configuracion = dao.Buscar(configuracion.IdConfiguracion);
        Debug.Log(configuracion);


        List<Configuracion> configuraciones;

        Debug.Log("Antes: ");
        configuraciones = dao.Lista();
        foreach (Configuracion configures2 in configuraciones)
        {
            Debug.Log(configures2.ToString());
            dao.Eliminar(configures2);
        }

        Debug.Log("Despues: ");
        configuraciones = dao.Lista();
        foreach (Configuracion configures2 in configuraciones)
        {
            Debug.Log(configures2.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
