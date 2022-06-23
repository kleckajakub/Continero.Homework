namespace Continero.Homework.Config {
  public class FakeConfigService : IConfigService {
    public string Get(string key) {
      if (key == ConfigKeys.SourceDocFilesDir) {
        return @"c:\temp\source_document.dat";
      }

      return @"c:\temp\target_document.dat";
    }
  }
}