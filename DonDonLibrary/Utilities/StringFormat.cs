using System;
using System.Threading;

namespace DonDonLibrary.Utilities
{
    public class StringFormat
    {
        private static string decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        public static string Stringify(float value)
        {
            return value.ToString().Replace(decimalSeparator, ".");
        }

        public static float Destringify(string value)
        {
            float valuef = 0;
            if (float.TryParse(value.Replace(".", decimalSeparator), out valuef))
                return valuef;
            else
                throw new Exception("Couldn't parse float");
        }
    }
}
