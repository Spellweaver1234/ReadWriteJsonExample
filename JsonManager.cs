using Newtonsoft.Json.Linq;
using ReadWriteJsonExample.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReadWriteJsonExample
{
    public class JsonManager
    {
        public JsonManager() { }

        public Dictionary<NamesEnum, string[]> ReadJson(string name)
        {
            var dictOfData = new Dictionary<NamesEnum, string[]>();

            try
            {
                if (!File.Exists(name))
                    WriteJson(name, null);

                using (var streamReader = new StreamReader(name))
                {
                    var jString = streamReader.ReadToEnd();
                    var jObject = JObject.Parse(jString);
                    var jEnums = (NamesEnum[])Enum.GetValues(typeof(NamesEnum));

                    dictOfData = new Dictionary<NamesEnum, string[]>();
                    foreach (var item in (NamesEnum[])Enum.GetValues(typeof(NamesEnum)))
                    {
                        var json = jObject[item.ToString()]?.ToString();
                        var key = item.ToString();

                        if (jObject.ContainsKey(key))
                        {
                            var jArray = JArray.Parse(json);
                            var value = jArray.Values<string>().ToArray();
                            dictOfData.Add(item, value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}");
            }

            return dictOfData;
        }

        public void WriteJson(string name, Dictionary<NamesEnum, string[]> dictOfData)
        {
            if (dictOfData == null) 
                dictOfData = new Dictionary<NamesEnum, string[]>();

            try
            {
                using (var streamWriter = new StreamWriter(name))
                {
                    var jObject = new JObject();
                    var jEnums = dictOfData.Keys.ToArray();

                    foreach (var item in jEnums)
                    {
                        var value = new JArray() { dictOfData[item].ToArray() };
                        jObject.Add(item.ToString(), value);
                    }

                    streamWriter.WriteLine(jObject);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}