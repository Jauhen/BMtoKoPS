using System;

namespace BMtoKOPS
{
    public class Player
    {
        public String n;
        public double w;
        public String l;

        public Player(String name)
        {
            n = name.Substring(0, 17).Trim();
            if (!name.Substring(17, 4).Trim().Equals(String.Empty))
            {
                w = KopsHelper.GetDoubleFromString(name.Substring(17, 4).Trim());
            }
            else
            {
                w = 0;
            }
            l = name.Substring(21, 5).Trim();
        }
    }
}
