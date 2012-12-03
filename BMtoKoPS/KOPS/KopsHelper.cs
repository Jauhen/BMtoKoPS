using System;
using System.Globalization;

namespace BMtoKOPS.KOPS {
    public static class KopsHelper {
        public static System.Globalization.CultureInfo GetLocalInfo() {
            return new System.Globalization.CultureInfo("ru-RU");
        }

        public static Double GetDoubleFromString(String str) {
            // prepare the string
            str = str.Trim(new char[] { '(', ')', ' ' });

            Double res = 0;

            // get current settings
            var format = new System.Globalization.NumberFormatInfo();

            // Try to covert string to number
            if (!Double.TryParse(str, NumberStyles.Float, format, out res)) {
                if (format.NumberDecimalSeparator == ",")
                    format.NumberDecimalSeparator = ".";
                else if (format.NumberDecimalSeparator == ".")
                    format.NumberDecimalSeparator = ",";
                // If we can't, return 0
                if (!Double.TryParse(str, NumberStyles.Float, format, out res))
                    return 0;
            }
            return res;
        }
    }
}
