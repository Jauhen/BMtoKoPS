using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace BMtoKoPSTests {
  public class Tests {
    public static void Main() {
      AppDomain.CurrentDomain.ExecuteAssembly(
          @"C:\Program Files\NUnit 2.6.2\bin\NUnit-console.exe",
          null,
          new string[] { Assembly.GetExecutingAssembly().Location });
    }
  }
}
