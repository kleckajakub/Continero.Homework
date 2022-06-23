using System;
using Continero.Homework.Storages;
using FluentAssertions;
using NUnit.Framework;

namespace Continero.Homework.Tests.Storages {
  [TestFixture]
  public class InMemoryDocumentStorageTests {
    private InMemoryDocumentStorage storage;

    [SetUp]
    public void SetUp() {
      storage = new InMemoryDocumentStorage();
    }

    [Test]
    public void loading_failed_if_doc_not_exists() {
      Assert.Throws<Exception>(() => storage.Load("unknonwDocument.xml"));
    }

    [Test]
    public void save_failed_for_null_file_bytes() {
      Assert.Throws<ArgumentNullException>(() => storage.Save(null, "file.xml"));
    }

    [Test]
    public void save_failed_for_null_file_name() {
      Assert.Throws<ArgumentNullException>(() => storage.Save(new byte[3], null));
    }

    [Test]
    public void save_failed_for_empty_file_name() {
      Assert.Throws<ArgumentNullException>(() => storage.Save(new byte[3], string.Empty));
    }

    [Test]
    public void save_doc_bytes_to_memory() {
      var docBytes = new byte[] {0x01, 0x02, 0x03};
      storage.Save(docBytes, "file.xml");

      var loadedBytes = storage.Load("file.xml");

      loadedBytes.Should().BeEquivalentTo(docBytes);
    }
  }
}