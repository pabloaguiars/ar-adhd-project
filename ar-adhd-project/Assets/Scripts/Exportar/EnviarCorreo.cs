using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using UnityEditor;


public class EnviarCorreo : MonoBehaviour
{
    PartidaDAO PartidaDAO;
    JugadorDAO jugadorDAO;
  
    public void Enviar()
    {
        Jugador jugador = jugadorDAO.BuscarJugadorActivo();
        if (jugador!=null)
        {
            
            List<Partida> partidas = PartidaDAO.ListaPartidasJugadorActivo();
            string reporte = "";
            reporte = Partida.ToCsvColumnas()+ "\n";
            foreach (Partida partida in partidas)
            {
                reporte += partida.ToCsv() + "\n";
            }
            string path = "Assets/Resources/Reporte.csv";
            string pathContrasena = "Assets/Resources/Contrasena.txt";
            try
            {
                File.WriteAllText(path, reporte);

                var fromAddress = new MailAddress("sweetvictory.noreply@gmail.com", "SweetVictory No-Reply");
                var toAddress = new MailAddress(jugador.Psicologo.Correo, "SweetVictory Support");
                string fromPassword = File.ReadAllText(pathContrasena);
                const string subject = "Reporte partidas";
                string body = "Reporte Partidas\nJugador: " + jugador.Nombre;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    message.Attachments.Add(new Attachment(path));
                    smtp.Send(message);
                }
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
    }


    void Start()
    {
        PartidaDAO = new PartidaDAO();
        jugadorDAO = new JugadorDAO();
    }
}
