using Continero.Homework.Convertors;
using Continero.Homework.Storages;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Continero.Homework.Tests {
  [TestFixture]
  public class DocumentServiceTests {
    [Test]
    public void can_load_document_from_one_storage_in_one_format_and_save_it_to_another_storage_in_second_format() {
      var expectedTargetBytes = new byte[] {0x01, 0x02, 0x03};

      var storage1 = new InMemoryDocumentStorage();
      var storage2 = new InMemoryDocumentStorage();
      var sourceConvertor = new DocumentConvertorMock {DocumentBytes = new byte[3]};
      var targetConvertor = new DocumentConvertorMock {DocumentBytes = expectedTargetBytes};
      var convertorFactory = Substitute.For<DocumentConvertorFactory>();
      
      storage1.Save(sourceConvertor.DocumentBytes, "sourceDoc.xml");
      convertorFactory.Create("sourceDoc.xml").Returns(sourceConvertor);
      convertorFactory.Create("targetDoc.json").Returns(targetConvertor);

      var documentService = new DocumentDownloader(storage1, storage2, convertorFactory);

      documentService.DownloadDocument("sourceDoc.xml", "targetDoc.json");

      var targetDocBytes = storage2.Load("targetDoc.json");
      targetDocBytes.Should().BeEquivalentTo(expectedTargetBytes);
    }

    private class DocumentConvertorMock : IDocumentConvertor {
      private readonly Document document = new Document();

      public byte[] DocumentBytes {get;set;}

      public Document ToDoc(byte[] sourceData) {
        return document;
      }

      public byte[] ToArray(Document doc) {
        return DocumentBytes;
      }
    }
  }
}