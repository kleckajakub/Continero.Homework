using Castle.Windsor;
using Continero.Homework.Installers;

namespace Continero.Homework {
  public class Program {
    private static void Main() {
      var container = new WindsorContainer();
      new ApplicationCore(container).Initialize()
                                    .InstallServices(new ServicesInstaller())
                                    .Run();
    }
  }
}