using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reloj : MonoBehaviour
{
    [Tooltip("Tiempo inicial en segundos")]
    public int tiempoinicial;

    [Tooltip("Escala del tiempo del reloj")]
    [Range(-10.0f,10.0f)]
    public float escalaTiempo = 1;

    private Text Crono;
    private float tiempoDelFrameConTimeScale = 0f;
    private float tiempoAMostrarEnSegundos = 0f;
    private float tiempoPausa, tiempoInicial, finTiempoNivel;
    private bool estadoPausa = false;

    // Start is called before the first frame update
    void Start()
    {
        //Establecer la escala de tiempo Original
        tiempoInicial = tiempoDelFrameConTimeScale;

        //Obtiene el componente de texto
        Crono = GetComponent<Text>();

        //Inicializamos la variable que acumula los tiempos de cada frame con el tiempo inicial
        tiempoAMostrarEnSegundos = tiempoInicial;

        actualizaReloj(tiempoInicial);
    }

    // Update is called once per frame
    void Update()
    {
        if(!estadoPausa)
        {
            //Representa el tiempo de cada frame considerando la escala de tiempo
            tiempoDelFrameConTimeScale = Time.deltaTime * escalaTiempo;

            //Acumula el tiempo transcurrido para luego mostrarlo en el juego
            tiempoAMostrarEnSegundos += tiempoDelFrameConTimeScale;
            actualizaReloj(tiempoAMostrarEnSegundos);
        }
    }

    void actualizaReloj(float tiempo)
    {
        int minutos = 0;
        int segundos = 0;
        string textoReloj;

        //Asesgura que el tiempo no sea negativo
        if (tiempo < 0)
            tiempo = 0;

        //Calcula minutos y segundos
        minutos = (int)tiempo / 60;
        segundos = (int)tiempo % 60;

        //Crea la cadena de caracteres con 2 digitos para los minutos y segundos
        textoReloj = minutos.ToString("00") + ":" + segundos.ToString("00");

        //Actualiza el elemento de text de UI con cadenas de caracteres
        Crono.text = textoReloj;

        if (Crono.text == "00:10")
        {
            estadoPausa = true;
            finTiempoNivel = escalaTiempo;
            escalaTiempo = 0;
        }
    }

    //public void Pausar()
    //{
    //    if(!estadoPausa)
    //    {
    //        estadoPausa = true;
    //        tiempoPausa = escalaTiempo;
    //        escalaTiempo = 0;
    //    }
    //}

    //public void Continuar()
    //{
    //    if(estadoPausa)
    //    {
    //        estadoPausa = false;
    //        escalaTiempo = tiempoPausa;
    //    }
    //}

    //public void Reiniciar()
    //{
    //    estadoPausa = false;
    //    escalaTiempo = tiempoInicial;
    //    tiempoAMostrarEnSegundos = tiempoInicial;
    //    actualizaReloj(tiempoAMostrarEnSegundos);

    //}
}
