using System;
using Castle.Windsor;

namespace Continero.Homework.Storages {
  public class DocumentStorageFactory {
    private readonly IWindsorContainer container;

    public DocumentStorageFactory(IWindsorContainer container) {
      this.container = container;
    }

    public virtual IDocumentStorage Create(DocumentStorageType storageType) {
      switch (storageType) {
        case DocumentStorageType.File:
          return container.Resolve<FileDocumentStorage>();

        case DocumentStorageType.InMemory:
          return new InMemoryDocumentStorage();

        default:
          throw new NotSupportedException($"Type {storageType} is not supported.");
      }
    }
  }
}