namespace Continero.Homework.Config {
  public class FakeConfigService : IConfigService {
    public string Get(string key) {
      if (key == ConfigKeys.SourceDocFilesDir) {
        return @"d:\temp\Continero.Homework\source\";
      }

      return @"d:\temp\Continero.Homework\target\";
    }
  }
}