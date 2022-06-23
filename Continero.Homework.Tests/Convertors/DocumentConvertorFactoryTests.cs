using System;
using Continero.Homework.Convertors;
using FluentAssertions;
using NUnit.Framework;

namespace Continero.Homework.Tests.Convertors {
  [TestFixture]
  public class DocumentConvertorFactoryTests {
    private DocumentConvertorFactory convertorFactory;

    [SetUp]
    public void SetUp() {
      convertorFactory = new DocumentConvertorFactory();
    }

    [Test]
    public void create_XmlDocumentConvertor_for_xml_input_file() {
      var docConvertor = convertorFactory.Create("file.xml");

      docConvertor.Should().BeAssignableTo<XmlDocumentConvertor>();
    }

    [Test]
    public void create_JsonDocumentConvertor_for_json_input_file() {
      var docConvertor = convertorFactory.Create("file.json");

      docConvertor.Should().BeAssignableTo<JsonDocumentConvertor>();
    }

    [Test]
    public void creation_failed_for_unsupported_input_file() {
      Assert.Throws<NotSupportedException>(() => convertorFactory.Create("file.bson"));
    }
  }
}