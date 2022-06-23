using System;
using System.Collections.Generic;

namespace Continero.Homework.Storages {
  public class InMemoryDocumentStorage : IDocumentStorage {
    private readonly IDictionary<string, byte[]> docs = new Dictionary<string, byte[]>();

    public byte[] Load(string docName) {
      if (docs.ContainsKey(docName) == false) {
        throw new Exception($"Document {docName} does not exist.");
      }

      return docs[docName];
    }

    public void Save(byte[] docData, string docName) {
      if (docData == null) {
        throw new ArgumentNullException(nameof(docData), "Document data can't be null.");
      }

      if (string.IsNullOrEmpty(docName)) {
        throw new ArgumentNullException(docName, "Document name can't be null or empty.");
      }

      docs[docName] = docData;
    }
  }
}