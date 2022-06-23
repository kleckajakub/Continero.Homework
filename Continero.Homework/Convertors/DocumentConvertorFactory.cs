using System;

namespace Continero.Homework.Convertors {
  public class DocumentConvertorFactory {
    public virtual IDocumentConvertor Create(string docName) {
      if (docName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase)) {
        return new XmlDocumentConvertor();
      }

      if (docName.EndsWith(".json", StringComparison.OrdinalIgnoreCase)) {
        return new JsonDocumentConvertor();
      }


      throw new NotSupportedException($"{docName} is not supported");
    }
  }
}