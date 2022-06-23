using System;
using System.IO;
using System.Xml.Serialization;

namespace Continero.Homework.Convertors {
  public class XmlDocumentConvertor : IDocumentConvertor {
    public Document ToDoc(byte[] sourceData) {
      if (sourceData == null || sourceData.Length == 0) {
        throw new ArgumentNullException(nameof(sourceData), "Source data can't be null/empty.");
      }

      var serializer = new XmlSerializer(typeof(Document));

      using (var stream = new MemoryStream(sourceData)) {
        return serializer.Deserialize(stream) as Document;
      }
    }

    public byte[] ToArray(Document doc) {
      if (doc == null) {
        throw new ArgumentNullException(nameof(doc), "Document can't be null.");
      }

      var serializer = new XmlSerializer(typeof(Document));

      using (var stream = new MemoryStream()) {
        serializer.Serialize(stream, doc);

        return stream.ToArray();
      }
    }
  }
}