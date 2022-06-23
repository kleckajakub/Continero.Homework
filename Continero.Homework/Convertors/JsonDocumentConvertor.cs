using System;
using System.Text;
using Newtonsoft.Json;

namespace Continero.Homework.Convertors {
  public class JsonDocumentConvertor : IDocumentConvertor {
    public Document ToDoc(byte[] sourceData) {
      if (sourceData == null || sourceData.Length == 0) {
        throw new ArgumentNullException(nameof(sourceData), "Source data can't be null/empty.");
      }

      var docJson = Encoding.UTF8.GetString(sourceData);

      return JsonConvert.DeserializeObject<Document>(docJson);
    }

    public byte[] ToArray(Document doc) {
      if (doc == null) {
        throw new ArgumentNullException(nameof(doc), "Document can't be null.");
      }

      var docJson = JsonConvert.SerializeObject(doc);

      return Encoding.UTF8.GetBytes(docJson);
    }
  }
}