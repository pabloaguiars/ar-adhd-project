using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Click : MonoBehaviour
{
    public GameObject TextBox,message;
    string nombre;
    // Start is called before the first frame update
    void Start()
    {
        nombre = gameObject.name;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }

    public void OnMouseDown()
    {
        if (nombre == TextBox.GetComponent<Text>().text)
        {
            message.GetComponent<Text>().text = "Bien hecho";
            Destroy(gameObject);
        }
        else
        {
            message.GetComponent<Text>().text = "Uff cerca!";
        }
    }
}
