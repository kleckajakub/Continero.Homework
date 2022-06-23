using System;
using Continero.Homework.Config;
using Continero.Homework.IO;
using Continero.Homework.Storages;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Continero.Homework.Tests.Storages {
  [TestFixture]
  public class FileDocumentStorageTests {
    private FileDocumentStorage storage;
    private IConfigService configService;
    private FileFacade fileFacade;
    private DirectoryFacade directoryFacade;

    [SetUp]
    public void SetUp() {
      configService = Substitute.For<IConfigService>();
      configService.Get(ConfigKeys.SourceDocFilesDir).Returns(@"c:\temp\source");
      configService.Get(ConfigKeys.TargetDocFilesDir).Returns(@"c:\temp\target");

      fileFacade = Substitute.For<FileFacade>();
      directoryFacade = Substitute.For<DirectoryFacade>();
      storage = new FileDocumentStorage(configService, fileFacade, directoryFacade);
    }


    [Test]
    public void load_failed_if_doc_file_does_not_exist() {
      fileFacade.Exists(@"c:\temp\source\unknownFile.xml").Returns(false);

      Assert.Throws<Exception>(() => storage.Load("unknownFile.xml"));
    }

    [Test]
    public void load_read_all_bytes_from_file() {
      var filePath = @"c:\temp\source\file.xml";
      var expectedFileBytes = new byte[] {0x01, 0x02, 0x03};
      fileFacade.Exists(filePath).Returns(true);
      fileFacade.ReadAllBytes(filePath).Returns(expectedFileBytes);

      var fileBytes = storage.Load("file.xml");

      fileBytes.Should().BeEquivalentTo(expectedFileBytes);
    }


    [Test]
    public void save_failed_if_target_doc_files_dir_does_not_exist() {
      directoryFacade.Exists(@"c:\temp\target").Returns(false);

      Assert.Throws<Exception>(() => storage.Save(new byte[3], "file.xml"));
    }

    [Test]
    public void save_bytes_to_file() {
      var fileBytes = new byte[] {0x01, 0x02, 0x03};
      directoryFacade.Exists(@"c:\temp\target").Returns(true);

      storage.Save(fileBytes, "file.xml");

      fileFacade.Received().WriteAllBytes(@"c:\temp\target\file.xml", fileBytes);
    }
  }
}