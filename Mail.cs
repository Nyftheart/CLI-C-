using System;
using EASendMail;

namespace mysendemail
{
    public class Mail
    {
        public static async void EnvoyerEmailAvecDonnees()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    string apiUrl = "https://nyftheartcli.alexandrea1-kramer.workers.dev/email";

                    // Envoi de la requête GET pour récupérer les données de l'e-mail
                    var response = await httpClient.GetStringAsync(apiUrl);

                    // Désérialisez les données de la réponse JSON
                    var emailData = Newtonsoft.Json.JsonConvert.DeserializeObject<EmailData>(response);

                    string destinataire = emailData.destinataire;

                    string sujet = emailData.sujet;

                    string corps = emailData.corps;

                    SmtpMail oMail = new SmtpMail("TryIt");

                    oMail.From = "email";
                    oMail.To = destinataire;
                    oMail.Subject = sujet;
                    oMail.TextBody = corps;

                    SmtpServer oServer = new SmtpServer("smtp.gmail.com");
                    oServer.User = "email";
                    oServer.Password = "codepass";
                    oServer.Port = 465;
                    oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                    Console.WriteLine("start to send email over SSL ...");

                    SmtpClient oSmtp = new SmtpClient();
                    oSmtp.SendMail(oServer, oMail);

                    Console.WriteLine("email was sent successfully!");
                }
                catch (Exception ep)
                {
                    Console.WriteLine("failed to send email with the following error:");
                    Console.WriteLine(ep.Message);
                }
            }
        }
    }
}