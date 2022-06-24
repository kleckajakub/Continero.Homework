using System;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Continero.Homework {
  //1. třída by měla být umístěna v samostatném souboru
  public class Document {
    public string Title { get; set; }
    public string Text { get; set; }
  }

  internal class Program_orig {
    private static void Main_orig(string[] args) {
      //2. cesta by měla být definována v config/ini souboru nebo vložená přes args
      //3. pokud je aplikace spuštěná jako služba, tak Environment.CurrentDirectory nefunguje/nevrací správnou cestu, vždy vrací c:\Windows\system32
      var sourceFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Source Files\\Document1.xml");
      var targetFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Target Files\\Document1.json");

      try {
        //4. jako úložiště se zde vždy použije soubor - není zde jednoduchá cesta, jak použít jiné úložiště (memory/DB/WebService...)
        //5. FileStream by měl být vytvářen v using bloku - ten zajistí jeho uzavření/flush a uvolnění z paměti (dispose) okamžitě poté, co přestaneme stream používat
        //6. soubor nemusí existovat - měla by tu být kontrola příp. notifikace uživatele
        //7. je zde přímá závislost na souborovém systému - je těžší testovat tuto část kódu v izolaci
        FileStream sourceStream = File.Open(sourceFileName, FileMode.Open);

        //8. StreamReader by měl být vytvářen v using bloku - ten zajistí jeho uzavření/flush a uvolnění z paměti (dispose) okamžitě poté, co přestaneme reader používat
        var reader = new StreamReader(sourceStream);

        //9. proměnná input je definovaná uvnitř bloku try..catch a není vidět pro zbytek programu
        string input = reader.ReadToEnd();
      } catch (Exception ex) {
        //10. pokud vytvoříme a vyhodíme vyjímku tímto stylem, tak ztratíme stack původní vyjímky, do nové vyjímky zkopírujeme pouze zprávu původní vyjímky
        //11. pokud vyhodíme vyjímku zde, tak program skončí nestandardně (neošetřená chyba), uživatel neví, co se stalo a zbytek kódu se nespustí
        throw new Exception(ex.Message);
      }

      //12. proměnná input může být prázdná - měla by tu být kontrola příp. upozornění uživatele
      //13. je zde přímá závislost na XDocument/XML parseru - neexistuje jednoduchá cesta, jak zvolit jiný parser
      var xdoc = XDocument.Parse(/*input*/"");

      //14. xdoc.Root může být null - možná NullReferenceException
      //15. xdoc.Root.Element("title/text") může být null - možná NullReferenceException
      //16. title a text neodpovídají jménům proměnných v třídě Document (tyto elementy se nenapárují)
      var doc = new Document {
        Title = xdoc.Root.Element("title").Value,
        Text = xdoc.Root.Element("text").Value
      };

      //17. je zde přímá závislost na JsonConvert - neexistuje jednoduchá cesta, jak zvolit jiný parser
      var serializedDoc = JsonConvert.SerializeObject(doc);

      //18. soubor nemusí existovat - měla by tu být kontrola popř. notifikace uživatele
      //19. FileStream by měl být vytvářen v using bloku - ten zajistí jeho uzavření/flush a uvolnění z paměti (dispose) okamžitě poté, co přestaneme stream používat 
      //20. výstupní soubor/dokument je vždy přepsán bez upozornění uživatele (to je spíš vlastnost než chyba - záleží na kontextu)
      var targetStream = File.Open(targetFileName, FileMode.Create, FileAccess.Write);

      //21. streamwriter by měl být vytvářen v using bloku - ten zajistí jeho uzavření/flush a uvolnění z paměti (dispose) okamžitě poté, co přestaneme stream používat 
      var sw = new StreamWriter(targetStream);
      sw.Write(serializedDoc);
    }
  }
}