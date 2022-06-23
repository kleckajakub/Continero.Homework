using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Continero.Homework.Config;
using Continero.Homework.Convertors;
using Continero.Homework.IO;
using Continero.Homework.Storages;

namespace Continero.Homework.Installers {
  public class ServicesInstaller : IWindsorInstaller {
    public void Install(IWindsorContainer container, IConfigurationStore store) {
      container.Register(
        Component.For<IWindsorContainer>().Instance(container),
        Component.For<IConfigService>().ImplementedBy<FakeConfigService>().LifeStyle.Transient,
        Component.For<FileFacade>().LifeStyle.Transient,
        Component.For<DirectoryFacade>().LifeStyle.Transient,
        Component.For<FileDocumentStorage>().LifeStyle.Singleton,
        Component.For<InMemoryDocumentStorage>().LifeStyle.Singleton,
        Component.For<XmlDocumentConvertor>().LifeStyle.Transient,
        Component.For<JsonDocumentConvertor>().LifeStyle.Transient,
        Component.For<DocumentStorageFactory>().LifeStyle.Transient,
        Component.For<DocumentConvertorFactory>().LifeStyle.Transient
      );

      var inMemoryDocService = new DocumentService(container.Resolve<InMemoryDocumentStorage>(),
        container.Resolve<InMemoryDocumentStorage>(), container.Resolve<DocumentConvertorFactory>());

      var fileAndIMemoryDocService = new DocumentService(container.Resolve<InMemoryDocumentStorage>(),
        container.Resolve<InMemoryDocumentStorage>(), container.Resolve<DocumentConvertorFactory>());

      container.Register(
        Component.For<DocumentService>().Instance(inMemoryDocService).Named("InMemoryDocService").LifeStyle.Singleton,
        Component.For<DocumentService>().Instance(fileAndIMemoryDocService).Named("FileAndInMemoryDocService").LifeStyle.Singleton);
    }
  }
}