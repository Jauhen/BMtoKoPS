using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BMtoKOPS.KOPS;
using System.IO;

namespace BMtoKoPSTests.Integration {
  [TestFixture]
  class MultySectionTournament {
    [Test]
    public void TestMultySection() {
      KopsTournament t = new KopsTournament("1.INF", new MultySectionReader());
      t.ReadResults();

      Assert.AreEqual(MultySectionResources.PrintResults, t.PrintResults());
      Assert.AreEqual(MultySectionResources.PrintProtocols, t.PrintProtocols());
      Assert.AreEqual(MultySectionResources.PrintAllHistories, t.PrintAllHistories());
      List<int> players = new List<int>() { 1, 3, 15 };
      Assert.AreEqual(MultySectionResources.PrintListHistories, t.PrintListHistories(players));
    }
  }

  class MultySectionReader : IReader {
    public TextReader GetINFReader(String path) {
      return new StringReader(MultySectionResources._INF);
    }
    public TextReader GetTextReader(String path, String extension) {
      switch (extension) {
        case "LUC":
          return new StreamReader(new MemoryStream(MultySectionResources._LUC));
        case "SES":
          return new StreamReader(new MemoryStream(MultySectionResources._SES));
        case "NAG":
          return new StreamReader(new MemoryStream(MultySectionResources._NAG));
        case "PBN":
          return new StreamReader(new MemoryStream(MultySectionResources._PBN));
        case "INR":
          return new StreamReader(new MemoryStream(MultySectionResources._INR));
      }
      throw new FileNotFoundException();
    }
    public BinaryReader GetBinaryReader(String path, String extension) {
      switch (extension) {
        case "KNT":
          return new BinaryReader(new MemoryStream(MultySectionResources._KNT));
        case "ZAP":
          return new BinaryReader(new MemoryStream(MultySectionResources._ZAP)); ;
      }
      throw new FileNotFoundException();
    }
  }
}
