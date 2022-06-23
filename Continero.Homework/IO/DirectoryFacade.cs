using System.IO;

namespace Continero.Homework.IO {
  public class DirectoryFacade {
    public virtual bool Exists(string path) {
      return Directory.Exists(path);
    }
  }
}