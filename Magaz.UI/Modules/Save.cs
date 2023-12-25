using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Magaz.Logic;

namespace Magaz.UI
{
    internal class Save
    {
        public static void ToCsv(List<Autocenter> info, string path)
        {
            List<string> lines = new List<string>();
            foreach (var item in info)
            {
                lines.Add(item.ToString());
            }
            try
            {
                File.WriteAllLines(path, lines);
                Console.WriteLine($"Було збережено у файл: {Path.GetFullPath(path)}");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        public static void ToJson(List<Autocenter> info, string path)
        {
            try
            {
                string jsonstring = "";
                foreach (var item in info)
                {
                    jsonstring += JsonSerializer.Serialize<Autocenter>(item);
                    jsonstring += "\n";
                }
                File.WriteAllText(path, jsonstring);
                Console.WriteLine($"Було збережено у файл: {Path.GetFullPath(path)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
