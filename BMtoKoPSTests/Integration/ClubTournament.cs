using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BMtoKOPS.KOPS;
using System.IO;
using System.Reflection;

namespace BMtoKoPSTests.Integration {
  [TestFixture]
  class ClubTournament {
    [Test]
    public void TestClubTournament() {
      KopsTournament t = new KopsTournament("1.INF", new ClubReader());
      t.ReadResults();

      Assert.AreEqual(ClubResources.PrintResults, t.PrintResults());
      Assert.AreEqual(ClubResources.PrintProtocols, t.PrintProtocols());
      Assert.AreEqual(ClubResources.PrintAllHistories, t.PrintAllHistories());
      List<int> players = new List<int>() { 1, 3, 10 };
      Assert.AreEqual(ClubResources.PrintListHistories, t.PrintListHistories(players));
    }
  }

  class ClubReader : IReader {
    public TextReader GetINFReader(String path) {
      return new StringReader(ClubResources._INF);
    }
    public TextReader GetTextReader(String path, String extension) {
      switch (extension) {
        case "LUC":
          return new StreamReader(new MemoryStream(ClubResources._LUC));
        case "SES":
          return new StreamReader(new MemoryStream(ClubResources._SES));
        case "NAG":
          return new StreamReader(new MemoryStream(ClubResources._NAG));
      }
      throw new FileNotFoundException();
    }
    public BinaryReader GetBinaryReader(String path, String extension) {
      switch (extension) {
        case "KNT":
          return new BinaryReader(new MemoryStream(ClubResources._KNT));
        case "ZAP":
          return new BinaryReader(new MemoryStream(ClubResources._ZAP));;
      }
      throw new FileNotFoundException();
    }
  }
}
