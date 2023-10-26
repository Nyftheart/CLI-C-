using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace mysendemail
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Envoyer un e-mail");
                Console.WriteLine("2. Mettre à jour les données de l'e-mail");
                Console.WriteLine("3. Convert PDF");
                Console.WriteLine("4. Quitter");
                Console.Write("Sélectionnez une option (1, 2, 3 ou 4): ");

                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        await EnvoyerRequeteAPI();
                        break;
                    case "2":
                        await MettreAJourEmailViaAPI();
                        break;
                    case "3":
                        await PDFConvert.PDF();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Option invalide. Veuillez réessayer.");
                        break;
                }
            }
        }

        static async Task EnvoyerRequeteAPI()
        {
            try
            {
                Mail.EnvoyerEmailAvecDonnees();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }

        static async Task MettreAJourEmailViaAPI()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    string apiUpdateUrl = "https://nyftheartcli.alexandrea1-kramer.workers.dev/email";

                    Console.Write("Entrez l'adresse e-mail du destinataire : ");
                    string destinataire = Console.ReadLine();

                    Console.Write("Entrez le sujet de l'e-mail : ");
                    string sujet = Console.ReadLine();

                    Console.Write("Entrez le contenu de l'e-mail : ");
                    string contenu = Console.ReadLine();

                    // Créez un objet EmailData avec les valeurs saisies par l'utilisateur
                    var updateEmailData = new EmailData
                    {
                        destinataire = destinataire,
                        sujet = sujet,
                        corps = contenu
                    };

                    // Envoi de la requête POST avec les données JSON
                    var updateResponse = await httpClient.PostAsJsonAsync(apiUpdateUrl, updateEmailData);

                    if (updateResponse.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Données de l'e-mail mises à jour avec succès !");
                    }
                    else
                    {
                        Console.WriteLine("Erreur lors de la mise à jour des données de l'e-mail : " + updateResponse.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur : " + ex.Message);
                }
            }
        }
    }

    public class EmailData
    {
        public string destinataire { get; set; }
        public string sujet { get; set; }
        public string corps { get; set; }
    }
}
