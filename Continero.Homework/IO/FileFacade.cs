using System.IO;

namespace Continero.Homework.IO {
  public class FileFacade {
    public virtual bool Exists(string path) {
      return File.Exists(path);
    }

    public virtual byte[] ReadAllBytes(string path) {
      return File.ReadAllBytes(path);
    }

    public virtual void WriteAllBytes(string path, byte[] bytes) {
      File.WriteAllBytes(path, bytes);
    }
  }
}