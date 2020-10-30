using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeleccionaObjeto : MonoBehaviour
{
    public GameObject TextBox;
    public int numero;
    string[] figuras = new string[4];

    // Start is called before the first frame update
    void Start()
    {
        figuras[0] = "Cubo";
        figuras[1] = "Esfera";
        figuras[2] = "Paleta";
        numero = Random.Range(0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        TextBox.GetComponent<Text>().text = figuras[numero];
    }
}
