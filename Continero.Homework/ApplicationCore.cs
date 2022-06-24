using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Continero.Homework {
  public class ApplicationCore {
    private readonly IWindsorContainer container;

    public ApplicationCore(IWindsorContainer container) {
      this.container = container;
    }

    public ApplicationCore Initialize() {
      AppDomain.CurrentDomain.UnhandledException += (sender, e) => {
        if (e.ExceptionObject is Exception exception)
          Console.WriteLine(exception);
      }; 

      return this;
    }

    public ApplicationCore InstallServices(params IWindsorInstaller[] installers) {
      container.Install(installers);
      return this;
    }

    //program/service loop can be here
    public void Run() {
      var documentService = container.Resolve<IDocumentDownloader>();
      
      ProcessDocument(documentService, "sourceDoc.xml", "targetDoc.json");
      ProcessDocument(documentService, "sourceDoc.json", "targetDoc.xml");

      Console.WriteLine("Press enter to exit.");
      Console.ReadLine();
    }

    private void ProcessDocument(IDocumentDownloader documentDownloader, string sourceDocName, string targetDocName) {
      try {
        documentDownloader.DownloadDocument(sourceDocName, targetDocName);
      } catch (Exception ex) {
        Console.WriteLine($"Error occurred during processing document.\r\n{ex.Message}");
      }
    }
  }
}