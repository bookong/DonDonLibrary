using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonDonLibrary.Utilities
{
    public class StringFormat
    {
        public static string Stringify(float value)
        {
            return value.ToString().Replace(',', '.');
        }

        public static float Destringify(string value)
        {
            float valuef = 0;
            if (float.TryParse(value.Replace('.', ','), out valuef))
                return valuef;
            else
                throw new Exception("Couldn't parse float");
        }
    }
}
