using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DonDonLibrary.Database;

namespace DatabaseEditor
{
    public class JsonConverter
    {
        public static void ToJson(MusicEntry[] musicEntries, string outPath)
        {
            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter sw = new StreamWriter(File.Open(outPath, FileMode.Create)))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;
                serializer.Serialize(jw, musicEntries);
            }
        }

        public static void FromJson(string inPath)
        {
            JsonSerializer serializer = new JsonSerializer();

            using (StreamReader sr = new StreamReader(File.Open(inPath, FileMode.Open)))
            using (JsonReader jr = new JsonTextReader(sr))
            {
                Console.WriteLine(serializer.Deserialize(jr));
            }
        }
    }
}
