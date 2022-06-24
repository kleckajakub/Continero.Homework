using Continero.Homework.Convertors;
using Continero.Homework.Storages;

namespace Continero.Homework {
  /// <summary>
  /// Pomocná třída pro načtení/zkonvertování a uložení dokumentů.
  /// Slepí vše dohromady - stažení surových dat doc, převedení na doc, převedení na nový formát a uložení do nového úložiště
  /// Náhrada původního kódu v souboru Program_orig.cs
  /// </summary>
  public class DocumentDownloader : IDocumentDownloader {
    private readonly IDocumentStorage sourceStorage;
    private readonly IDocumentStorage targetStorage;
    private readonly DocumentConvertorFactory convertorFactory;

    public DocumentDownloader(IDocumentStorage sourceStorage, IDocumentStorage targetStorage, DocumentConvertorFactory convertorFactory) {
      this.sourceStorage = sourceStorage;
      this.targetStorage = targetStorage;
      this.convertorFactory = convertorFactory;
    }

    public void DownloadDocument(string sourceDocName, string targetDocName) {
      var sourceDocData = sourceStorage.Load(sourceDocName);
      var sourceDocConvertor = convertorFactory.Create(sourceDocName);
      var sourceDoc = sourceDocConvertor.ToDoc(sourceDocData);

      var targetConvertor = convertorFactory.Create(targetDocName);
      var targetDocumentData = targetConvertor.ToArray(sourceDoc);
      targetStorage.Save(targetDocumentData, targetDocName);
    }
  }
}