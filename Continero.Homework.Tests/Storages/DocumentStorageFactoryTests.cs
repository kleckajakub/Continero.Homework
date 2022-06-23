using System;
using Castle.Windsor;
using Continero.Homework.Installers;
using Continero.Homework.Storages;
using FluentAssertions;
using NUnit.Framework;

namespace Continero.Homework.Tests.Storages {
  [TestFixture]
  public class DocumentStorageFactoryTests {
    private DocumentStorageFactory storageFactory;

    [SetUp]
    public void SetUp() {
      var container = new WindsorContainer();
      container.Install(new ServicesInstaller());
      storageFactory = new DocumentStorageFactory(container);
    }

    [Test]
    public void create_file_storage_if_is_choose_file_storage_type() {
      var storage = storageFactory.Create(DocumentStorageType.File);
      storage.Should().BeAssignableTo<FileDocumentStorage>();
    }

    [Test]
    public void create_in_memory_storage_if_is_choose_in_memory_storage_type() {
      var storage = storageFactory.Create(DocumentStorageType.InMemory);
      storage.Should().BeAssignableTo<InMemoryDocumentStorage>();
    }

    [Test]
    public void creation_failed_for_unsupported_storage_type() {
      Assert.Throws<NotSupportedException>(() => storageFactory.Create((DocumentStorageType) 255));
    }
  }
}