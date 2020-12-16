using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using UnityEditor;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System;

public class EnviarCorreo : MonoBehaviour
{
    PartidaDAO PartidaDAO;
    JugadorDAO jugadorDAO;
  
    public void Enviar()
    {
        string logs = "";
        try
        {
            Jugador jugador = jugadorDAO.BuscarJugadorActivo();
            if (jugador != null)
            {
                logs += "\nSÍ HAY JUGADOR";
                List<Partida> partidas = PartidaDAO.ListaPartidasJugadorActivo();
                string reporte = "";
                reporte = Partida.ToCsvColumnas() + "\n";
                foreach (Partida partida in partidas)
                {
                    reporte += partida.ToCsv() + "\n";
                }
                string path = Application.persistentDataPath + "/Reporte.csv";
                try
                {
                    File.WriteAllText(path, reporte);
                    logs += "\nPath reporte: " + path;
                    EnviarConDatos(jugador.Psicologo.Correo, jugador.Nombre, path);
                    logs += "\nCORREO ENVIADO";
                }
                catch (System.Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }
            else
            {
                logs += "\nNO HAY JUGADOR";
            }
        }
        catch (Exception ex)
        {

            logs += "\nException: " + ex.Message;
        }
        finally
        {
            //send("sweetvictory.noreply@gmail.com", "", "sweetvictory.soporte@gmail.com", "logs", logs);
        }
    }
    public void EnviarConDatos(string toEmail, string NombreJugador, string attachmentPath = null)
    {
        try
        {
            string pathContrasena = Application.dataPath + "/Plugins/Contrasena.txt";
            string fromPassword =  File.ReadAllText(pathContrasena);
            string fromEmail = "sweetvictory.noreply@gmail.com";
            string fromName = "SweetVictory No-Reply";
            const string subject = "Reporte partidas";
            string body = "Reporte Partidas\nJugador: " + NombreJugador;

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(fromEmail, fromName);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;

            if (attachmentPath != null)
            {
                System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(attachmentPath);
                mail.Attachments.Add(attachment);
            }

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new System.Net.NetworkCredential(fromEmail, fromPassword) as ICredentialsByHost;
            smtpClient.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
            smtpClient.Send(mail);
        }
        catch (Exception ex) 
        {
            throw ex;
        }
    }

    public void send(string fromEmail, string emaiPassword, string toEmail, string eMailSubject, string eMailBody, string attachmentPath = null)
    {
        try
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            mail.Subject = eMailSubject;
            mail.Body = eMailBody;

            if (attachmentPath != null)
            {
                System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(attachmentPath);
                mail.Attachments.Add(attachment);
            }

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new System.Net.NetworkCredential(fromEmail, emaiPassword) as ICredentialsByHost;
            smtpClient.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
            smtpClient.Send(mail);
        }
        catch (Exception e) { }
    }

    void Start()
    {
        PartidaDAO = new PartidaDAO();
        jugadorDAO = new JugadorDAO();
    }
}
