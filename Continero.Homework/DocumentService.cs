using Continero.Homework.Convertors;
using Continero.Homework.Storages;

namespace Continero.Homework {
  /// <summary>
  /// Wrapper/helper class for loading, converting and saving documents.
  /// </summary>
  public class DocumentService {
    private readonly IDocumentStorage sourceStorage;
    private readonly IDocumentStorage targetStorage;
    private readonly DocumentConvertorFactory convertorFactory;

    public DocumentService(IDocumentStorage sourceStorage, IDocumentStorage targetStorage, DocumentConvertorFactory convertorFactory) {
      this.sourceStorage = sourceStorage;
      this.targetStorage = targetStorage;
      this.convertorFactory = convertorFactory;
    }

    public void LoadDocumentAndSaveItInNewFormat(string sourceDocName, string targetDocName) {
      var sourceDocData = sourceStorage.Load(sourceDocName);
      var sourceDocConvertor = convertorFactory.Create(sourceDocName);
      var sourceDoc = sourceDocConvertor.ToDoc(sourceDocData);

      var targetConvertor = convertorFactory.Create(targetDocName);
      var targetDocumentData = targetConvertor.ToArray(sourceDoc);
      targetStorage.Save(targetDocumentData, targetDocName);
    }
  }
}