using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BMtoKOPS.KOPS {
  public class KopsReader : IReader {
    public TextReader GetINFReader(String path) {
      return new StreamReader(path);
    }

    public BinaryReader GetBinaryReader(String path, String extension) {
      String pathRes = String.Format(@"{0}\{1}.{2}",
          Path.GetDirectoryName(path),
          Path.GetFileNameWithoutExtension(path),
          extension);

      return new BinaryReader(File.OpenRead(pathRes));
    }

    public TextReader GetTextReader(String path, String extension) {
      String pathRes = String.Format(@"{0}\{1}.{2}",
          Path.GetDirectoryName(path),
          Path.GetFileNameWithoutExtension(path),
          extension);

      return new StreamReader(pathRes);
    }
  }
}
