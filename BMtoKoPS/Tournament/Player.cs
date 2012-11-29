using System;
using BMtoKOPS.KOPS;

namespace BMtoKOPS {
  /// <summary>
  /// Parse 26 chair record from LUC file and provide player's info
  /// </summary>
  public class Player {
    /// <summary>
    /// Players first letter of first name and surname
    /// </summary>
    public String name;
    /// <summary>
    /// Players rating
    /// </summary>
    public double wk;
    /// <summary>
    /// Players location
    /// </summary>
    public String location;

    public Player(String name) {
      this.name = name.Substring(0, 17).Trim();
      if (!name.Substring(17, 4).Trim().Equals(String.Empty)) {
        this.wk = KopsHelper.GetDoubleFromString(name.Substring(17, 4).Trim());
      } else {
        this.wk = 0;
      }
      this.location = name.Substring(21, 5).Trim();
    }

    public bool isEmpty() {
      return name.Equals(String.Empty);
    }
  }
}
