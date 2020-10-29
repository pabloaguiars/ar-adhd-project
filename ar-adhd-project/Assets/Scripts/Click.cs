using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Click : MonoBehaviour
{
    public GameObject TextBox,message;
    string nombre;
    private float posicionX, posicionY;
    private float[] posicionZ = new float[6];

    // Start is called before the first frame update
    void Start()
    {
        posicionX = Random.Range(3f, 6f);
        posicionY = Random.Range(0f, 2f);
        posicionZ[0] = 5;
        posicionZ[1] = 6;
        posicionZ[2] = 7;
        posicionZ[3] = -5;
        posicionZ[4] = -6;
        posicionZ[5] = -7;

        nombre = gameObject.name;
        transform.position = new Vector3(Random.Range(-posicionX,posicionX), Random.Range(-posicionY,posicionY), posicionZ[Random.Range(0,5)]);
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
