using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DonDonLibrary.Database
{
    internal class Parse
    {
        private static string[] FORMATS = { "0_text" };

        internal static dynamic FromXml<T>(XmlDocument doc)
        {
            if (FORMATS.Contains(doc.DocumentElement.GetAttribute("Format")))
            {
                if(typeof(T) == typeof(TextArray))
                {
                    TextArray textArray = new TextArray();
                    textArray.signature = doc.DocumentElement.GetAttribute("Signature");
                    textArray.entries = new TextEntry[doc.DocumentElement.ChildNodes.Count];

                    int i = 0;
                    foreach(XmlElement node in doc.DocumentElement.ChildNodes)
                    {
                        textArray.entries[i] = new TextEntry();
                        textArray.entries[i].name = node.GetAttribute("Id");
                        textArray.entries[i].displayName = node.InnerText;

                        textArray.entries[i].displayName = node.InnerText.Replace("\\n", "\x0a");
                        i++;
                    }

                    return textArray;
                }
            }
            return doc;
        }
    }
}
