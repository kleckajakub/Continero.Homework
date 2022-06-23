using System;
using System.Text;
using Continero.Homework.Convertors;
using FluentAssertions;
using NUnit.Framework;

namespace Continero.Homework.Tests.Convertors {
  [TestFixture]
  public class JsonDocumentConvertorTests {
    [SetUp]
    public void Setup() {
      convertor = new JsonDocumentConvertor();
    }

    private JsonDocumentConvertor convertor;

    private const string docJson = @"{""Title"":""title"",""Text"":""text""}";

    [Test]
    public void building_document_from_bytes_failed_for_null_bytes() {
      Assert.Throws<ArgumentNullException>(() => convertor.ToDoc(null));
    }

    [Test]
    public void building_document_from_bytes_failed_for_empty_bytes() {
      Assert.Throws<ArgumentNullException>(() => convertor.ToDoc(new byte[0]));
    }

    [Test]
    public void build_document_from_bytes() {
      var doc = convertor.ToDoc(Encoding.UTF8.GetBytes(docJson));

      doc.Title.Should().Be("title");
      doc.Text.Should().Be("text");
    }


    [Test]
    public void serialization_of_document_to_bytes_failed_for_null_document() {
      Assert.Throws<ArgumentNullException>(() => convertor.ToArray(null));
    }

    [Test]
    public void serialize_document_to_bytes() {
      var docBytes = convertor.ToArray(new Document {Title = "title", Text = "text"});

      var expectedBytes = Encoding.UTF8.GetBytes(docJson);

      docBytes.Should().BeEquivalentTo(expectedBytes);
    }
  }
}