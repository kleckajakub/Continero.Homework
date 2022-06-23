using System;
using System.IO;
using Continero.Homework.Config;
using Continero.Homework.IO;

namespace Continero.Homework.Storages {
  public class FileDocumentStorage : IDocumentStorage {
    private readonly IConfigService configService;
    private readonly FileFacade fileFacade;
    private readonly DirectoryFacade directoryFacade;

    public FileDocumentStorage(IConfigService configService, FileFacade fileFacade, DirectoryFacade directoryFacade) {
      this.configService = configService;
      this.fileFacade = fileFacade;
      this.directoryFacade = directoryFacade;
    }

    public byte[] Load(string docName) {
      var sourceFilePath = Path.Combine(configService.Get(ConfigKeys.SourceDocFilesDir), docName);

      if (fileFacade.Exists(sourceFilePath) == false) {
        throw new Exception($"Document {sourceFilePath} not found.");
      }

      return fileFacade.ReadAllBytes(sourceFilePath);
    }

    public void Save(byte[] docData, string docName) {
      var targetFileDir = configService.Get(ConfigKeys.TargetDocFilesDir);

      if (directoryFacade.Exists(targetFileDir) == false) {
        throw new Exception($"Dir {targetFileDir} does not exist.");
      }

      var targetFilePath = Path.Combine(targetFileDir, docName);
      fileFacade.WriteAllBytes(targetFilePath, docData);
    }
  }
}