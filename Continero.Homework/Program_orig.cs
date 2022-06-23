using System;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Continero.Homework {
  //1. class should be placed in separate file
  //2. missing [Serialiable] attribute
  [Serializable]
  public class Document {
    public string Title { get; set; }
    public string Text { get; set; }
  }

  internal class Program_orig {
    private static void Main_orig(string[] args) {
      //3. paths should be defined in config/ini files or we can pass them in args
      //4. Environment.CurrentDirectory doesn't work in Windows service
      var sourceFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Source Files\\Document1.xml");
      var targetFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Target Files\\Document1.json");

      try {
        //5. we use only file as a source of data - there is no easy way to use another data source (DB/WebService...)
        //6. FileStream should be created/open in using block - it should be close and dispose asap
        //7. file may not exist - here should be some check and possible user notification
        //8. here is a dependency on file system - it is harder to write unit test
        FileStream sourceStream = File.Open(sourceFileName, FileMode.Open);
        //9. StreamReader should be created in using block - it should be dispose asap
        var reader = new StreamReader(sourceStream);

        //10. input is defined in try..catch block and is not visible for rest of program
        string input = reader.ReadToEnd();
      } catch (Exception ex) {
        //11. if we create and throw new Exception in this way, so we lost stack of the first exception - we have only message of first exception
        //12. if we throw exception here, so program ends with unhandled exception, so user doesn't know, what's happened and rest of code never be invoke
        throw new Exception(ex.Message);
      }

      //13. input could be empty - there should be a check and possible user notification
      //14. here is a dependency on XML parser - there is no easy way to use another parser
      var xdoc = XDocument.Parse(/*input*/"");

      //15. xdoc.Root can be null - possible NullReferenceException
      //16. xdoc.Root.Element("title/text") can be null - possible NullReferenceException
      //17. title and text not corresponding with Title and Text in class Document - this elements are not paired
      var doc = new Document {
        Title = xdoc.Root.Element("title").Value,
        Text = xdoc.Root.Element("text").Value
      };

      //18. here is dependency on JsonConvert - there is no easy way to use another converter
      var serializedDoc = JsonConvert.SerializeObject(doc);

      //19. path of target file may not exists - here should be some check and user notification
      //20. FileStream should be created in using block - it should be close, flush and dispose asap
      //21. output file is always rewrite without user notification
      var targetStream = File.Open(targetFileName, FileMode.Create, FileAccess.Write);

      //22. streamwriter should be created in using block - it should be flush and dispose asap
      var sw = new StreamWriter(targetStream);
      sw.Write(serializedDoc);
    }
  }
}