using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BMtoKOPS.KOPS {
  public interface IReader {
    TextReader GetINFReader(String path);
    TextReader GetTextReader(String path, String extension);
    BinaryReader GetBinaryReader(String path, String extension);
  }
}
