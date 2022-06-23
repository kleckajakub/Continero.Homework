namespace Continero.Homework.Storages {
  public interface IDocumentStorage {
    byte[] Load(string docName);

    void Save(byte[] docData, string docName);
  }
}