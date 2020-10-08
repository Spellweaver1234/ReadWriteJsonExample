using ReadWriteJsonExample.Enums;
using System;
using System.Collections.Generic;

namespace ReadWriteJsonExample
{
    class Program
    {
        static string name = @"JsonExample.json";

        static Dictionary<NamesEnum, string[]> dictionaryOfData;

        static void Main(string[] args)
        {
            JsonManager jsonManager = new JsonManager();
            Console.WriteLine("Read start data...");
            dictionaryOfData = jsonManager.ReadJson(name);

            Console.WriteLine("Show start data:");
            ShowData(dictionaryOfData);

            Console.WriteLine("Edit data...");
            dictionaryOfData[NamesEnum.FirstNames] = new string[] { "888", "333" };
            dictionaryOfData[NamesEnum.SecondNames] = new string[] { "00", "==+==", null };
            //dictionaryOfData = null;

            Console.WriteLine("Write edited data...");
            jsonManager.WriteJson(name, dictionaryOfData);

            Console.WriteLine("Show edited data:");
            ShowData(dictionaryOfData);
        }

        static void ShowData(Dictionary<NamesEnum, string[]> dictOfData)
        {
            if (dictOfData != null)
            {
                foreach (var keyValuePair in dictOfData)
                {
                    Console.WriteLine(keyValuePair.Key);
                    foreach (var item in keyValuePair.Value)
                    {
                        if (item != null
                            && item != "")
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
            }
        }
    }
}