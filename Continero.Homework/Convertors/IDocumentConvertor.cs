namespace Continero.Homework.Convertors {
  public interface IDocumentConvertor {
    Document ToDoc(byte[] sourceData);
    byte[] ToArray(Document doc);
  }
}