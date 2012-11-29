using System;
using System.Collections.Generic;
using System.Text;

namespace BMtoKOPS {
  public class Pairs {
    private List<List<Player>> pairs;
    private List<int> numbers;

    public Pairs() {
      pairs = new List<List<Player>>();
    }

    public void AddPair(List<Player> players) {
      pairs.Add(players);
    }

    public String GetPairNames(int n) {
      if (pairs[n][0].isEmpty() && pairs[n][1].isEmpty()) {
        return "";
      } else {
        return String.Format("<nobr>{0} &mdash; {1}</nobr>",
            pairs[n][0].name,
            pairs[n][1].name);
      }
    }

    public double GetPairRank(int n) {
      return pairs[n][0].wk + pairs[n][1].wk;
    }

    public String GetPairRegion(int n) {
      return pairs[n][0].location.Equals(pairs[n][1].location) ?
              pairs[n][0].location :
              String.Format("{0}/{1}", pairs[n][0].location, pairs[n][1].location);
    }

    public void SetNumbers(List<int> numbers) {
      this.numbers = numbers;
    }

    public int GetMaxNumber() {
      return numbers.Count;
    }

    public int GetNumber(int i) {
      return numbers[i];
    }

    public int GetInternalPairNumber(int n) {
      int counter = -1;
      for (int i = 0; i < numbers.Count; i++) {
        if (numbers[i] != 0 && i < n) {
          counter++;
        }
      }
      return counter;
    }

    public int GetPairNumber(int n) {
      for (int i = 0; i < numbers.Count; i++) {
        if (numbers[i] == n) {
          return i + 1;
        }
      }

      return 0;
    }

    public int GetNumberOfPairs() {
      return pairs.Count;
    }
  }
}
