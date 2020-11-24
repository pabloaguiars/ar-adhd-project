using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeleccionaObjeto : MonoBehaviour
{
    public GameObject Objetivo;
    public int numero;
    string[] figuras = new string[4];

    // Start is called before the first frame update
    void Start()
    {
        //Asignación de valores al arreglo de figuras
        figuras[0] = "Candy";
        figuras[1] = "Chocolate";
        figuras[2] = "Paleta";
        //numero = Random.Range(0, 3);
        Objetivo.GetComponent<Text>().text = figuras[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
