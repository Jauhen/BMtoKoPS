using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace BMtoKoPSTests {
  public class Tests {
    public static void Main() {
      AppDomain.CurrentDomain.ExecuteAssembly(
          @"C:\Program Files (x86)\NUnit\bin\NUnit-console.exe",
          null,
          new string[] { Assembly.GetExecutingAssembly().Location });
    }
  }
}
