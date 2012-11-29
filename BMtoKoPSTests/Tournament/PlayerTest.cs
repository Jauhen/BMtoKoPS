using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMtoKOPS;
using NUnit.Framework;

namespace BMtoKoPSTests.Tournament {
  [TestFixture]
  public class PlayerTest {
    [Test]
    public void TestPlayer() {
      Player player = new Player("V Surkov          2.5VT   ");

      Assert.AreEqual("V Surkov", player.name);
      Assert.AreEqual(2.5, player.wk);
      Assert.AreEqual("VT", player.location);
      Assert.IsFalse(player.isEmpty());
    }

    [Test]
    public void TestPlayerWithoutRating() {
      Player player = new Player("V Surkov             VT   ");

      Assert.AreEqual(0, player.wk);
    }

    [Test]
    public void TestPlayerEmptyLine() {
      Player player = new Player("                          ");

      Assert.AreEqual("", player.name);
      Assert.AreEqual(0, player.wk);
      Assert.AreEqual("", player.location);
      Assert.IsTrue(player.isEmpty());
    }
  }
}
