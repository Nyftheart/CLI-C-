using System;
using System.Threading.Tasks;
using PuppeteerSharp;
using GrapeCity.Documents.Pdf.TextMap;

public class PDFConvert
{
    public static async Task PDF()
    {
        Console.Write("Entrez l'URL à convertir en PDF : ");
        string url = Console.ReadLine();

        using var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();
        var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true
        });
        var page = await browser.NewPageAsync();
        await page.GoToAsync(url);

        Console.Write("Entrez le nom de fichier : ");
        string pdfFilePath = Console.ReadLine();

        await page.PdfAsync("/Users/nyftheart/Downloads/" + pdfFilePath +".pdf");

        Console.WriteLine($"Le PDF a été enregistré à l'emplacement : {pdfFilePath}");
    }
}